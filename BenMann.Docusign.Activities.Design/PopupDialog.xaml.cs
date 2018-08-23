using System;
using System.Windows;

namespace BenMann.Docusign.Activities.Design
{
    public partial class InputDialogSample : Window
    {
        public InputDialogSample(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            lblQuestion.Content = question;
            txtAnswer.Text = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }
    }
}
/*
 *                 InputDialogSample inputDialog = new InputDialogSample("Enter yo namE", "NIAA");
                if (inputDialog.ShowDialog() == true)
                    SigPositionButton.Content = inputDialog.Answer;
*/