using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs
{
    public abstract class AddBigDisplayItemTab : AddDisplayItemTab
    {
        [Category("Formatting")]
        public InArgument<int> Height { get; set; }

        public int height;

        protected new void Initialize(CodeActivityContext context)
        {
            base.Initialize(context);
            height = Height.Get(context);
        }
    }
}
