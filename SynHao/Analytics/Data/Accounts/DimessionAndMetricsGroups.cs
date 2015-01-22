using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Data.Accounts
{
    [Serializable]
    public  class DimessionAndMetricsGroups
    {
        // public string Type { get; set; }

        public string Group { get; set; }

        public string DataType { get; set; }

        public string UiName { get; set; }

        public string Id { get; set; }

        public string Status { get; set; }
    }
}
