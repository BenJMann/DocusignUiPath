using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Signing
{
    [DisplayName("Add Decline Tab")]
    public sealed class AddDeclineTab : AddButtonTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            DeclineTab declineTab;

            if (anchorText != null)
                declineTab = new DeclineTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, buttonText);
            else
                declineTab = new DeclineTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, buttonText);

            AddTabToRecipient(declineTab);
        }
    }
}
