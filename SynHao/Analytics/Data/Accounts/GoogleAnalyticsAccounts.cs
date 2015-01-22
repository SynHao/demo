using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Data.Accounts
{
    public class GoogleAnalyticsAccounts
    {
        [JsonProperty("id")]
        public string AccountId { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("selfLink")]
        public string SelfLink { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        public PermissionsData Permissions { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("childLink")]
        public ChildLinkData ChildLink { get; set; }
    }
}
