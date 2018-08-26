using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Envelopes
{

    public sealed class CreateEnvelope : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Subject Line")]
        public InArgument<string> Subject { get; set; }

        [Category("Output")]
        public OutArgument<Envelope> Envelope { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Envelope.Set(context, new Envelope(Subject.Get(context)));
        }
    }
}
