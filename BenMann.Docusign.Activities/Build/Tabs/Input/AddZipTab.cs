using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Input
{
    [DisplayName("Add Zip Tab")]
    public sealed class AddZipTab : AddDisplayItemTab
    {
        public bool Shared { get; set; }
        public bool Required { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            ZipTab zipTab;

            if (anchorText != null)
                zipTab = new ZipTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);
            else
                zipTab = new ZipTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, Required, Shared);

            AddTabToRecipient(zipTab);
        }
    }
}
