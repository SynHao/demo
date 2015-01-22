using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Data.Accounts
{
    public class ChildLinkData
    {
        [JsonProperty("href")]
        public virtual string Href { get; set; }

        [JsonProperty("type")]
        public virtual string Type { get; set; }
    }
}
