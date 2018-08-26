using System;
using System.Activities;
using System.Net.Http;
using System.ComponentModel;
using BenMann.Docusign;
using Docusign.DocusignTypes;

namespace Docusign.Envelopes
{
    public class EnvelopeSentResponse
    {
        public string envelopeId;
        public string uri;
        public string statusDateTime;
        public string status;
    }

public sealed class SendEnvelope : DocusignActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<Envelope> Envelope { get; set; }

        [Category("Output")]
        [DisplayName("Envelope ID")]
        [Description("Envelope ID")]
        public OutArgument<string> EnvelopeID { get; set; }

        public Envelope env;
        EnvelopeSentResponse resObj;

        Action SendEnvelopeDelegate;


        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            LoadAuthentication(context);

            env = Envelope.Get(context);

            SendEnvelopeDelegate = new Action(_SendEnvelope);
            return SendEnvelopeDelegate.BeginInvoke(callback, state);
        }

        void _SendEnvelope()
        {
            DocusignResponse response = new DocusignResponse();
            SendRestRequest(response, HttpMethod.Post, "envelopes", env).Wait();
            if (response.IsError)
            {
                response.Throw();
            }
            resObj = response.GetData<EnvelopeSentResponse>();
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            SendEnvelopeDelegate.EndInvoke(result);
            EnvelopeID.Set(context, resObj.envelopeId);
        }
    }
}
