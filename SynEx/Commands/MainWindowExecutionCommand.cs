using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

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
