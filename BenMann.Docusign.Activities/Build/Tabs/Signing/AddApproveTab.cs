using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Signing
{
    [DisplayName("Add Approve Tab")]
    public sealed class AddApproveTab : AddButtonTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            ApproveTab approveTab;

            if (anchorText != null)
                approveTab = new ApproveTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, buttonText);
            else
                approveTab = new ApproveTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, buttonText);

            AddTabToRecipient(approveTab);
        }
    }
}
