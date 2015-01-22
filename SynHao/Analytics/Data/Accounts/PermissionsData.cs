using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Data.Accounts
{
    public class PermissionsData
    {
        [JsonProperty("effective")]
        public virtual IList<String> Effective { get; set; }
    }
}
