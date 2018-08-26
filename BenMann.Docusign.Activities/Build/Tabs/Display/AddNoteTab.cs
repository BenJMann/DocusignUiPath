using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.Display
{
    [DisplayName("Add Note Tab")]
    public sealed class AddNoteTab : AddBigDisplayItemTab
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            NoteTab noteTab;

            if (width == 0) width = 100;
            if (height == 0) height = 50;

            if (anchorText != null)
                noteTab = new NoteTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, height);
            else
                noteTab = new NoteTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, height);

            AddTabToRecipient(noteTab);
        }
    }
}
