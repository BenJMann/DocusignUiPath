using System;
using System.Activities;
using System.Net.Http;
using System.ComponentModel;
using BenMann.Docusign;
using Docusign.DocusignTypes;
using System.Collections.Generic;
using Microsoft.VisualBasic.Activities;

namespace Docusign.Envelopes
{
    public sealed class ListEnvelopes : DocusignActivity
    {
        Dictionary<string, string> Query = new Dictionary<string, string>();
        EnvelopeResponse resObj;
        Action GetEnvelopesDelegate;
        EnvelopeInfo envInfo = new EnvelopeInfo();

        [Category("Input")]
        [DisplayName("Envelope IDs")]
        [Description("Comma Seperated list of Envelope IDs")]
        public InArgument<string> EnvelopeIDs { get; set; }
        [Category("Input")]
        [DisplayName("From Date")]
        [Description("Date to begin search")]
        public InArgument<string> FromDate { get; set; }
        [Category("Input")]
        [DisplayName("To Date")]
        [Description("Date to end search")]
        public InArgument<string> ToDate { get; set; }
        [Category("Input")]
        [DisplayName("From-To Status")]
        public FromToStatusTypes FromToStatus { get; set; }
        [Category("Input")]
        [Description("Comma Seperated list of statuses")]
        public InArgument<string> Status { get; set; }

        [Category("Output")]
        [DisplayName("Envelope List")]
        [Description("List of Envelopes returned")]
        public OutArgument<EnvelopeInfoList> EnvelopeList { get; set; }


        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            LoadAuthentication(context);

            if (EnvelopeIDs.Get(context) != null)
                Query["envelope_ids"] = EnvelopeIDs.Get(context).Replace(" ", "");
            Query["from_date"] = FromDate.Get(context);
            Query["to_date"] = ToDate.Get(context);
            if (FromToStatus.ToString() != null && FromToStatus.ToString() != "Any")
                Query["from_to_status"] = FromToStatus.ToString();
            if (Status.Get(context) != null)
                Query["status"] = Status.Get(context).Replace(" ", "");


            GetEnvelopesDelegate = new Action(_ListEnvelopes);
            return GetEnvelopesDelegate.BeginInvoke(callback, state);
        }

        void _ListEnvelopes()
        {
            DocusignResponse response = new DocusignResponse();
            SendRestRequest(response, HttpMethod.Get, "envelopes", null, Query).Wait();
            if (response.IsError)
            {
                response.Throw();
            }
            resObj = response.GetData<EnvelopeResponse>();
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            GetEnvelopesDelegate.EndInvoke(result);
            EnvelopeInfoList envelopeInfoList = new EnvelopeInfoList();
            foreach (EnvelopeInfo envInf in resObj.envelopes)
                envelopeInfoList.Add(envInf);
            EnvelopeList.Set(context, envelopeInfoList);
        }
    }
}
