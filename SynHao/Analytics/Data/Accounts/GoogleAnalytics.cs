using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Data.Accounts
{
    public  class GoogleAnalytics
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        [JsonProperty("startIndex")]
        public int StartIndex { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("previousLink")]
        public string PreviousLink { get; set; }

        [JsonProperty("nextLink")]
        public string NextLink { get; set; }

        [JsonProperty("items")]
        public IList<GoogleAnalyticsAccounts> AccountList { get; set; }
    }
}
