using System;
using System.Activities;
using System.ComponentModel;
using System.Windows.Markup;
using System.IO;
using Docusign.DocusignTypes;

namespace Docusign.Tabs
{
    public abstract class AddTabBase : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Document")]
        public InArgument<Document> Document { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Signer")]
        public InArgument<Recipient> Signer { get; set; }

        [Category("Position Relative")]
        [RequiredArgument]
        [OverloadGroup("RelativeSignature")]
        [DefaultValue(null)]
        [DisplayName("Anchor Text")]
        [Description("Text to find in the document")]
        public InArgument<string> AnchorText { get; set; }

        [Category("Position Relative")]
        [RequiredArgument]
        [OverloadGroup("RelativeSignature")]
        [DependsOn("AnchorText")]
        [DefaultValue(null)]
        [DisplayName("Offset X")]
        [Description("Relative to Anchor Text")]
        public InArgument<int> OffsetX { get; set; }

        [Category("Position Relative")]
        [RequiredArgument]
        [OverloadGroup("RelativeSignature")]
        [DependsOn("AnchorText")]
        [DefaultValue(null)]
        [DisplayName("Offset Y")]
        [Description("Relative to Anchor Text")]
        public InArgument<int> OffsetY { get; set; }


        [Category("Position Absolute")]
        [RequiredArgument]
        [OverloadGroup("AbsoluteSignature")]
        [DefaultValue(null)]
        [DisplayName("Position X")]
        [Description("Absolute X position")]
        public InArgument<int> PositionX { get; set; }

        [Category("Position Absolute")]
        [RequiredArgument]
        [OverloadGroup("AbsoluteSignature")]
        [DependsOn("SignatureXPosition")]
        [DefaultValue(null)]
        [DisplayName("Position Y")]
        [Description("Absolute Y position")]
        public InArgument<int> PositionY { get; set; }

        [Category("Metadata")]
        [DisplayName("Tab Label")]
        [Description("For use with Formula Tab")]
        public InArgument<string> TabLabel { get; set; }
        [Category("MetaData")]
        [Description("Hover Display")]
        [DisplayName("Tool Tip")]
        public InArgument<string> ToolTip { get; set; }

        [Browsable(false)]
        private InArgument<int> PageNumberImplementation { get; set; } = 1;

        [Category("Input")]
        [RequiredArgument]
        [Description("Page of Document to display Tab")]
        [DisplayName("Page Number")]
        public InArgument<int> PageNumber
        {
            get
            {
                return PageNumberImplementation;
            }
            set
            {
                PageNumberImplementation = value;
            }
        }

        protected Document doc;
        protected Recipient rec;

        protected int sigX, sigY, offsetX, offsetY, pageNumber;
        protected string anchorText, tabLabel, toolTip;

        protected void Initialize(CodeActivityContext context)
        {
            doc = Document.Get(context);
            rec = Signer.Get(context);
            if (rec.RecipientType != "Signer") throw new ArgumentException("Only Signers can have tabs added to them, not other Recipient types");

            sigX = PositionX.Get(context);
            sigY = PositionY.Get(context);

            anchorText = AnchorText.Get(context);
            offsetX = OffsetX.Get(context);
            offsetY = OffsetY.Get(context);

            tabLabel = TabLabel.Get(context);
            toolTip = ToolTip.Get(context);
            pageNumber = PageNumber.Get(context);

            if (anchorText != null && anchorText != "" && Path.GetExtension(doc.filename) != ".pdf")
            {
                throw new FormatException("Can only use relative positioning on .pdf files");
            }
        }
        protected void AddTabToRecipient(Tab tab)
        {
            Signer signer;
            if (rec.RecipientType == "Signer")
            {
                signer = (Signer)rec;
                signer.AddTab(tab);
            }
            else
            {
                throw new ArgumentException("Cannot add tabs to recipient type " + rec.RecipientType);
            }
        }
    }
}
