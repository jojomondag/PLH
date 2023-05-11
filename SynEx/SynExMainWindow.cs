using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace SynEx
{
    [Guid("8d9bdffe-1d68-4ec3-89b7-ff5728f556bc")]
    public class SynExMainWindow : ToolWindowPane
    {
        public SynExMainWindow() : base(null)
        {
            this.Caption = "SynExMainWindow";

            this.Content = new SynExMainWindowControl();
        }
    }
}