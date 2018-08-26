using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs
{
    public abstract class AddDisplayItemTab : AddConstDisplayTab
    {
        [Category("Size")]
        [Description("Width")]
        public InArgument<int> Width{ get; set; }

        [Category("Input")]
        [Description("Default Value")]
        public virtual InArgument<string> Value { get; set; }

        public int width;
        public string value;

        protected new virtual void Initialize(CodeActivityContext context)
        {
            InitializeDelegate(context);

            width = Width.Get(context);
            value = Value.Get(context);
            //value = null; 
        }
        protected void InitializeDelegate(CodeActivityContext context)
        {
            base.Initialize(context);

        }
    }
}
