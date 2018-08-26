using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Recipients
{

    [DisplayName("Add Certified Delivery")]
    public sealed class AddCertifiedDelivery : AddRecipientBase
    {
        protected override void Execute(CodeActivityContext context)
        {

            Initialize(context);
            CertifiedDelivery certifiedDelivery = new CertifiedDelivery(name, email, routingOrder);
            AddRecipient(context, certifiedDelivery);
        }
    }
}
