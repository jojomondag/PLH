using System.Windows;
using System.Windows.Controls;
using SynEx.Data;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using SynEx.Logic;
using System.IO;
using System.Threading.Tasks;

namespace SynEx
{
    public partial class SynExMainWindowControl : UserControl
    {
        public SynExMainWindowControl()
        {
            this.InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Button1ClickAsync();
        }
        private async Task Button1ClickAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            // Extract the function and class names
            CodeExtractor.ExtractFunctionNames();
            CodeExtractor.ExtractClassNames();
            CodeExtractor.ExtractClassAndFunctionNames();

            // Get the current date and time as a formatted string
            string dateTimeString = DataManager.GetCurrentDateTimeString();

            // Get the solution path and create the folder for the extracted syntax
            string solutionPath = DataManager.GetSolutionPath();
            string folderPath = Path.Combine(solutionPath, "ExtractedSyntax", dateTimeString);
            Directory.CreateDirectory(folderPath);

            // Get the file path for the extracted function names
            string functionFilePath = Path.Combine(folderPath, "functionNames.txt");

            // Read the extracted function names from the file
            string functionNames = File.ReadAllText(functionFilePath);

            // Get the file path for the extracted class names
            string classFilePath = Path.Combine(folderPath, "classNames.txt");

            // Read the extracted class and function names from the file
            string classNames = File.ReadAllText(classFilePath);

            // Get the file path for the extracted function names
            string ClassAndFunctionNamesFilePath = Path.Combine(folderPath, "ClassAndFunctionNames.txt");

            // Read the extracted function names from the file
            string ClassAndFunctionNamesNames = File.ReadAllText(ClassAndFunctionNamesFilePath);

            // Copy the extracted class and function names to the clipboard and show a success message
            ClipboardManager.SetTextToClipboard(ClassAndFunctionNamesNames);
        }
    }
}
