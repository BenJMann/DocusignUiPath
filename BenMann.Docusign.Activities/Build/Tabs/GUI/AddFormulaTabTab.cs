using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs.GUI
{
    [DisplayName("Add Formula Tab")]
    public sealed class AddFormulaTab : AddDisplayItemTab
    {
	public bool Shared {get; set; }
        [Category("Input")]
        [Description("Use TabLabels in [] brackets. Only Number Tabs are supported in calculations")]
        public InArgument<string> Formula { get; set; }
        public string formula;

        [Browsable(false)]
        public override InArgument<string> Value { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            FormulaTab formulaTab;
            formula = Formula.Get(context);

            if (anchorText != null)
                formulaTab = new FormulaTab(anchorText, offsetX+12, offsetY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, formula, Shared);
            else
                formulaTab = new FormulaTab(sigX+12, sigY, doc.documentId, pageNumber, toolTip, tabLabel, bold, italic, underline, font, fontColor, fontSize, width, value, formula, Shared);

            AddTabToRecipient(formulaTab);
        }
    }
}
