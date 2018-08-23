using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Input
{
    [DisplayName("Add Signer Attachment Tab")]
    public sealed class AddSignerAttachmentTab : AddTabBase
    {
        public InArgument<int> ScaleValue { get; set; }
        public int scaleValue;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            SignerAttachmentTab signerAttachmentTab;
            scaleValue = ScaleValue.Get(context);

            if (anchorText != null)
                signerAttachmentTab = new SignerAttachmentTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue);
            else
                signerAttachmentTab = new SignerAttachmentTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue);

            AddTabToRecipient(signerAttachmentTab);
        }
    }
}
