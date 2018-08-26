using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.GUI
{
    [DisplayName("Add Radio Group Tab")]
    public sealed class AddRadioGroupTab : AddDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }

        [Browsable(false)]
        public override InArgument<string> Value { get; set; }

        [Category("Input")]
        [DisplayName("Radio Items")]
        [Description("Comma Seperated names of Radio Items")]
        [RequiredArgument]
        public InArgument<string> RadioItems { get; set; }
        
        [Browsable(false)]
        public InArgument<int> RadioSpacingImplementation = 20;
        [Category("Input")]
        [DisplayName("Radio Y Spacing")]
        [Description("Y Spacing of Radio Items")]
        [RequiredArgument]
        public InArgument<int> RadioSpacing
        {
            get
            {
                return RadioSpacingImplementation;
            }
            set
            {
                RadioSpacingImplementation = value;
            }
        }

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
                generateRadioLabels(radioLabels, anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, spacing, radioItemCount);
                radioGroupTab = new RadioGroupTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, spacing, radioItemCount, Required, Shared);
            }
            else
            {
                generateRadioLabels(radioLabels, sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, spacing, radioItemCount);
                radioGroupTab = new RadioGroupTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, spacing, radioItemCount, Required, Shared);
            }

            AddTabToRecipient(radioGroupTab);
        }
        private void generateRadioLabels(string[] radioLabels, int sigX, int sigY, int documentId, int pageNumber, string toolTip, string tabLabel, bool bold, bool italic, bool underline, string font, string fontColor, int fontSize, int spacing, int radioItemCount)
        {
            for (var i = 0; i < radioLabels.Length; i++)
            {
                string radioLabel = radioLabels[i];
                var trimmed_item = radioLabel.Trim();
                TextDisplayTab textTab = new TextDisplayTab(sigX + 20, (sigY + spacing * i) - 5, documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, 0, trimmed_item, 0, Shared);
                AddTabToRecipient(textTab);
            }
        }
        private void generateRadioLabels(string[] radioLabels, string anchorText, int offsetX, int offsetY, int documentId, int pageNumber, string toolTip, string tabLabel, bool bold, bool italic, bool underline, string font, string fontColor, int fontSize, int spacing, int radioItemCount)
        {
            for (var i = 0; i < radioLabels.Length; i++)
            {
                string radioLabel = radioLabels[i];
                var trimmed_item = radioLabel.Trim();
                TextTab textTab = new TextTab(anchorText, offsetX + 20, (offsetY + spacing * i) - 5, documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, 0, trimmed_item, 0, Shared);
                AddTabToRecipient(textTab);
            }
        }
    }
}
