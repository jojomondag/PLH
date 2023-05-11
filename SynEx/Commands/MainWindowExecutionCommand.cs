using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;

namespace SynEx
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MainWindowExecutionCommand : BaseCommand<MainWindowExecutionCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await SynExMainWindowCommand.Instance.ExecuteAsync();
        }
    }
}