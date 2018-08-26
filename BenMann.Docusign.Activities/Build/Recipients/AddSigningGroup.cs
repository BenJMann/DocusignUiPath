using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Recipients
{
    /*
    [DisplayName("Add Signing Group")]
    public class AddSigningGroup : AddRecipientBase
    {
        [Browsable(false)]
        public new InArgument<string> Name { get; set; } = "";
        [Browsable(false)]
        public new InArgument<string> Email { get; set; } = "";

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Signing Group Name")]
        public InArgument<string> SigningGroupName { get; set; }

        private string signingGroupName;

        protected override void Execute(CodeActivityContext context)
        {
            Initialize(context);
            int signingGroupId = int.Parse(signingGroupName);
            Signer signer = new SigningGroup(routingOrder, signingGroupId);
            AddRecipient(context, signer);
        }

        protected new void Initialize(CodeActivityContext context)
        {
            name = null;
            email = null;
            routingOrder = RoutingOrder.Get(context);
            signingGroupName = SigningGroupName.Get(context);

            env = Envelope.Get(context);
        }
        protected new void AddRecipient(CodeActivityContext context, Recipient recipient)
        {
            env.AddRecipient(recipient);
            Recipient.Set(context, recipient);
        }
    }
    */
}
