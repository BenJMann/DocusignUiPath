using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Recipients
{

    [DisplayName("Add Editor")]
    public sealed class AddEditor : AddRecipientBase
    {
        protected override void Execute(CodeActivityContext context)
        {

            Initialize(context);
            Editor editor = new Editor(name, email, routingOrder);
            AddRecipient(context, editor);
        }
    }
}
