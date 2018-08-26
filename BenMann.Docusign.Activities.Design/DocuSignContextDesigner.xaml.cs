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

namespace BenMann.Docusign.Activities.Design
{
    // Interaction logic for DocuSignContextDesigner.xaml
    public partial class DocuSignContextDesigner
    {
        public DocuSignContextDesigner()
        {
            InitializeComponent();
            InfoIcon.Width = 16;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputDialogSample inputDialog = new InputDialogSample();
            inputDialog.ShowDialog();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoIcon.Width = 20;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoIcon.Width = 16;
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
