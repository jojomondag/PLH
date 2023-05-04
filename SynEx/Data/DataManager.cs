using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = System.Windows.MessageBox;

namespace SynEx.Data
{
    public class DataManager
    {
        public static async Task<List<string>> ExtractDetails(List<string> csFiles, int extractionLevel)
        {
            List<string> combinedItems = new();

            foreach (string file in csFiles)
            {
                string fileContent = File.ReadAllText(file);
                SyntaxTree tree = CSharpSyntaxTree.ParseText(fileContent);
                CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

                var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
                foreach (var classDeclaration in classDeclarations)
                {
                    string className = classDeclaration.Identifier.ValueText;
                    combinedItems.Add(className);

                    var methodDeclarations = classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>();
                    foreach (var methodDeclaration in methodDeclarations)
                    {
                        StringBuilder sb = new();

                        if (extractionLevel >= 4)
                        {
                            var modifiers = methodDeclaration.Modifiers;
                            foreach (var modifier in modifiers)
                            {
                                sb.Append(modifier.ValueText + " ");
                            }
                        }

                        if (extractionLevel >= 3)
                        {
                            sb.Append(methodDeclaration.ReturnType.ToString() + " ");
                        }

                        sb.Append(methodDeclaration.Identifier.ValueText);

                        if (extractionLevel >= 2)
                        {
                            sb.Append("(");
                            var parameters = methodDeclaration.ParameterList.Parameters;
                            for (int i = 0; i < parameters.Count; i++)
                            {
                                sb.Append(parameters[i].ToString());
                                if (i < parameters.Count - 1) sb.Append(", ");
                            }
                            sb.Append(")");
                        }

                        combinedItems.Add("\t" + sb.ToString());
                    }
                }
            }

            return combinedItems;
        }

        // Create a function that gets the path of the currently opened solution
        public static async Task<string> GetSolutionPathAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));

            if (!string.IsNullOrEmpty(dte.Solution.FullName))
            {
                string solutionPath = Path.GetDirectoryName(dte.Solution.FullName);
                return solutionPath;
            }
            else
            {
                return null;
            }
        }

        // Create a function that finds all files of type .cs and stores them in a list
        public static async Task<List<string>> GetCsFilesAsync()
        {
            List<string> csFiles = new();

            // Get the solution path
            string solutionPath = await GetSolutionPathAsync();
            if (!string.IsNullOrEmpty(solutionPath))
            {
                // Find all .cs files in the solution directory and subdirectories
                csFiles = Directory.GetFiles(solutionPath, "*.cs", SearchOption.AllDirectories).ToList();
            }

            return csFiles;
        }
        public static async Task SaveCoordinatorAsync(string action)
        {
            List<string> csFiles = await GetCsFilesAsync();
            string folderPath = await GetSolutionPathAsync();
            List<string> combinedItems = new();

            switch (action)
            {
                case "1":
                    combinedItems = await ExtractDetails(csFiles, 1);
                    break;

                case "2":
                    combinedItems = await ExtractDetails(csFiles, 2);
                    break;

                case "3":
                    combinedItems = await ExtractDetails(csFiles, 3);
                    break;

                case "4":
                    combinedItems = await ExtractDetails(csFiles, 4);
                    break;

                default:
                    MessageBox.Show("Error: Unrecognized action.");
                    return;
            }

            SaveCombinedItemsToFile(combinedItems, folderPath);
            ClipboardManager.SetTextToClipboard(combinedItems);
        }
        public static void SaveCombinedItemsToFile(List<string> combinedItems, string folderPath)
        {
            if (combinedItems == null || combinedItems.Count == 0) return;

            // Create a unique filename using the current date and time
            string fileName = $"CombinedItems_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            using StreamWriter sw = new StreamWriter(filePath);
            foreach (string item in combinedItems)
            {
                sw.WriteLine(item);
            }
        }
    }
}