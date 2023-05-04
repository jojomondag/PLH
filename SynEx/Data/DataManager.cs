using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using SynEx.Logic;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SynEx.Data
{
    public class DataManager
    {
        // Create a function that gets the path of the currently opened solution
        public static string GetSolutionPath()
        {
            // Make sure we're on the UI thread before accessing the DTE object
            ThreadHelper.ThrowIfNotOnUIThread();

            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));

            if (!string.IsNullOrEmpty(dte.Solution.FullName))
            {
                string solutionPath = Path.GetDirectoryName(dte.Solution.FullName);
                return solutionPath;
            }
            else
            {
                return null;
            }
        }

        // Create a function that finds all files of type .cs and stores them in a list
        public static List<string> GetCsFiles()
        {
            List<string> csFiles = new List<string>();

            // Get the solution path
            string solutionPath = GetSolutionPath();

            if (!string.IsNullOrEmpty(solutionPath))
            {
                // Find all .cs files in the solution directory and subdirectories
                csFiles = Directory.GetFiles(solutionPath, "*.cs", SearchOption.AllDirectories).ToList();
            }

            return csFiles;
        }

        // Create a helper method that returns the current date and time as a formatted string
        public static string GetCurrentDateTimeString()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        // Create a helper method that saves text to a file using a TextWriter
        private static void SaveTextToFile(string filePath, List<string> textLines)
        {
            using (TextWriter writer = File.CreateText(filePath))
            {
                foreach (string line in textLines)
                {
                    // Write each line of text to the file
                    writer.WriteLine(line);
                }
            }
        }

        // Create a function that saves a list of function names to a file with the current date and time in the file name
        public static void SaveFunctionNamesToFile(List<string> functionNames, string folderPath)
        {
            // Get the current date and time as a formatted string
            string dateTimeString = GetCurrentDateTimeString();

            // Combine the folder path and date/time string to create the file path
            string filePath = Path.Combine(folderPath, $"functionNames_{dateTimeString}.txt");

            // Call the SaveTextToFile function to save the function names to the file
            SaveTextToFile(filePath, functionNames);
        }

        // Create a function that saves a list of class names to a file with the current date and time in the file name
        public static void SaveClassNamesToFile(List<string> classNames, string folderPath)
        {
            // Get the current date and time as a formatted string
            string dateTimeString = GetCurrentDateTimeString();

            // Combine the folder path and date/time string to create the file path
            string filePath = Path.Combine(folderPath, $"classNames_{dateTimeString}.txt");

            // Call the SaveTextToFile function to save the class names to the file
            SaveTextToFile(filePath, classNames);
        }

        //SaveCoordinator is the way we coordinate how the files/Code Text shall be Saved and Copied.
        private static Dictionary<string, List<string>> AssociateItemsWithFiles(List<string> csFiles, List<string> items)
        {
            Dictionary<string, List<string>> fileToItemsMap = new Dictionary<string, List<string>>();

            foreach (string item in items)
            {
                string foundFile = csFiles.FirstOrDefault(file => file.Contains(item));
                if (foundFile != null)
                {
                    if (fileToItemsMap.ContainsKey(foundFile))
                    {
                        fileToItemsMap[foundFile].Add(item);
                    }
                    else
                    {
                        fileToItemsMap[foundFile] = new List<string> { item };
                    }
                }
            }

            return fileToItemsMap;
        }
        public static void SaveCoordinator(string action)
        {
            switch (action)
            {
                case "1":
                    List<string> csFiles = GetCsFiles();

                    List<string> classNames = CodeExtractor.ExtractClassNames(csFiles);
                    List<string> functionNames = CodeExtractor.ExtractFunctionNames(csFiles);

                    string folderPath = GetSolutionPath();

                    SaveFunctionNamesToFile(functionNames, folderPath);
                    SaveClassNamesToFile(classNames, folderPath);

                    // Combine class names and function names into a single list
                    List<string> combinedItems = new List<string>();
                    foreach (string file in csFiles)
                    {
                        string fileContent = File.ReadAllText(file);
                        SyntaxTree tree = CSharpSyntaxTree.ParseText(fileContent);
                        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

                        var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
                        foreach (var classDeclaration in classDeclarations)
                        {
                            string className = classDeclaration.Identifier.ValueText;
                            if (classNames.Contains(className))
                            {
                                combinedItems.Add(className);
                            }

                            var methodDeclarations = classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>();
                            foreach (var methodDeclaration in methodDeclarations)
                            {
                                string functionName = methodDeclaration.Identifier.ValueText;
                                if (functionNames.Contains(functionName))
                                {
                                    combinedItems.Add("\t" + functionName);
                                }
                            }
                        }
                    }

                    // Set combined items to the clipboard
                    ClipboardManager.SetTextToClipboard(combinedItems);

                    break;

                default:
                    Console.WriteLine("Error: Unrecognized action.");
                    break;
            }
        }
    }
}