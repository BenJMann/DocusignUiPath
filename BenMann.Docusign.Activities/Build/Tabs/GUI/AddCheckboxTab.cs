using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.GUI
{
    [DisplayName("Add Checkbox Tab")]
    public sealed class AddCheckboxTab : AddTabBase
    {
	public bool Shared {get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            CheckboxTab checkboxTab;

            if (anchorText != null)
                checkboxTab = new CheckboxTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, Shared);
            else
                checkboxTab = new CheckboxTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, Shared);

            AddTabToRecipient(checkboxTab);
        }
    }
}
