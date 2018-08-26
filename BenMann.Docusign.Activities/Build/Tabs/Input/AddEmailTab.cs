using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Email Tab")]
    public sealed class AddEmailTab : AddDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            EmailTab emailTab;

            if (anchorText != null)
                emailTab = new EmailTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);
            else
                emailTab = new EmailTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);

            AddTabToRecipient(emailTab);
        }
    }
}
