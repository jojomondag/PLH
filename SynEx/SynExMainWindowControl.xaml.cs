using System.Windows;
using System.Windows.Controls;
using SynEx.Data;

namespace SynEx
{
    public partial class SynExMainWindowControl : UserControl
    {
        public SynExMainWindowControl()
        {
            this.InitializeComponent();
        }
        private void ClassFuncClick(object sender, RoutedEventArgs e)
        {
            ClassFuncClickAsync();
        }
        private async Task ClassFuncClickAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DataManager.SaveCoordinator("1");
        }
    }
}
