﻿#pragma checksum "..\..\SynExMainWindowControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E9B204C3951E25258D0342CCFF6F8FECFF20D36E3A576EEBE493AE678B269389"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SynEx {
    
    
    /// <summary>
    /// SynExMainWindowControl
    /// </summary>
    public partial class SynExMainWindowControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\SynExMainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SynEx.SynExMainWindowControl MyToolWindow;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\SynExMainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract1Butt;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SynExMainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract2Butt;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\SynExMainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract3Butt;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\SynExMainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract4Butt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SynEx;component/synexmainwindowcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SynExMainWindowControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MyToolWindow = ((SynEx.SynExMainWindowControl)(target));
            return;
            case 2:
            this.Extract1Butt = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\SynExMainWindowControl.xaml"
            this.Extract1Butt.Click += new System.Windows.RoutedEventHandler(this.Extract1Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Extract2Butt = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\SynExMainWindowControl.xaml"
            this.Extract2Butt.Click += new System.Windows.RoutedEventHandler(this.Extract2Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Extract3Butt = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\SynExMainWindowControl.xaml"
            this.Extract3Butt.Click += new System.Windows.RoutedEventHandler(this.Extract3Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Extract4Butt = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\SynExMainWindowControl.xaml"
            this.Extract4Butt.Click += new System.Windows.RoutedEventHandler(this.Extract4Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 48 "..\..\SynExMainWindowControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExtractFolderStructureClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

