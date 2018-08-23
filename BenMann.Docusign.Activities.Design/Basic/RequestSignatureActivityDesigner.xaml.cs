using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace BenMann.Docusign.Activities.Design
{
    // Interaction logic for RequestSignatureActivityDesigner.xaml
    public partial class RequestSignatureActivityDesigner
    {
        public List<string> MahNames
        {
            get
            {
                return new List<string>
                {
                    "Relative Position", "Absolute Position"
                };
            }
            set { }
        }
        public RequestSignatureActivityDesigner()
        {
            InitializeComponent();
            this.DataContext = this;
            SigComboBox.SelectedValue = "Absolute Position";

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
                ModelProperty property = this.ModelItem.Properties["DocumentFilePath"];
                property.SetValue(new InArgument<string>(Utils.TrimFilePath(_openFileDialog.FileName, Directory.GetCurrentDirectory())));
            }
        }

        private void SetAbsolute()
        {
            AbsPositioning.Visibility = Visibility.Visible;
            RelPositioning.Visibility = Visibility.Collapsed;

            ModelProperty p1 = this.ModelItem.Properties["AnchorText"];
            p1.SetValue(null);
            ModelProperty p2 = this.ModelItem.Properties["OffsetX"];
            p2.SetValue(null);
            ModelProperty p3 = this.ModelItem.Properties["OffsetY"];
            p3.SetValue(null);
        }
        private void SetRelative()
        {
            AbsPositioning.Visibility = Visibility.Collapsed;
            RelPositioning.Visibility = Visibility.Visible;

            ModelProperty p1 = this.ModelItem.Properties["PositionX"];
            p1.SetValue(null);
            ModelProperty p2 = this.ModelItem.Properties["PositionY"];
            p2.SetValue(null);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var state = (string)SigComboBox.SelectedValue;
            if (state == "Relative Position") SetRelative();
            else if (state == "Absolute Position") SetAbsolute();
        }
    }
  
}
