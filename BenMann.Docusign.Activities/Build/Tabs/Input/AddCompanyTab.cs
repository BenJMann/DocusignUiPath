using System.Activities;
using System.ComponentModel;
using Docusign.Revamped.DocusignTypes;

namespace BenMann.Docusign.Activities.Tabs.Input
{
    [DisplayName("Add Company Tab")]
    public sealed class AddCompanyTab : AddDisplayItemTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            CompanyTab companyTab;

            if (anchorText != null)
                companyTab = new CompanyTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);
            else
                companyTab = new CompanyTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value);

            AddTabToRecipient(companyTab);
        }
    }
}
