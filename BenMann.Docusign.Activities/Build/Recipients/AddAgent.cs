using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Recipients
{

    [DisplayName("Add Agent")]
    public sealed class AddAgent : AddRecipientBase
    {
        protected override void Execute(CodeActivityContext context)
        {

            Initialize(context);
            Agent agent = new Agent(name, email, routingOrder);
            AddRecipient(context, agent);
        }
    }
}
