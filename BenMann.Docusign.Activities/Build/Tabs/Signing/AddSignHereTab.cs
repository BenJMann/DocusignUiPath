using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Signing
{
    [DisplayName("Add Sign Here Tab")]
    public sealed class AddSignHereTab : AddTabBase
    {
        public InArgument<int> ScaleValue { get; set; }
        public int scaleValue;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            SignHereTab signHereTab;
            scaleValue = ScaleValue.Get(context);

            if (anchorText != null)
                signHereTab = new SignHereTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue);
            else
                signHereTab = new SignHereTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue);

            AddTabToRecipient(signHereTab);
        }
    }
}
