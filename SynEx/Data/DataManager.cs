using EnvDTE;
using System.IO;

namespace SynEx.Data
{
    public static class DataManager
    {
        // Create a function that gets the path of the currently opened solution
        public static string GetSolutionPath(DTE dte)
        {
            // Check if there is a loaded solution
            if (!string.IsNullOrEmpty(dte.Solution.FullName))
            {
                // Get the path of the currently loaded solution
                string solutionPath = Path.GetDirectoryName(dte.Solution.FullName);
                return solutionPath;
            }
            else
            {
                // Return null or an empty string if there is no loaded solution
                return null;
            }
        }
    }
}
