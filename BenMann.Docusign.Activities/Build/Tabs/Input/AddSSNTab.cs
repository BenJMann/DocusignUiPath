using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add SSN Tab")]
    public sealed class AddSSNTab : AddDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            SSNTab ssnTab;

            if (anchorText != null)
                ssnTab = new SSNTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);
            else
                ssnTab = new SSNTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);

            AddTabToRecipient(ssnTab);
        }
    }
}
