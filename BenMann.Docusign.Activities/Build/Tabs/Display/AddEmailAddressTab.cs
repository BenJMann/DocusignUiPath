using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.Display
{
    [DisplayName("Add Email Address Tab")]
    public sealed class AddEmailAddressTab : AddConstDisplayTab
    {
        [Category("Input")]
        public InArgument<string> Value { get; set; }
        public string value;

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            EmailAddressTab emailAddressTab;

            if (anchorText != null)
                emailAddressTab = new EmailAddressTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, value);
            else
                emailAddressTab = new EmailAddressTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, value);

            AddTabToRecipient(emailAddressTab);
        }
    }
}
