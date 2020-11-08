﻿#pragma checksum "..\..\StartMenu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7FB32BE7E9A16856E446DF1DFD65249CAFB327BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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


namespace SeaBattle {
    
    
    /// <summary>
    /// StartMenu
    /// </summary>
    public partial class StartMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\StartMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gWrapper;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\StartMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bTwoPlayersMode;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\StartMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bAbout;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\StartMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bExit;
        
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
            System.Uri resourceLocater = new System.Uri("/SeaBattle;component/startmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StartMenu.xaml"
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
            this.gWrapper = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 22 "..\..\StartMenu.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AskSizeForTwoPlayersMode);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 27 "..\..\StartMenu.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ExitApplication);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 33 "..\..\StartMenu.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowAbout);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bTwoPlayersMode = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\StartMenu.xaml"
            this.bTwoPlayersMode.Click += new System.Windows.RoutedEventHandler(this.AskSizeForTwoPlayersMode);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bAbout = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\StartMenu.xaml"
            this.bAbout.Click += new System.Windows.RoutedEventHandler(this.ShowAbout);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bExit = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\StartMenu.xaml"
            this.bExit.Click += new System.Windows.RoutedEventHandler(this.ExitApplication);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

