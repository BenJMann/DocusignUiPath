using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs
{
    public abstract class AddDisplayItemTab : AddConstDisplayTab
    {
        [Category("Formatting")]
        public InArgument<int> Width{ get; set; }
        [Category("Input")]
        public InArgument<string> Value{ get; set; }

        public int width;
        public string value;

        protected new virtual void Initialize(CodeActivityContext context)
        {
            InitializeDelegate(context);

            width = Width.Get(context);
            value = Value.Get(context);
        }
        protected void InitializeDelegate(CodeActivityContext context)
        {
            base.Initialize(context);

        }
    }
}
