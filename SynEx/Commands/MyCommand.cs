using System.ComponentModel.Design;

namespace SynEx
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await SynExMainWindowCommand.Instance.ExecuteAsync();
        }
    }
}