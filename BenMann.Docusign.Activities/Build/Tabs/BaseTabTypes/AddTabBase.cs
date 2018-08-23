using System;
using System.Activities;
using System.ComponentModel;
using System.Windows.Markup;
using System.IO;
using Docusign.Revamped.DocusignTypes;

namespace BenMann.Docusign.Activities.Tabs
{
    public abstract class AddTabBase : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<object> Document { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<object> Recipient { get; set; }

        [Category("Signature: Relative Anchor")]
        [RequiredArgument]
        [OverloadGroup("RelativeSignature")]
        [DefaultValue(null)]
        [DisplayName("Anchor Text")]
        public InArgument<string> AnchorText { get; set; }

        [Category("Signature: Relative Anchor")]
        [RequiredArgument]
        [OverloadGroup("RelativeSignature")]
        [DependsOn("AnchorText")]
        [DefaultValue(null)]
        [DisplayName("Offset X")]
        public InArgument<int> OffsetX { get; set; }

        [Category("Signature: Relative Anchor")]
        [RequiredArgument]
        [OverloadGroup("RelativeSignature")]
        [DependsOn("AnchorText")]
        [DefaultValue(null)]
        [DisplayName("Offset Y")]
        public InArgument<int> OffsetY { get; set; }


        [Category("Signature: Absolute Position")]
        [RequiredArgument]
        [OverloadGroup("AbsoluteSignature")]
        [DefaultValue(null)]
        [DisplayName("Position X")]
        public InArgument<int> PositionX { get; set; }

        [Category("Signature: Absolute Position")]
        [RequiredArgument]
        [OverloadGroup("AbsoluteSignature")]
        [DependsOn("SignatureXPosition")]
        [DefaultValue(null)]
        [DisplayName("Position Y")]
        public InArgument<int> PositionY { get; set; }

        [Category("Metadata")]
        public InArgument<string> TabLabel { get; set; }
        [Category("MetaData")]
        public InArgument<string> ToolTip { get; set; }
        [Category("Input")]
        public InArgument<int> PageNumber { get; set; }

        protected Document doc;
        protected Recipient rec;

        protected int sigX, sigY, offsetX, offsetY, pageNumber;
        protected string anchorText, tabLabel, toolTip;

        protected void Initialize(CodeActivityContext context)
        {
            doc = (Document)Document.Get(context);
            rec = (Recipient)Recipient.Get(context);

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
