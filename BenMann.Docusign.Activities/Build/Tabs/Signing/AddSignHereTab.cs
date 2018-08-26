using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Signing
{
    [DisplayName("Add Sign Here Tab")]
    public sealed class AddSignHereTab : AddTabBase
    {
        public bool Required { get; set; } = true;
        [DisplayName("Scale Value")]
        [Description("Size of Element")]
        public InArgument<int> ScaleValue { get; set; }
        public int scaleValue;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            SignHereTab signHereTab;
            scaleValue = ScaleValue.Get(context);

            if (anchorText != null)
                signHereTab = new SignHereTab(anchorText, offsetX, offsetY-21, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue, !Required);
            else
                signHereTab = new SignHereTab(sigX, sigY-21, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue, !Required);

            AddTabToRecipient(signHereTab);
        }
    }
}
