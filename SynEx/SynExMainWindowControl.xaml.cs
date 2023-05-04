using System.Windows;
using System.Windows.Controls;
using SynEx.Data;
using System.Threading.Tasks;

namespace SynEx
{
    public partial class SynExMainWindowControl : UserControl
    {
        public SynExMainWindowControl()
        {
            InitializeComponent();
        }

        private async void Extract1Click(object sender, RoutedEventArgs e)
        {
            await Extract1ClickAsync();
        }

        private async Task Extract1ClickAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DataManager.SaveCoordinator("1");
        }

        private async void Extract2Click(object sender, RoutedEventArgs e)
        {
            await Extract2ClickAsync();
        }

        private async Task Extract2ClickAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DataManager.SaveCoordinator("2");
        }

        private async void Extract3Click(object sender, RoutedEventArgs e)
        {
            await Extract3ClickAsync();
        }

        private async Task Extract3ClickAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DataManager.SaveCoordinator("3");
        }

        private async void Extract4Click(object sender, RoutedEventArgs e)
        {
            await Extract4ClickAsync();
        }

        private async Task Extract4ClickAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DataManager.SaveCoordinator("4");
        }
    }
}
