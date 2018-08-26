using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Title Tab")]
    public sealed class AddTitleTab : AddDisplayItemTab
    {
        public bool Required { get; set; }
        [Browsable(false)]
        public override InArgument<string> Value { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            TitleTab titleTab;

            if (anchorText != null)
                titleTab = new TitleTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required);
            else
                titleTab = new TitleTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required);

            AddTabToRecipient(titleTab);
        }
    }
}
