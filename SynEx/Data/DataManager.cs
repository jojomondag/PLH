using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using SynEx.Services;
using System;
using SynEx.Helpers;
using SynEx.Utils;

namespace SynEx.Data
{
    public class DataManager
    {
        public static async Task ProcessProjectItemsAsync(EnvDTE.ProjectItems projectItems, List<string> fileSystemItems, string folderPath, int indentLevel, JoinableTaskFactory joinableTaskFactory)
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

                    await ProcessProjectItemsAsync(item.ProjectItems, fileSystemItems, subFolderPath, indentLevel + 1, joinableTaskFactory);
                }
            }
        }
        public static async Task SaveCoordinatorAsync(string action)
        {

            // Get all file system items in the current solution
            IEnumerable<string> fileSystemItems = DTEProvider.GetAllProjectFileSystemItems(DTEProvider.Instance.Solution);

            // Get the C# files from the file system items
            List<string> csFiles = fileSystemItems.Where(item => item.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)).ToList();
            List<string> combinedItems = new();

            string actionName;

            // Create an instance of CodeParser
            SynExDataExtractor codeParser = new SynExDataExtractor();

            switch (action)
            {
                case "1":
                    combinedItems = await codeParser.ExtractDetailsAsync(csFiles, 1);
                    actionName = "FunctionNames";
                    break;

                case "2":
                    combinedItems = await codeParser.ExtractDetailsAsync(csFiles, 2);
                    actionName = "FunctionNames_Parameters";
                    break;

                case "3":
                    combinedItems = await codeParser.ExtractDetailsAsync(csFiles, 3);
                    actionName = "FunctionNames_Parameters_ReturnTypes";
                    break;

                case "4":
                    combinedItems = await codeParser.ExtractDetailsAsync(csFiles, 4);
                    actionName = "AccessModifier_Static_FunctionNames_Parameters_ReturnTypes";
                    break;

                default:
                    MessageHelper.ShowError("Error: Unrecognized action.");
                    return;
            }

            ClipboardManager.SetTextToClipboard(combinedItems);
        }
        public static async Task SaveCombinedItemsToFileAsync(string nameOfAction, List<string> combinedItems, string projectPath)
        {
            if (combinedItems == null || combinedItems.Count == 0) return;

            // Create a SynEx directory within the project directory
            string synexPath = Path.Combine(projectPath, "SynEx");
            Directory.CreateDirectory(synexPath);

            // Create a unique filename using the current date and time
            string fileName = $"{nameOfAction}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(synexPath, fileName);

            using StreamWriter sw = new StreamWriter(filePath);

            // Write each item in combinedItems to the file
            foreach (string item in combinedItems)
            {
                await sw.WriteLineAsync(item);
            }
        }
    }
}
