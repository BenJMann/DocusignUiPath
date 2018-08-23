using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Input
{
    [DisplayName("Add Email Tab")]
    public sealed class AddEmailTab : AddDisplayItemTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            EmailTab emailTab;

            if (anchorText != null)
                emailTab = new EmailTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);
            else
                emailTab = new EmailTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);

            AddTabToRecipient(emailTab);
        }
    }
}
