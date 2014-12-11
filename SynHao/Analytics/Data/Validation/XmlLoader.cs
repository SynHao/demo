using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Analytics.Data.Validation
{
    public class XmlLoader
    {
        private List<XElement> dimCategories;
        private List<XElement> metCategories;

        public void Loader()
        {
            dimCategories = new List<XElement>();
            metCategories = new List<XElement>();

            // 加载所有的指标
            XDocument xDocumentMetric =
                XDocument.Load(
                System.Xml.XmlReader.Create(
                Assembly.GetExecutingAssembly().GetManifestResourceStream("Analytics.Data.General." +
                "Metrics.xml")));

            foreach (XElement element in xDocumentMetric.Root.Elements("Category"))
            {
                metCategories.Add(element);
            }

            // 加载所有维度
            XDocument xDocumentDimession =
                XDocument.Load(
                System.Xml.XmlReader.Create(
                Assembly.GetExecutingAssembly().GetManifestResourceStream("Analytics.Data.General." +
                "Dimensions.xml")));

            foreach (XElement element in xDocumentDimession.Elements("Category"))
            {
                dimCategories.Add(element);
            }
        }

        public List<XElement> MetCategories
        {
            get { return metCategories; }
            set { metCategories = value; }
        }

        public List<XElement> DimCategories
        {
            get { return dimCategories; }
            set { dimCategories = value; }
        }

        public static System.Xml.XmlDocument Dimensions
        {
            get
            {
                System.Xml.XmlDocument dimessions = new System.Xml.XmlDocument();
                dimessions.Load(System.Xml.XmlReader.Create(
                    System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Analytics.Data.General.Dimensions.xml")));
                return dimessions;
            }
        }

        public static System.Xml.XmlDocument Metrics
        {
            get
            {
                System.Xml.XmlDocument metrics = new System.Xml.XmlDocument();
                metrics.Load(System.Xml.XmlReader.Create(
                    System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Analytics.Data.General.Metrics.xml")));
                return metrics;
            }
        }
    }
}
