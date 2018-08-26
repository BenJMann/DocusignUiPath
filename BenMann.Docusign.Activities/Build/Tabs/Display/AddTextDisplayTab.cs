using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Display
{
    [DisplayName("Add Text Tab")]
    public sealed class AddTextDisplayTab : AddBigDisplayItemTab
    {
	public bool Shared {get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            TextDisplayTab textTab;

            if (anchorText != null)
                textTab = new TextDisplayTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, height, Shared);
            else
                textTab = new TextDisplayTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, height, Shared);

            AddTabToRecipient(textTab);
        }
    }
}
