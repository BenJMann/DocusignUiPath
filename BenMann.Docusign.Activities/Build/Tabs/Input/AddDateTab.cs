using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Date Tab")]
    public sealed class AddDateTab : AddDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            DateTab dateTab;

            if (anchorText != null)
                dateTab = new DateTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);
            else
                dateTab = new DateTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);

            AddTabToRecipient(dateTab);
        }
    }
}
