using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Text Tab")]
    public sealed class AddTextInputTab : AddBigDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            TextInputTab textTab;

            if (anchorText != null)
                textTab = new TextInputTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, height, Required, Shared);
            else
                textTab = new TextInputTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, height, Required, Shared);

            AddTabToRecipient(textTab);
        }
    }
}
