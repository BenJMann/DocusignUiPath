using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Display
{
    [DisplayName("Add Title Tab")]
    public sealed class AddTitleTab : AddDisplayItemTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            TitleTab titleTab;

            if (anchorText != null)
                titleTab = new TitleTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);
            else
                titleTab = new TitleTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);

            AddTabToRecipient(titleTab);
        }
    }
}
