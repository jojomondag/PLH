using SynEx.Logic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using SynEx.Logic;

namespace SynEx
{
    public partial class SynExMainWindowControl : UserControl
    {
        public SynExMainWindowControl()
        {
            this.InitializeComponent();
        }
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CodeExtractor.ExtractFunctionNames();
        }
    }
}