using SynEx.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ExtractFolderStructure
{
    public static async Task ExtractFilesAndFolderStructureTree()
    {
        // Get the path to the solution using the DataManager class's GetSolutionPathAsync method
        string solutionPath = await DataManager.GetSolutionPathAsync();

        // Extract the folder structure and file paths using the solution path
        IEnumerable<string> folderStructure = Directory.EnumerateDirectories(solutionPath, "*", SearchOption.AllDirectories);
        IEnumerable<string> fileNames = Directory.EnumerateFiles(solutionPath, "*", SearchOption.AllDirectories);

        // Combine the folder structure and file paths into a single collection
        IEnumerable<string> folderAndFilePaths = folderStructure.Concat(fileNames);

        // Copy the folder and file paths to the clipboard
        ClipboardManager.SetTextToClipboard(folderAndFilePaths.ToList());

        // Save the folder and file paths to a file
        await DataManager.SaveCombinedItemsToFileAsync(folderAndFilePaths.ToList());
    }
}