using EnvDTE;
using SynEx.Data;
using SynEx.Helpers;
using System.Windows;

#pragma warning disable VSTHRD100 // Avoid async void methods
namespace SynEx
{
    public partial class SynExMainWindowControl
    {
        public SynExMainWindowControl()
        {
            InitializeComponent();
        }
        private async void SelectFolderClick(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                
            });
        }
        private async void Extract1Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>{
                await ExtractClickAsync("1");
            });
        }
        private async void Extract2Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("2");
            });
        }
        private async void Extract3Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("3");
            });
        }
        private async void Extract4Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("4");
            });
        }
        private async void ExtractFolderStructureClick(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                var joinableTaskFactory = ThreadHelper.JoinableTaskFactory;
                var extractor = new ExtractFolderStructure(joinableTaskFactory);
                await extractor.ExtractFilesAndFolderStructureTreeAsync();
            });
        }
        private async Task ExtractClickAsync(string action)
        {
            await DataManager.SaveCoordinatorAsync(action);
        }
    }
}
#pragma warning restore VSTHRD100 // Avoid async void methods