using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    public class Settings
    {
        private Settings() { }

        private static Settings _instance;
        public static Settings Instance
        {
            get { return _instance == null ? (_instance = new Settings()) : _instance; }
        }
        public bool AutoEscapeFilter = true;
        public uint RequestTimeout = 10000;
        public string ProxyAddress = string.Empty;
        public uint ProxyPort;
        public bool UseProxy = false;
        public string ProxyUsername = string.Empty;
        public string ProxyPassword = string.Empty;
        public string ClientId = "587309872788-g295nn514r7jf2k79g8108rp6cr08hor.apps.googleusercontent.com";
        public string ClientSecret = "uRt6iXDwVly2mZY_fsOi4jF4";

        public System.Xml.XmlDocument MetricsXml;

        public System.Xml.XmlDocument DimensionsXml;
    }
}
