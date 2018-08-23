using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Recipients
{

    [DisplayName("Add Carbon Copy")]
    public sealed class AddCarbonCopy : AddRecipientBase
    {
        protected override void Execute(CodeActivityContext context)
        {

            Initialize(context);
            CarbonCopy carbonCopy = new CarbonCopy(name, email, routingOrder);
            AddRecipient(context, carbonCopy);
        }
    }
}
