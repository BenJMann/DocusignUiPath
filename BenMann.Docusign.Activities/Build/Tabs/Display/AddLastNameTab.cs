using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Display
{
    [DisplayName("Add Last Name Tab")]
    public sealed class AddLastNameTab : AddConstDisplayTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            LastNameTab lastNameTab;

            if (anchorText != null)
                lastNameTab = new LastNameTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);
            else
                lastNameTab = new LastNameTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);

            AddTabToRecipient(lastNameTab);
        }
    }
}
