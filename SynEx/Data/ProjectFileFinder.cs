using EnvDTE;
using Microsoft.VisualStudio.Shell;
using SynEx.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynEx
{
    public class ProjectFileFinder
    {
        private void FindProjectFilesRecursively(EnvDTE.ProjectItems projectItems, List<EnvDTE.ProjectItem> projectFiles)
        {
            if (projectItems == null) return;

            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (EnvDTE.ProjectItem item in projectItems)
            {
                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    projectFiles.Add(item);
                }
                else if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    FindProjectFilesRecursively(item.ProjectItems, projectFiles);
                }
            }
        }
        public static async Task<List<string>> GetCsFilesAsync(List<ProjectItem> projectItems)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            return await Task.Run(() =>
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
            });
        }
        public async Task<List<EnvDTE.ProjectItem>> FindAllProjectFilesAsync()
        {
            List<EnvDTE.ProjectItem> projectFiles = new List<EnvDTE.ProjectItem>();

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var projects = DTEProvider.Instance.Solution.Projects;
            foreach (EnvDTE.Project project in projects)
            {
                FindProjectFilesRecursively(project.ProjectItems, projectFiles);
            }

            return projectFiles;
        }
    }
}