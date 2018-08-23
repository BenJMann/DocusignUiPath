using System;
using System.Activities;
using System.Net.Http;
using System.ComponentModel;
using BenMann.Docusign.DocusignTypes;

namespace BenMann.Docusign.Activities.Envelopes
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
        public InArgument<object> Envelope { get; set; }

        public Envelope env;

        Action SendEnvelopeDelegate;


        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            LoadAuthentication(context);

            env = (Envelope)Envelope.Get(context);

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
            EnvelopeSentResponse resObj = response.GetData<EnvelopeSentResponse>();
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            SendEnvelopeDelegate.EndInvoke(result);
        }
    }
}
