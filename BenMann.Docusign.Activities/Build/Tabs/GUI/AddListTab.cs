using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.GUI
{
    [DisplayName("Add List Tab")]
    public sealed class AddListTab : AddConstDisplayTab
    {
        [Category("Input")]
        public InArgument<string> ListItems { get; set; }
        public string listItems;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            ListTab listTab;
            listItems = ListItems.Get(context);

            if (anchorText != null)
                listTab = new ListTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, listItems);
            else
                listTab = new ListTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, listItems);

            AddTabToRecipient(listTab);
        }
    }
}
