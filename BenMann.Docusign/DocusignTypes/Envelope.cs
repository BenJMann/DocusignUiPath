using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Docusign.DocusignTypes
{
    public class Envelope
    {
        public string status;
        public string emailSubject;
        public List<Document> documents;
        public Recipients recipients;

        public Envelope(string subject)
        {
            this.emailSubject = subject;
            this.status = "sent";

            documents = new List<Document>();
            recipients = new Recipients();
        }

        public Envelope(
            string subject,
            List<Document> documents,
            List<Recipient> recipients_in
            )
        {
            this.emailSubject = subject;
            this.status = "sent";

            recipients = new Recipients();
            recipients.Add(recipients_in);

            this.documents = documents;
        }
        public void AddDocument(Document document)
        {
            this.documents.Add(document);
        }
        public void AddRecipient(Recipient recipient)
        {
            this.recipients.Add(recipient);
        }
    }

    public enum FromToStatusTypes
    {
        Any, Voided, Changed, Created, Deleted, Sent, Delivered, Signed, Completed, Declined, TimedOut, Processing
    }
    public class EnvelopeResponse
    {
        public List<EnvelopeInfo> envelopes;
    }

    public class EnvelopeInfo
    {
        public EnvelopeInfo()
        {

        }
        public string envelopeId;
        public string status;
        public string statusChangedDateTime;
    }



}











