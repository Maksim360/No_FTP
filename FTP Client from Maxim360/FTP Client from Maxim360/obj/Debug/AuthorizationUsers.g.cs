﻿#pragma checksum "..\..\AuthorizationUsers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4DB175E67E7137F55C40AC55032217AD9244C4A9381BB95CC0030A9CDF372C7A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using FTP_Client_from_Maxim360;
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


namespace FTP_Client_from_Maxim360 {
    
    
    /// <summary>
    /// AuthorizationUsers
    /// </summary>
    public partial class AuthorizationUsers : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\AuthorizationUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Login;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\AuthorizationUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\AuthorizationUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AuthorizationBttn;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\AuthorizationUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox AutoAuthorization;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\AuthorizationUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SavePassword;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\AuthorizationUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button IsReg;
        
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
            System.Uri resourceLocater = new System.Uri("/FTP Client from Maxim360;component/authorizationusers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AuthorizationUsers.xaml"
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
            this.Login = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.AuthorizationBttn = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\AuthorizationUsers.xaml"
            this.AuthorizationBttn.Click += new System.Windows.RoutedEventHandler(this.AuthorizationBtnn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AutoAuthorization = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.SavePassword = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.IsReg = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\AuthorizationUsers.xaml"
            this.IsReg.Click += new System.Windows.RoutedEventHandler(this.IsReg_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

