using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.GUI
{
    [DisplayName("Add List Tab")]
    public sealed class AddListTab : AddConstDisplayTab
    {
	public bool Shared {get; set; }
        public bool Required { get; set; }

        [Category("Input")]
        [DisplayName("List Items")]
        [Description("Comma Seperated names of List Items")]
        public InArgument<string> ListItems { get; set; }
        public string listItems;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            ListTab listTab;
            listItems = ListItems.Get(context);

            if (anchorText != null)
                listTab = new ListTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, listItems, Required, Shared);
            else
                listTab = new ListTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, listItems, Required, Shared);

            AddTabToRecipient(listTab);
        }
    }
}
