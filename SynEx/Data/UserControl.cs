using EnvDTE;
using System.IO;
using System.Threading.Tasks;

namespace SynEx
{
    public class UserControl
    {
        private static UserControl _instance;
        private readonly DTE _dte;

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
