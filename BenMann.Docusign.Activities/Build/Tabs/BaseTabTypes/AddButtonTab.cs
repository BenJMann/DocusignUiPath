using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs
{
    public abstract class AddButtonTab : AddConstDisplayTab
    {
        [Category("Size")]
        [DisplayName("Width")]
        [Description("Width")]
        public InArgument<int> Width { get; set; }
        [Category("Input")]
        [DisplayName("Button Text")]
        [Description("Text Displayed on Button")]
        public InArgument<string> ButtonText { get; set; }

        public int width;
        public string buttonText;

        protected new void Initialize(CodeActivityContext context)
        {
            base.Initialize(context);

            width = Width.Get(context);
            buttonText = ButtonText.Get(context);
        }
    }
}
