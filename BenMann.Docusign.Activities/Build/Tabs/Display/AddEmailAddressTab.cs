using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Display
{
    [DisplayName("Add Email Address Tab")]
    public sealed class AddEmailAddressTab : AddConstDisplayTab
    {

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            EmailAddressTab emailAddressTab;

            if (anchorText != null)
                emailAddressTab = new EmailAddressTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, null);
            else
                emailAddressTab = new EmailAddressTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, null);

            AddTabToRecipient(emailAddressTab);
        }
    }
}
