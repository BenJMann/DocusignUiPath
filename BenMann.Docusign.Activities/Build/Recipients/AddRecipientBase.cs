using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Recipients
{

    public abstract class AddRecipientBase : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<Envelope> Envelope { get; set; }
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Recipient Name")]
        public InArgument<string> Name { get; set; }
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Recipient Email")]
        public InArgument<string> Email { get; set; }
        [Category("Input")]

        [Browsable(false)]
        private InArgument<int> RoutingOrderImplementation = 1;

        [DisplayName("Routing Order")]
        [RequiredArgument]
        [Description("Define the order recipients receive the envelope")]
        public InArgument<int> RoutingOrder
        {
            get
            {
                return RoutingOrderImplementation;
            }
            set
            {
                RoutingOrderImplementation = value;
            }
        }

        [Category("Output")]
        [Description("Created Recipient")]
        public OutArgument<Recipient> Recipient { get; set; }

        protected string name, email;
        protected int routingOrder;
        protected Envelope env;

        protected void Initialize(CodeActivityContext context)
        {
            name = Name.Get(context);
            email = Email.Get(context);
            routingOrder = RoutingOrder.Get(context);

            env = Envelope.Get(context);
        }
        protected void AddRecipient(CodeActivityContext context, Recipient recipient)
        {
            env.AddRecipient(recipient);
            Recipient.Set(context, recipient);
        }

    }
}
