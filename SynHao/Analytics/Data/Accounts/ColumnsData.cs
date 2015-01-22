using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Data.Accounts
{
    public  class ColumnsData
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string ETag { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }

        [JsonProperty("attributeNames")]
        public IList<string> AttributeNames { get; set; }

        [JsonProperty("items")]
        public IList<ColumnsItems> Items { get; set; }
    }
}
