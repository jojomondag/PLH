using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = System.Windows.MessageBox;
using EnvDTE;

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

        public static async Task<List<string>> GetCsFilesAsync(List<ProjectItem> projectItems)
        {
            List<string> csFiles = new();

            foreach (var projectItem in projectItems)
            {
                if (projectItem.Name.EndsWith(".cs"))
                {
                    string filePath = projectItem.FileNames[0];
                    csFiles.Add(filePath);
                }
            }

            return csFiles;
        }

        public static async Task SaveCoordinatorAsync(string action)
        {
            // Get the C# project items using UserControl
            List<ProjectItem> projectItems = await UserControl.Instance.GetAllCsProjectItemsAsync();

            // Get the C# files from the project items
            List<string> csFiles = await GetCsFilesAsync(projectItems);

            string folderPath = await UserControl.Instance.GetSolutionPathAsync();
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

            await SaveCombinedItemsToFileAsync(combinedItems);
            ClipboardManager.SetTextToClipboard(combinedItems);
        }
        public static async Task SaveCombinedItemsToFileAsync(List<string> combinedItems)
        {
            if (combinedItems == null || combinedItems.Count == 0) return;

            // Get the solution// path and create a SynEx directory within it
            string solutionPath = await UserControl.Instance.GetSolutionPathAsync();
            string synexPath = Path.Combine(solutionPath, "SynEx");
            Directory.CreateDirectory(synexPath);

            // Create a unique filename using the current date and time
            string fileName = $"CombinedItems_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(synexPath, fileName);

            using StreamWriter sw = new StreamWriter(filePath);
            foreach (string item in combinedItems)
            {
                sw.WriteLine(item);
            }
        }
    }
}