using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docusign.Revamped.DocusignTypes
{

    public class Recipients
    {
        public List<Agent> agents;
        public List<CarbonCopy> carbonCopies;
        public List<CertifiedDelivery> certifiedDeliveries;
        public List<Editor> editors;
        public List<Intermediary> intermediaries;
        public List<Signer> signers;

        public void Add(List<Recipient> recipients)
        {
            foreach (Recipient recipient in recipients)
            {
                Add(recipient);
            }
        }
        public void Add(Recipient recipient)
        {
            if (recipient.RecipientType == "Agent")
            {
                if (agents == null) agents = new List<Agent>();
                agents.Add((Agent)recipient);
            }
            if (recipient.RecipientType == "CarbonCopy")
            {
                if (carbonCopies == null) carbonCopies = new List<CarbonCopy>();
                carbonCopies.Add((CarbonCopy)recipient);
            }
            if (recipient.RecipientType == "CertifiedDelivery")
            {
                if (certifiedDeliveries == null) certifiedDeliveries = new List<CertifiedDelivery>();
                certifiedDeliveries.Add((CertifiedDelivery)recipient);
            }
            if (recipient.RecipientType == "Editor")
            {
                if (editors == null) editors = new List<Editor>();
                editors.Add((Editor)recipient);
            }
            if (recipient.RecipientType == "Intermediary")
            {
                if (intermediaries == null) intermediaries = new List<Intermediary>();
                intermediaries.Add((Intermediary)recipient);
            }
            if (recipient.RecipientType == "Signer")
            {
                if (signers == null) signers = new List<Signer>();
                signers.Add((Signer)recipient);
            }
        }
    }
    public abstract class Recipient
    {
        public int recipientId;
        public string name;
        public string email;
        public int routingOrder;

        [JsonIgnore]
        public abstract string RecipientType { get; }

        [JsonIgnore]
        private static int recipientIndex = 1;

        public Recipient(string name, string email, int routingOrder, int index)
        {
            if (index > 0)
            {
                recipientIndex = index + 1;
                this.recipientId = index;
            }
            else
            {
                this.recipientId = recipientIndex;
                recipientIndex++;
            }
            this.name = name;
            this.email = email;
            this.routingOrder = routingOrder;
        }
    }
    public class Signer : Recipient
    {
        public Tabs tabs;

        [JsonIgnore]
        public override string RecipientType
        {
            get { return "Signer"; }
        }

        public Signer(string name, string email, int routingOrder, List<Tab> tabs = null, int index = -1)
            : base(name, email, routingOrder, index)
        {
            if (tabs != null)
            {
                this.tabs = new Tabs();
                this.tabs.Add(tabs);
            }
        }
        public void AddTab(Tab tab)
        {
            if (this.tabs == null) this.tabs = new Tabs();
            this.tabs.Add(tab);
        }
    }

    public class Agent : Recipient
    {
        [JsonIgnore]
        public override string RecipientType
        {
            get { return "Signer"; }
        }

        public Agent(string name, string email, int routingOrder, int index = -1)
            : base(name, email, routingOrder, index)
        {
        }
    }
    public class CarbonCopy : Recipient
    {
        [JsonIgnore]
        public override string RecipientType
        {
            get { return "CarbonCopy"; }
        }

        public CarbonCopy(string name, string email, int routingOrder, int index = -1)
            : base(name, email, routingOrder, index)
        {
        }
    }
    public class CertifiedDelivery : Recipient
    {
        [JsonIgnore]
        public override string RecipientType
        {
            get { return "CertifiedDelivery"; }
        }

        public CertifiedDelivery(string name, string email, int routingOrder, int index = -1)
            : base(name, email, routingOrder, index)
        {
        }
    }
    public class Editor : Recipient
    {
        [JsonIgnore]
        public override string RecipientType
        {
            get { return "Editor"; }
        }

        public Editor(string name, string email, int routingOrder, int index = -1)
            : base(name, email, routingOrder, index)
        {
        }
    }
    public class Intermediary : Recipient
    {
        [JsonIgnore]
        public override string RecipientType
        {
            get { return "Intemediary"; }
        }

        public Intermediary(string name, string email, int routingOrder, int index = -1)
            : base(name, email, routingOrder, index)
        {
        }
    }


}
