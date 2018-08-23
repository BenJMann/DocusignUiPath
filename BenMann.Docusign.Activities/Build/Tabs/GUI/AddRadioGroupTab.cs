using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.GUI
{
    [DisplayName("Add Radio Group Tab")]
    public sealed class AddRadioGroupTab : AddDisplayItemTab
    {
        [Category("Input")]
        public InArgument<string> RadioItems { get; set; }
        [Category("Input")]
        public InArgument<int> RadioSpacing { get; set; }

        public string radioItems;
        public int spacing;

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            RadioGroupTab radioGroupTab;

            radioItems = RadioItems.Get(context);
            spacing = RadioSpacing.Get(context);

            string[] radioLabels = radioItems.Split(',');
            int radioItemCount = radioLabels.Length;
            if (anchorText != null)
            {
                generateRadioLabels(radioLabels,  anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, spacing, radioItemCount);
                radioGroupTab = new RadioGroupTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, spacing, radioItemCount);
            }
            else
            {
                generateRadioLabels(radioLabels, sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, spacing, radioItemCount);
                radioGroupTab = new RadioGroupTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, spacing, radioItemCount);
            }

            AddTabToRecipient(radioGroupTab);
        }
        private void generateRadioLabels(string[] radioLabels, int sigX, int sigY, int documentId, int pageNumber, string toolTip, string tabLabel, bool bold, bool italic, bool underline, string font, string fontColor, int fontSize, int spacing, int radioItemCount)
        {
            for (var i=0; i<radioLabels.Length; i++)
            {
                string radioLabel = radioLabels[i];
                var trimmed_item = radioLabel.Trim();
                TextTab textTab = new TextTab(sigX+20, (sigY+spacing*i)-5, documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, 0, trimmed_item, 0);
                AddTabToRecipient(textTab);
            }
        }
        private void generateRadioLabels(string[] radioLabels, string anchorText, int offsetX, int offsetY, int documentId, int pageNumber, string toolTip, string tabLabel, bool bold, bool italic, bool underline, string font, string fontColor, int fontSize, int spacing, int radioItemCount)
        {
            for (var i= 0; i < radioLabels.Length; i++)
            {
                string radioLabel = radioLabels[i];
                var trimmed_item = radioLabel.Trim();
                TextTab textTab = new TextTab(anchorText, offsetX+20, (offsetY+spacing*i)-5, documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, 0, trimmed_item, 0);
                AddTabToRecipient(textTab);
            }
        }
    }
}
