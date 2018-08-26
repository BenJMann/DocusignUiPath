using System.Activities;
using System.ComponentModel;

namespace Docusign.Tabs
{
    public abstract class AddBigDisplayItemTab : AddDisplayItemTab
    {
        [Category("Size")]
        [Description("Height")]
        [DisplayName("Height")]
        public InArgument<int> Height { get; set; }

        public int height;

        protected new void Initialize(CodeActivityContext context)
        {
            base.Initialize(context);
            height = Height.Get(context);
        }
    }
}
