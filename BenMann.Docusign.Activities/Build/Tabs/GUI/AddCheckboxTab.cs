using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.GUI
{
    [DisplayName("Add Checkbox Tab")]
    public sealed class AddCheckboxTab : AddTabBase
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            CheckboxTab checkboxTab;

            if (anchorText != null)
                checkboxTab = new CheckboxTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel);
            else
                checkboxTab = new CheckboxTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel);

            AddTabToRecipient(checkboxTab);
        }
    }
}
