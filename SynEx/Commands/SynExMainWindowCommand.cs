using System.ComponentModel.Design;
using System.Windows;
using System;
using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

namespace SynEx
{
    internal sealed class SynExMainWindowCommand
    {
        public const int CommandId = 256;

        public static readonly Guid CommandSet = new("924599ce-e991-459e-becd-2d2b29abd238");

        private readonly AsyncPackage package;

        private SynExMainWindowCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
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

#pragma warning disable VSTHRD100 // Avoid async void methods

        private async void Execute(object sender, EventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                await ExecuteAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Oops! Something went wrong: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task ExecuteAsync()
        {
            await this.package.JoinableTaskFactory.RunAsync(async delegate
            {
                ToolWindowPane window = await this.package.ShowToolWindowAsync(typeof(SynExMainWindow), 0, true, this.package.DisposalToken);
                if ((null == window) || (null == window.Frame))
                {
                    throw new NotSupportedException("Cannot create tool window");
                }
            });
        }
    }
}