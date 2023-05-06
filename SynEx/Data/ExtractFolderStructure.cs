using System.Collections.Generic;
using EnvDTE;
using SynEx.Data;
using EnvDTE80;
using System.Linq;
using System.IO;

namespace SynEx
{
    public class ExtractFolderStructure
    {
        public static async Task ExtractFilesAndFolderStructureTree()
        {
            DTE dte = UserControl.Instance.Dte;
            EnvDTE.Solution solution = dte.Solution;

            List<string> fileSystemItems = new List<string>();

            // Get full path of the solution
            string solutionPath = Path.GetDirectoryName(solution.FullName);

            // Get all file and folder paths in the solution
            foreach (EnvDTE.Project project in solution.Projects)
            {
                if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
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

                            ProcessProjectItems(item.ProjectItems, fileSystemItems, folderPath, 1);
                        }
                    }
                }
                else
                {
                    // Get all files and folders in the solution folder
                    ProcessProjectItems(project.ProjectItems, fileSystemItems, "", 0);
                }
            }

            // Add solution path to the top of the output
            fileSystemItems.Insert(0, solutionPath);

            DataManager.SaveCombinedItemsToFileAsync("ExtractFolderStructureTree", fileSystemItems);
            ClipboardManager.SetTextToClipboard(fileSystemItems);
            // Do something with filePaths and folderPaths lists if necessary
        }

        private static void ProcessProjectItems(EnvDTE.ProjectItems projectItems, List<string> fileSystemItems, string folderPath, int indentLevel)
        {
            foreach (EnvDTE.ProjectItem item in projectItems)
            {
                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    string filePath = $"{folderPath}\\{item.Name}";
                    string indentedFilePath = new string(' ', indentLevel * 4) + filePath;
                    fileSystemItems.Add(indentedFilePath);
                }

                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    string subFolderPath = $"{folderPath}\\{item.Name}";
                    string indentedSubFolderPath = new string(' ', indentLevel * 4) + subFolderPath;
                    fileSystemItems.Add(indentedSubFolderPath);

                    ProcessProjectItems(item.ProjectItems, fileSystemItems, subFolderPath, indentLevel + 1);
                }
            }
        }
    }
}
