using BenMann.Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Envelopes
{

    public sealed class CreateEnvelope : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Subject { get; set; }

        [Category("Output")]
        public OutArgument<object> Envelope { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Envelope.Set(context, new Envelope(Subject.Get(context)));
        }
    }
}
