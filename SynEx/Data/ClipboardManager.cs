using System.Windows;

namespace SynEx.Data
{
    public class ClipboardManager
    {
        public static void SetTextToClipboard(string text)
        {
            // Set the given text to the clipboard
            Clipboard.SetText(text);

            // Show a message box to indicate that the text has been copied to the clipboard
            System.Windows.MessageBox.Show("The extracted function names have been copied to the clipboard.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}