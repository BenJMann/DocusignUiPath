using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Recipients
{

    [DisplayName("Add Intermediary")]
    public sealed class AddIntermediary : AddRecipientBase
    {
        protected override void Execute(CodeActivityContext context)
        {

            Initialize(context);
            Intermediary intermediary = new Intermediary(name, email, routingOrder);
            AddRecipient(context, intermediary);
        }
    }
}
