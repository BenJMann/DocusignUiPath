using BenMann.Docusign;
using Docusign.DocusignTypes;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Windows.Markup;

namespace Docusign.Basic
{
    [DisplayName("Request Signature")]
    public class RequestSignature : DocusignActivity
    {
        [RequiredArgument]
        [Category("Input")]
        public InArgument<string> RecipientName { get; set; }
        [RequiredArgument]
        [Category("Input")]
        public InArgument<string> RecipientEmail { get; set; }
        [RequiredArgument]
        [Category("Input")]
        public InArgument<string> Subject { get; set; }
        [RequiredArgument]
        [Category("Input")]
        public InArgument<string> DocumentFilePath { get; set; }


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

        [Browsable(false)]
        public InArgument<string> SignatureMethodBase = "Relative Position";
        [Browsable(false)]
        public InArgument<string> SignatureMethod
        {
            get
            {
                return SignatureMethodBase ?? "Relative Position";
            }
            set
            {
                SignatureMethodBase = value;
            }
        }

        string recipientName;
        string recipientEmail;
        string subject;
        string documentFilePath;
        int sigX;
        int sigY;

        string anchorText;
        int offsetX;
        int offsetY;

        Action SendEnvelopeDelegate;

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            recipientName = RecipientName.Get(context);
            recipientEmail = RecipientEmail.Get(context);
            subject = Subject.Get(context);
            documentFilePath = DocumentFilePath.Get(context);
            sigX = PositionX.Get(context);
            sigY = PositionY.Get(context);

            anchorText = AnchorText.Get(context);
            offsetX = OffsetX.Get(context);
            offsetY = OffsetY.Get(context);

            if (anchorText != null && anchorText != "" && Path.GetExtension(documentFilePath) != ".pdf")
            {
                throw new FormatException("Can only use relative positioning on .pdf files");
            }

            LoadAuthentication(context);

            SendEnvelopeDelegate = new Action(SendEnvelope);
            return SendEnvelopeDelegate.BeginInvoke(callback, state);
        }

        void SendEnvelope()
        {
            int docId = 1;
            int recptId = 1;
            int routingOrder = 1;

            Document document = new Document(Path.GetFileName(documentFilePath), documentFilePath, docId);
            List<Document> documents = new List<Document>() { document };

            SignHereTab signHereTab;
            if (anchorText != null)
                signHereTab = new SignHereTab(anchorText, offsetX, offsetY, docId, 1, null, null, 1, false);
            else
                signHereTab = new SignHereTab(sigX, sigY, docId, 1, null, null, 1, false);

            List<Tab> tabs = new List<Tab>() { signHereTab };
            Signer r1signer = new Signer(recipientName, recipientEmail, routingOrder, tabs, recptId);

            List<Recipient> recipients = new List<Recipient>() { r1signer };

            Envelope env = new Envelope(subject, documents, recipients);

            DocusignResponse response = new DocusignResponse();
            SendRestRequest(response, HttpMethod.Post, "envelopes", env).Wait();
            if (response.IsError)
            {
                response.Throw();
            }
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            SendEnvelopeDelegate.EndInvoke(result);
        }
    }
}
