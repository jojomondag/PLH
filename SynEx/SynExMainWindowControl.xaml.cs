using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using SynEx.Data;
using EnvDTE;

namespace SynEx
{
    public partial class SynExMainWindowControl : UserControl
    {
        public SynExMainWindowControl()
        {
            this.InitializeComponent();
        }
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            // Get an instance of the DTE object, which implements the IServiceProvider interface
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));

            // Pass the DTE object to the GetSolutionPath() method of the DataManager class to get the solution path
            string solutionPath = DataManager.GetSolutionPath(dte);

            // Show a message box with the solution path
            System.Windows.MessageBox.Show($"Solution Path: {solutionPath}", "Solution Path", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}