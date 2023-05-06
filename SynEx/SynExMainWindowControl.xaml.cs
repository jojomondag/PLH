using System.Windows;
using SynEx.Data;

namespace SynEx
{
    public partial class SynExMainWindowControl : System.Windows.Controls.UserControl
    {
        public SynExMainWindowControl()
        {
            InitializeComponent();
        }
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void Extract1Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                await ExtractClickAsync("1");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Oops! Something went wrong: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void Extract2Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                await ExtractClickAsync("2");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Oops! Something went wrong: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void Extract3Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                await ExtractClickAsync("3");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Oops! Something went wrong: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void Extract4Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                await ExtractClickAsync("4");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Oops! Something went wrong: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void ExtractFolderStructureClick(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                var joinableTaskFactory = ThreadHelper.JoinableTaskFactory;
                var extractor = new ExtractFolderStructure(joinableTaskFactory);
                await extractor.ExtractFilesAndFolderStructureTreeAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Oops! Something went wrong: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task ExtractClickAsync(string action)
        {
            await DataManager.SaveCoordinatorAsync(action);
        }
    }
}