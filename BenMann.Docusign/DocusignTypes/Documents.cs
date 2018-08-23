using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docusign.Revamped.DocusignTypes
{
    public class Document
    {
        public int documentId;
        public string name;
        public string documentBase64;

        [JsonIgnore]
        private static int documentIndex = 1;

        [JsonIgnore]
        public string filename;

        public Document(string name, string filename, int index = -1)
        {
            if (index > 0)
            {
                documentIndex = index + 1;
                this.documentId = index;
            }
            else
            {
                this.documentId = documentIndex;
                documentIndex++;
            }
            this.name = name;
            this.filename = filename;
            Byte[] bytes = File.ReadAllBytes(filename);
            this.documentBase64 = Convert.ToBase64String(bytes);
        }
    }
}
