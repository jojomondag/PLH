﻿#pragma checksum "..\..\mainWindowControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "60DAF58A9618CA7E4415C21CCDB6C9EE9A75B75424118EFF7BD68E08888DD98D"
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
using System.Windows.Forms.Integration;
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
        
        
        #line 8 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SynEx.SynExMainWindowControl MyToolWindow;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectFolder;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract1Butt;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract2Butt;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract3Butt;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Extract4Butt;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\mainWindowControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExtractFolderStructureTreeButton;
        
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
            System.Uri resourceLocater = new System.Uri("/SynEx;component/mainwindowcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\mainWindowControl.xaml"
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
            
            #line 9 "..\..\mainWindowControl.xaml"
            this.MyToolWindow.Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SelectFolder = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\mainWindowControl.xaml"
            this.SelectFolder.Click += new System.Windows.RoutedEventHandler(this.SelectFolderClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Extract1Butt = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\mainWindowControl.xaml"
            this.Extract1Butt.Click += new System.Windows.RoutedEventHandler(this.Extract1Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Extract2Butt = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\mainWindowControl.xaml"
            this.Extract2Butt.Click += new System.Windows.RoutedEventHandler(this.Extract2Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Extract3Butt = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\mainWindowControl.xaml"
            this.Extract3Butt.Click += new System.Windows.RoutedEventHandler(this.Extract3Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Extract4Butt = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\mainWindowControl.xaml"
            this.Extract4Butt.Click += new System.Windows.RoutedEventHandler(this.Extract4Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ExtractFolderStructureTreeButton = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\mainWindowControl.xaml"
            this.ExtractFolderStructureTreeButton.Click += new System.Windows.RoutedEventHandler(this.ExtractFolderStructureClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

