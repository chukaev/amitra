using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Models
{
    public class Transfer
    {
        private JToken jToken;

        public Transfer(JToken jToken)
        {
            this.jToken = jToken;
        }

        public List<TransferPart> TransferParts { get; set; }
    }
}