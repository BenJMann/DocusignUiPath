using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Number Tab")]
    public sealed class AddNumberTab : AddDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }
        [Description("Default Value")]
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
                numberTab = new NumberTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);
            else
                numberTab = new NumberTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);

            AddTabToRecipient(numberTab);
        }
    }
}
