using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Display
{
    [DisplayName("Add First Name Tab")]
    public sealed class AddFirstNameTab : AddConstDisplayTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            FirstNameTab firstNameTab;

            if (anchorText != null)
                firstNameTab = new FirstNameTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);
            else
                firstNameTab = new FirstNameTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);

            AddTabToRecipient(firstNameTab);
        }
    }
}
