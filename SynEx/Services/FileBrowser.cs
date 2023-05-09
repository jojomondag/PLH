using EnvDTE;
using System.IO;
using System.Windows.Forms;

public class DirPathPick
{
    private readonly ProjectInfo _projectInfo;

    private readonly string _pathStorageFile;
    private const string _pathStorageFileName = "pathStorage.json";

    public DirPathPick(DTE dte)
    {
        var projectInfo = new ProjectInfo
        {
            Name = dte.Solution.FullName,
            Path = ""
        };
        SynExFolder.SaveProjectInfo(projectInfo);

        string solutionDir = Path.GetDirectoryName(dte.Solution.FullName);
        _pathStorageFile = Path.Combine(solutionDir, _pathStorageFileName);
        CreateAndShowDialog();
    }

    private void CreateAndShowDialog()
    {
        // Create an instance of the FolderBrowserDialog class
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

        // Set the properties of the FolderBrowserDialog
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.Description = "Select a folder to save your files";

        // Display the dialog and get the selected path
        DialogResult result = folderBrowserDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            string selectedPath = folderBrowserDialog.SelectedPath;

            // Store the selected path in the ProjectInfo object
            _projectInfo.Path = selectedPath;

            // Save the ProjectInfo object to a JSON file
            SynExFolder.SaveProjectInfo(_projectInfo);
        }
    }
}