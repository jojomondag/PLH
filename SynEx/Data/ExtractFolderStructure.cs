using System.Collections.Generic;
using EnvDTE;
using SynEx.Data;
using EnvDTE80;
using System.Linq;

namespace SynEx
{
    public class ExtractFolderStructure
    {
        public static async Task ExtractFilesAndFolderStructureTree()
        {
            DTE dte = UserControl.Instance.Dte;
            EnvDTE.Solution solution = dte.Solution;

            List<string> filePaths = new List<string>();
            List<string> folderPaths = new List<string>();

            foreach (EnvDTE.Project project in solution.Projects)
            {
                if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
                {
                    ProcessProjectItems(project.ProjectItems, filePaths, folderPaths);
                }
            }

            // Copy the gathered file paths and folder paths to the clipboard
            ClipboardManager.SetTextToClipboard(filePaths.Concat(folderPaths).ToList());
            // Do something with filePaths and folderPaths lists if necessary
        }

        private static void ProcessProjectItems(ProjectItems projectItems, List<string> filePaths, List<string> folderPaths)
        {
            foreach (EnvDTE.ProjectItem item in projectItems)
            {
                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    string filePath = item.FileNames[1];
                    filePaths.Add(filePath);
                }
                else if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    string folderPath = item.FileNames[1];
                    folderPaths.Add(folderPath);
                    if (item.ProjectItems != null)
                    {
                        ProcessProjectItems(item.ProjectItems, filePaths, folderPaths);
                    }
                }
            }
        }
    }
}
