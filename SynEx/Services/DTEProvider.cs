using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using SynEx.Helpers;

namespace SynEx.Services
{
    public static class DTEProvider
    {
        private static DTE _instance;
        public static DTE Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Get the DTE service
                    _instance = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE;
                }

                return _instance;
            }
        }
        public static IEnumerable<string> GetAllProjectFileSystemItems(Solution solution)
        {
            var fileSystemItems = new List<string>();

            foreach (Project project in solution.Projects)
            {
                TraverseProjectItems(project.ProjectItems, fileSystemItems);
            }

            return fileSystemItems;
        }
        private static void TraverseProjectItems(ProjectItems projectItems, List<string> fileSystemItems)
        {
            if (projectItems == null)
            {
                return;
            }

            foreach (ProjectItem item in projectItems)
            {
                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    fileSystemItems.Add(item.FileNames[1]);
                }
                else if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    fileSystemItems.Add(item.FileNames[1]);
                    TraverseProjectItems(item.ProjectItems, fileSystemItems);
                }
            }
        }
        public static string GetActiveProjectName()
        {
            var activeProjects = Instance.ActiveSolutionProjects as Array;
            if (activeProjects != null && activeProjects.Length > 0)
            {
                Project activeProject = activeProjects.GetValue(0) as Project;
                return activeProject?.Name;
            }

            return null;
        }        
        //Create a function that gets the path to active project
        public static string GetActiveProjectPath()
        {
            // Get the currently selected project in the Solution Explorer
            var selectedProject = Instance.SelectedItems.Item(1).Project;
            if (selectedProject != null)
            {
                // If the selected project is a VSProject, the Properties item "FullPath" will give the full path to the project directory
                var projectPath = selectedProject.Properties.Item("FullPath").Value as string;

                // Check if the projectPath is null or if the selected project's name is equal to the name of the solution
                if (string.IsNullOrEmpty(projectPath) || selectedProject.Name == Instance.Solution.FullName)
                {
                    MessageHelper.ShowWarning("Please select a project in the Solution Explorer.");
                    return null;
                }

                return projectPath;
            }

            MessageHelper.ShowWarning("No project selected. Please select a project in the Solution Explorer. The project has to be maximized for it to be selected");
            return null;
        }

    }
}
