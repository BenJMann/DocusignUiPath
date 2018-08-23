using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Input
{
    [DisplayName("Add Date Tab")]
    public sealed class AddDateTab : AddDisplayItemTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            DateTab dateTab;

            if (anchorText != null)
                dateTab = new DateTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);
            else
                dateTab = new DateTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);

            AddTabToRecipient(dateTab);
        }
    }
}
