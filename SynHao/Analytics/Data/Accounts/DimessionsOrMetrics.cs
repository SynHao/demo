using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics.Data.Accounts
{
    [Serializable]
    public class DimessionsOrMetrics
    {
        public string DataType { get; set; }

        public string UiName { get; set; }

        public string Id { get; set; }

    }
}
