using System.Windows;
using System.Windows.Controls;
using SynEx.Data;

namespace SynEx
{
    public partial class SynExMainWindowControl : System.Windows.Controls.UserControl
    {
        public SynExMainWindowControl()
        {
            InitializeComponent();
        }

        private async void Extract1Click(object sender, RoutedEventArgs e)
        {
            await ExtractClickAsync("1");
        }

        private async void Extract2Click(object sender, RoutedEventArgs e)
        {
            await ExtractClickAsync("2");
        }

        private async void Extract3Click(object sender, RoutedEventArgs e)
        {
            await ExtractClickAsync("3");
        }

        private async void Extract4Click(object sender, RoutedEventArgs e)
        {
            await ExtractClickAsync("4");
        }
        private async void ExtractFolderStructureClick(object sender, RoutedEventArgs e)
        {
            await ExtractFolderStructure.ExtractFilesAndFolderStructureTree();
        }

        private async Task ExtractClickAsync(string action)
        {
            await DataManager.SaveCoordinatorAsync(action);
        }
    }
}