using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Recipients
{

    [DisplayName("Add Signer")]
    public sealed class AddSigner : AddRecipientBase
    {
        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            Signer signer = new Signer(name, email, routingOrder);
            AddRecipient(context, signer);
        }
    }
}
