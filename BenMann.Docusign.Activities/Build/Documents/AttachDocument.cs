using BenMann.Docusign.DocusignTypes;
using Docusign.Revamped.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Documents
{
    public sealed class AttachDocument : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<object> Envelope { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Name { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Filename { get; set; }

        [Category("Output")]
        public OutArgument<object> Document { get; set;  }

        protected override void Execute(CodeActivityContext context)
        {
            Envelope env = (Envelope)Envelope.Get(context);
            string name = Name.Get(context);
            string filename = Filename.Get(context);

            Document doc = new Document(name, filename);

            env.AddDocument(doc);

            Document.Set(context, doc);
        }
    }
}
