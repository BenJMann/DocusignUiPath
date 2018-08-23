using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Display
{
    [DisplayName("Add Full Name Tab")]
    public sealed class AddFullNameTab : AddConstDisplayTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            FullNameTab fullNameTab;

            if (anchorText != null)
                fullNameTab = new FullNameTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);
            else
                fullNameTab = new FullNameTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);

            AddTabToRecipient(fullNameTab);
        }
    }
}