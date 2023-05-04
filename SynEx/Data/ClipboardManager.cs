using System.Collections.Generic;
using System.Text;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace SynEx.Data
{
    public class ClipboardManager
    {
        public static void SetTextToClipboard(List<string> items)
        {
            // Convert the list of items to a single string, separated by newlines
            StringBuilder sb = new StringBuilder();
            foreach (string item in items)
            {
                sb.AppendLine(item);
            }

            // Set the combined text to the clipboard
            Clipboard.SetText(sb.ToString());

            // Show a message box to indicate that the text has been copied to the clipboard
            MessageBox.Show("The extracted items have been copied to the clipboard.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
