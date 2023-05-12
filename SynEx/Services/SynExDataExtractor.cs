using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynEx.Data;
using EnvDTE;

namespace SynEx.Services
{
    public class SynExDataExtractor
    {
        private readonly JoinableTaskFactory _joinableTaskFactory;

        public SynExDataExtractor()
        {
            _joinableTaskFactory = ThreadHelper.JoinableTaskFactory;
        }
        public async Task<List<string>> ExtractDetailsAsync(List<string> csFiles, int extractionLevel)
        {
            List<string> combinedItems = new();

            foreach (string file in csFiles)
            {
                string fileContent = await Task.Run(() => File.ReadAllText(file));

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
        public async Task<List<string>> ExtractFilesAndFolderStructureTreeAsync()
        {
            DTE dte = DTEProvider.Instance;

            List<string> fileSystemItems = new List<string>();

            string solutionPath = Path.GetDirectoryName(dte.Solution.FullName);

            // Create a JoinableTaskFactory instance
            JoinableTaskFactory joinableTaskFactory = ThreadHelper.JoinableTaskFactory;

            // Add all file and folder paths in the solution to the list
            foreach (EnvDTE.Project project in dte.Solution.Projects)
            {
                if (project.Kind != EnvDTE.Constants.vsCATIDSolution)
                {
                    // Get all files in the project
                    foreach (EnvDTE.ProjectItem item in project.ProjectItems)
                    {
                        if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                        {
                            fileSystemItems.Add($"{project.Name}\\{item.Name}");
                        }
                    }

                    // Get all folders and their files in the project
                    foreach (EnvDTE.ProjectItem item in project.ProjectItems)
                    {
                        if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                        {
                            string folderPath = $"{project.Name}\\{item.Name}";
                            fileSystemItems.Add(folderPath);

                            // Pass the JoinableTaskFactory instance to the ProcessProjectItemsAsync method
                            await DataManager.ProcessProjectItemsAsync(item.ProjectItems, fileSystemItems, folderPath, 1, joinableTaskFactory);
                        }
                    }
                }
                else
                {
                    // Get all files and folders in the solution folder
                    await DataManager.ProcessProjectItemsAsync(project.ProjectItems, fileSystemItems, "", 0, joinableTaskFactory);
                }
            }

            // Add the solution path to the beginning of the list
            fileSystemItems.Insert(0, solutionPath);

            return fileSystemItems;
        }
    }
}