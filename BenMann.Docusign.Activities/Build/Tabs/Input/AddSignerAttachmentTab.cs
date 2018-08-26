using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Signer Attachment Tab")]
    public sealed class AddSignerAttachmentTab : AddTabBase
    {
        public bool Required { get; set; }
        [DisplayName("Scale Value")]
        [Description("Size of element")]
        public InArgument<int> ScaleValue { get; set; }
        public int scaleValue;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            SignerAttachmentTab signerAttachmentTab;
            scaleValue = ScaleValue.Get(context);

            if (anchorText != null)
                signerAttachmentTab = new SignerAttachmentTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue, !Required);
            else
                signerAttachmentTab = new SignerAttachmentTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue, !Required);

            AddTabToRecipient(signerAttachmentTab);
        }
    }
}
