using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Signing
{
    [DisplayName("Add Initial Here Tab")]
    public sealed class AddInitialHereTab : AddTabBase
    {
        public bool Required { get; set; } = true;
        [DisplayName("Scale Value")]
        [Description("Size of element")]
        public InArgument<int> ScaleValue { get; set; }
        public int scaleValue;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            InitialHereTab initialHereTab;
            scaleValue = ScaleValue.Get(context);

            if (anchorText != null)
                initialHereTab = new InitialHereTab(anchorText, offsetX, offsetY-12, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue, !Required);
            else
                initialHereTab = new InitialHereTab(sigX, sigY-12, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue, !Required);

            AddTabToRecipient(initialHereTab);
        }
    }
}
