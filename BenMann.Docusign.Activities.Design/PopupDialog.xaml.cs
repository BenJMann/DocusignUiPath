using System;
using System.Windows;
using System.Windows.Navigation;

namespace BenMann.Docusign.Activities.Design
{
    public partial class InputDialogSample : Window
    {
        public InputDialogSample()
        {
            InitializeComponent();

            projectLink.RequestNavigate += Navigate;
            instructionLink.RequestNavigate += Navigate;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Navigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
/*
 *                 
                if (inputDialog.ShowDialog() == true)
                    SigPositionButton.Content = inputDialog.Answer;
*/
