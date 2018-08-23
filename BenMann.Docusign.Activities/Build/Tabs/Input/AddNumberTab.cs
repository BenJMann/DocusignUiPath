using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Input
{
    [DisplayName("Add Number Tab")]
    public sealed class AddNumberTab : AddDisplayItemTab
    {
        public new InArgument<float> Value { get; set; }
        public new float value;

        protected override void Initialize(CodeActivityContext context)
        {
            base.InitializeDelegate(context);
            width = Width.Get(context);
            value = Value.Get(context);
        }
        protected override void Execute(CodeActivityContext context)
        {

            Initialize(context);
            NumberTab numberTab;

            if (anchorText != null)
                numberTab = new NumberTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);
            else
                numberTab = new NumberTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);

            AddTabToRecipient(numberTab);
        }
    }
}
