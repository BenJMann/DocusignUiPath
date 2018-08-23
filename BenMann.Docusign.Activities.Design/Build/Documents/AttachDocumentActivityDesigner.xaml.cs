using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.IO;
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
    // Interaction logic for AttachDocumentActivityDesigner.xaml
    public partial class AttachDocumentActivityDesigner
    {
        public AttachDocumentActivityDesigner()
        {
            InitializeComponent();
        }

        private void Button_LoadDocument(object sender, RoutedEventArgs e)
        {

            OpenFileDialog _openFileDialog = new OpenFileDialog
            {
                Title = "Open Document",
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            if (_openFileDialog.ShowDialog() == true)
            {
                ModelProperty property = this.ModelItem.Properties["Filename"];
                property.SetValue(new InArgument<string>(Utils.TrimFilePath(_openFileDialog.FileName, Directory.GetCurrentDirectory())));
            }
        }
    }
}
