using Docusign.DocusignTypes;
using System.Activities;
using System.ComponentModel;

namespace Docusign.Documents
{
    public sealed class AttachDocument : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<Envelope> Envelope { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Name { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Filename { get; set; }

        [Category("Output")]
        [Description("Created Document")]
        public OutArgument<Document> Document { get; set;  }

        protected override void Execute(CodeActivityContext context)
        {
            Envelope env = Envelope.Get(context);
            string name = Name.Get(context);
            string filename = Filename.Get(context);

            Document doc = new Document(name, filename);

            env.AddDocument(doc);

            Document.Set(context, doc);
        }
    }
}
