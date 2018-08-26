using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BenMann.Docusign.Activities.Design.Authentication
{
    // Interaction logic for AuthenticateActivityDesigner.xaml
    public partial class AuthenticateActivityDesigner
    {
        public List<string> AuthenticationMethods
        {
            get
            {
                return new List<string>
                {
                    "Manual", "Insecure", "Secure"
                };
            }
            set { }
        }
        public List<string> Browsers
        {
            get
            {
                return new List<string>
                {
                    "IE", "Firefox", "Chrome"
                };
            }
            set { }
        }
        public AuthenticateActivityDesigner()
        {
            InitializeComponent();
            //AuthMethodComboBox.SelectedIndex = 0;
            //BrowserComboBox.SelectedIndex = 0;
            SetManual();
        }
        private void SetManual()
        {
            ManualAuth.Visibility = Visibility.Visible;
            AutomaticAuth.Visibility = Visibility.Collapsed;
            SecureAuth.Visibility = Visibility.Collapsed;
            InsecureAuth.Visibility = Visibility.Collapsed;
        }
        private void SetSecure()
        {
            ManualAuth.Visibility = Visibility.Collapsed;
            AutomaticAuth.Visibility = Visibility.Visible;
            SecureAuth.Visibility = Visibility.Visible;
            InsecureAuth.Visibility = Visibility.Collapsed;
        }
        private void SetInsecure()
        {
            ManualAuth.Visibility = Visibility.Collapsed;
            AutomaticAuth.Visibility = Visibility.Visible;
            SecureAuth.Visibility = Visibility.Collapsed;
            InsecureAuth.Visibility = Visibility.Visible;
        }
        private void AuthMethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var state = AuthMethodComboBox.SelectedValue.ToString();
            if (state == "Secure") SetSecure();
            else if (state == "Insecure") SetInsecure();
            else if (state == "Manual") SetManual();
        }
    }
}
