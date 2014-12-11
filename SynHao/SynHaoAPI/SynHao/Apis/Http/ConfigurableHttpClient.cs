using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Http
{
    public class ConfigurableHttpClient:HttpClient
    {
        public ConfigurableMessageHandler MessageHandler { get; private set; }
    }
}
