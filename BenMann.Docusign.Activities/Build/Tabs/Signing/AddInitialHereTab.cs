using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Signing
{
    [DisplayName("Add Initial Here Tab")]
    public sealed class AddInitialHereTab : AddTabBase
    {
        public InArgument<int> ScaleValue { get; set; }
        public int scaleValue;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            InitialHereTab initialHereTab;
            scaleValue = ScaleValue.Get(context);

            if (anchorText != null)
                initialHereTab = new InitialHereTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue);
            else
                initialHereTab = new InitialHereTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, scaleValue);

            AddTabToRecipient(initialHereTab);
        }
    }
}
