using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Input
{
    [DisplayName("Add SSN Tab")]
    public sealed class AddSSNTab : AddDisplayItemTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            SSNTab sSNTab;

            if (anchorText != null)
                sSNTab = new SSNTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);
            else
                sSNTab = new SSNTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);

            AddTabToRecipient(sSNTab);
        }
    }
}
