using Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;
using Analytics;

namespace UI.Controls.CustomTreeView
{
    public class SizeViewModel : INotifyPropertyChanged
    {
        bool? _isChecked = false;
        SizeViewModel _parent;
        int maxSelections;

        public string Name { get; private set; }
        public string Value { get; private set; }
        public bool IsInitiallyExpanded { get; private set; }
        public List<SizeViewModel> Children { get; private set; }

        SizeViewModel(string name)
        {
            this.Name = name;
            this.Children = new List<SizeViewModel>();
        }

        public SizeViewModel(string name, string value)
        {
            this.Value = value;
            this.Name = name;
            this.Children = new List<SizeViewModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string p;
        private string paramValue;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        public int MaxSelections
        {
            get { return maxSelections; }
            set { maxSelections = value; }
        }

        private void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
                this.Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null)
            {
                _parent.VerifyCheckState();
            }
            this.OnPropertyChanged("IsChecked");
        }

        private void OnPropertyChanged(string p)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        private void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.Children.Count; i++)
            {
                bool? current = this.Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }

        public static List<SizeViewModel> CreateDimensions()
        {
            return LoadXML(SizeKeyType.Dimension);
        }

        public static List<SizeViewModel> CreateMetrics()
        {
             return LoadXML(SizeKeyType.Metric);
        }

        /*
        private static List<SizeViewModel> Load(SizeKeyType feedObjectType)
        {
            SizeViewModel sizes = new SizeViewModel(feedObjectType == SizeKeyType.Dimension ? "dimensions" : "metrics");
            string uri = "https://www.googleapis.com/analytics/v3/metadata/ga/columns";
            WebRequest request = HttpRequestFactory.Instance.CreateRequest(uri);
            request.Method = "GET";

        }*/

        private static List<SizeViewModel> LoadXML(SizeKeyType feedObjectType)
        {
            SizeViewModel sizes = new SizeViewModel(feedObjectType == SizeKeyType.Dimension ? "Dimensions" : "Metrics");
            try
            {
                XDocument xDocument = Analytics.Data.Query.GetSizeCollectionAsXML(feedObjectType);
                foreach (XElement element in xDocument.Root.Elements("Category"))
                {
                    SizeViewModel category = new SizeViewModel(element.Attribute("name").Value);

                    foreach (XElement subElement in element.Elements(feedObjectType == SizeKeyType.Dimension ? "Dimension" : "Metric"))
                    {
                        string paramValue = subElement.Attribute("value").Value;
                        SizeViewModel size = new SizeViewModel(subElement.Attribute("name").Value, paramValue);
                        category.Children.Add(size);
                    }
                    category.IsInitiallyExpanded = false;

                    sizes.Children.Add(category);
                }
                sizes.IsInitiallyExpanded = true;
                sizes.Initialize();
            }
            catch { }
            return new List<SizeViewModel> { sizes };
        }

        private void Initialize()
        {
            foreach (SizeViewModel child in this.Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }
    }
}
