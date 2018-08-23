using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs
{
    public abstract class AddButtonTab : AddConstDisplayTab
    {
        [Category("Formatting")]
        public InArgument<int> Width { get; set; }
        [Category("Input")]
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
