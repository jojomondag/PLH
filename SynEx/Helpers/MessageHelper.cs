using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace SynEx.Helpers
{
    //Message helper class to display messages in a good and proper way for my Extention.
    internal static class MessageHelper
    {
        public static void ShowInfo(string message)
        {
            ShowMessage(message, OLEMSGICON.OLEMSGICON_INFO);
        }
        public static void ShowWarning(string message)
        {
            ShowMessage(message, OLEMSGICON.OLEMSGICON_WARNING);
        }
        public static void ShowError(string message)
        {
            ShowMessage(message, OLEMSGICON.OLEMSGICON_CRITICAL);
        }
        public static void ShowMessage(string message, OLEMSGICON icon)
        {
            var uiShell = (IVsUIShell)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result;

            uiShell.ShowMessageBox(
                dwCompRole: 0,
                rclsidComp: ref clsid,
                pszTitle: "SynEx",
                pszText: message,
                pszHelpFile: null,
                dwHelpContextID: 0,
                msgbtn: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                msgdefbtn: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                msgicon: icon,
                fSysAlert: 0,
                pnResult: out result);
        }
    }
}