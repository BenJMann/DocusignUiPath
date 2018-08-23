using BenMann.Docusign.DocusignTypes;
using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Recipients
{

    public abstract class AddRecipientBase : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<object> Envelope { get; set; }
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Name { get; set; }
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Email { get; set; }
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Routing Order")]
        public InArgument<int> RoutingOrder { get; set; }

        [Category("Output")]
        public OutArgument<object> Recipient { get; set; }

        protected string name, email;
        protected int routingOrder;
        protected Envelope env;

        protected void Initialize(CodeActivityContext context)
        {
            name = Name.Get(context);
            email = Email.Get(context);
            routingOrder = RoutingOrder.Get(context);

            env = (Envelope)Envelope.Get(context);
        }
        protected void AddRecipient(CodeActivityContext context, Recipient recipient)
        {
            env.AddRecipient(recipient);
            Recipient.Set(context, recipient);
        }

    }
}
