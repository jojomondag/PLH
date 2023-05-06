using EnvDTE;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Project = EnvDTE.Project;

namespace SynEx
{
    public class UserControl
    {
        private static UserControl _instance;
        private readonly DTE _dte;

        private void GetProjectItemsRecursively(ProjectItems projectItems, List<ProjectItem> allItems)
        {
            if (projectItems == null) return;

            foreach (ProjectItem item in projectItems)
            {
                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    allItems.Add(item);
                }
                else if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    GetProjectItemsRecursively(item.ProjectItems, allItems);
                }
            }
        }
        public async Task<List<ProjectItem>> GetAllCsProjectItemsAsync()
        {
            List<ProjectItem> projectItems = new List<ProjectItem>();

            var projects = _dte.Solution.Projects;
            foreach (Project project in projects)
            {
                GetProjectItemsRecursively(project.ProjectItems, projectItems);
            }

            return projectItems;
        }
        private UserControl(DTE dte)
        {
            if (dte == null)
            {
                throw new ArgumentNullException(nameof(dte));
            }

            _dte = dte;
        }

        public static UserControl Initialize(IServiceProvider serviceProvider)
        {
            if (_instance == null)
            {
                DTE dte = (DTE)serviceProvider.GetService(typeof(DTE));
                if (dte == null)
                {
                    throw new InvalidOperationException("Failed to get DTE object.");
                }
                _instance = new UserControl(dte);
            }
            return _instance;
        }

        public static UserControl Instance
        {
            get
            {
                return _instance;
            }
        }

        public DTE Dte => _dte;

        public async Task<string> GetSolutionPathAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (!string.IsNullOrEmpty(_dte.Solution.FullName))
            {
                string solutionPath = Path.GetDirectoryName(_dte.Solution.FullName);
                return solutionPath;
            }
            else
            {
                return null;
            }
        }
    }
}
