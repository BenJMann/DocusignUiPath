using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs.GUI
{
    [DisplayName("Add Formula Tab")]
    public sealed class AddFormulaTab : AddDisplayItemTab
    {
        [Category("Input")]
        public InArgument<string> Formula { get; set; }

        public new InArgument<string> Value = null;
        public string formula;
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            FormulaTab formulaTab;
            formula = Formula.Get(context);

            if (anchorText != null)
                formulaTab = new FormulaTab(anchorText, offsetX, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, formula);
            else
                formulaTab = new FormulaTab(sigX, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, formula);

            AddTabToRecipient(formulaTab);
        }
    }
}
