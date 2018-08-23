using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Recipients
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
