using System.ComponentModel.Design;
using System;
using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;
using SynEx.Helpers;

namespace SynEx
{
    internal sealed class SynExMainWindowCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new("a2a86f81-8cba-4fca-93c0-46965a98b8c1");

        private readonly AsyncPackage package;

        private SynExMainWindowCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }
        public static SynExMainWindowCommand Instance
        {
            get;
            private set;
        }
        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new SynExMainWindowCommand(package, commandService);
        }
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            await ExceptionHelper.TryCatchAsync(ExecuteAsync);
        }
        public async Task ExecuteAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            ToolWindowPane window = await this.package.ShowToolWindowAsync(typeof(SynExMainWindow), 0, true, this.package.DisposalToken);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }
        }
    }
}