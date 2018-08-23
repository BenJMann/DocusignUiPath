using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Display
{
    [DisplayName("Add EnvelopeID Tab")]
    public sealed class AddEnvelopeIDTab : AddConstDisplayTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            EnvelopeIDTab envelopeIDTab;

            if (anchorText != null)
                envelopeIDTab = new EnvelopeIDTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);
            else
                envelopeIDTab = new EnvelopeIDTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize);

            AddTabToRecipient(envelopeIDTab);
            
        }
    }
}
