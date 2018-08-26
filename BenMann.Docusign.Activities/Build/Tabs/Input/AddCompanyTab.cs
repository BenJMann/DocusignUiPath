using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Company Tab")]
    public sealed class AddCompanyTab : AddDisplayItemTab
    {
        public bool Required { get; set; }
        [Browsable(false)]
        public override InArgument<string> Value { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            CompanyTab companyTab;

            if (anchorText != null)
                companyTab = new CompanyTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required);
            else
                companyTab = new CompanyTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required);

            AddTabToRecipient(companyTab);
        }
    }
}
