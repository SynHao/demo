using Analytics.Authorization;
using Analytics.Data;
using Analytics.Data.Accounts;
using Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using UI.Controls.CustomTreeView;

namespace UI
{
    /// <summary>
    /// QueryBuilder.xaml 的交互逻辑
    /// </summary>
    public partial class QueryBuilder : Window
    {
        DoubleAnimation _animatePropertyWidth;
        DoubleAnimation _animatePropertyHeight;

        Query _query;
        UserAccount _currentUserAccount;
        AccountManager _accM;
        GoogleAnalytics _gaAccounts;
        GoogleAnalyticsView _gaView;
        ColumnsData _columnsData;
        Dictionary<string, List<DimessionsOrMetrics>> _dimessions;
        Dictionary<string , List<DimessionsOrMetrics>> _metrics;
        SizeKeyType activeSize;

        public delegate void QueryComplete(Query query);
        public event QueryComplete queryComplete;

        private bool queryNotCompleted = false;
        private bool dimensionsMayHaveChanged = false;

        public enum ListType { Dim, Met, Fil, Sort, View };

        public UserAccount CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set { _currentUserAccount = value; }
        }

        private List<RadioButton> TimeSpanBoxesColl
        {
            get
            {
                return new RadioButton[] { todayCheckBox, yesterdayCheckBox, weekCheckBox, weekCheckBoxAnglosax, monthCheckBox, 
                    quarterCheckBox, yearCheckBox, periodNotSpecifiedCheckBox, thisYearBox }.Where(p => p != null).ToList<RadioButton>();
            }
        }

        public QueryBuilder()
        {
            InitializeComponent();
        }

        public QueryBuilder(UserAccount userAccount, Query query)
        {
            double pixeHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (pixeHeight < 800)
            {
                SnapsToDevicePixels = true;
            }
            InitializeComponent();

            if (pixeHeight > 800)
                SizeToContent = System.Windows.SizeToContent.WidthAndHeight;

            this._query = query != null ? query : new Query();
            _currentUserAccount = userAccount;
            InitializeForm();
            SetTimePeriod(query);
            if (query.Ids.Count > 0)
            {
                PreselectView(userAccount, query.Ids.First());
            }
        }

        private void PreselectView(UserAccount userAccount, Item view)
        {
            string viewId = view.Key;
            var entries = userAccount.Entrys.Where(e => e.ViewId == "ga:" + viewId);
            var entry = entries.SingleOrDefault();
            if (entry == null)
            {
                return;
            }

            int i = 0;
            foreach (var item in comboBoxAccount.Items)
            {
                var eitem = item as Entry;
                if (eitem == null)
                {
                    continue;
                }
                if (eitem.AccountId == entry.AccountId)
                {
                    comboBoxAccount.SelectedIndex = i;
                    break;
                }
                ++i;
            }

            if (comboBoxView.Items.Count > 0)
            {
                i = 0;
                foreach (var item in comboBoxView.Items)
                {
                    var eitem = item as Entry;
                    if (eitem == null)
                    {
                        continue;
                    }
                    if (eitem.ViewId == entry.ViewId)
                    {
                        comboBoxView.SelectedIndex = i;
                        break;
                    }
                    ++i;
                }
            }
        }

        private void SetTimePeriod(Query query)
        {
            if (!(query.Metrics.Values.Count.Equals(0)) && (_query.TimePeriod != TimePeriod.PeriodNotSpecified))
            {
                setCalendarToDefault();
                foreach (RadioButton itBox in TimeSpanBoxesColl)
                    itBox.IsChecked = query.TimePeriod.ToString() == itBox.Tag.ToString();

                timeSpanTab.IsSelected = true;
            }
        }

        private void setCalendarToDefault()
        {
            startDateCalendar.SelectedDate = DateTime.Now;
            startDateCalendar.DisplayDate = DateTime.Now;
            endDateCalendar.SelectedDate = DateTime.Now;
            endDateCalendar.DisplayDate = DateTime.Now;
        }

        private void InitializeForm()
        {
            //_accM = new AccountManager();
            //_gaAccounts = _accM.GetGoogleAnalyticsAccountData(userAccount);
            //_gaView = _accM.GetGoogleAnalyticsViewData(userAccount, "~all", "~all");
            //_columnsData = _accM.GetGoogleAnalyticsColumnsData();    
            BindSizeList(ListType.Dim);
            BindSizeList(ListType.Met);
            BindSizeList(ListType.Fil);
            BindSizeList(ListType.Sort);
            BindSizeList(ListType.View);

            if (_query.SortParams != null && _query.SortParams.Count > 0)
            {
                sortBycomboBox.SelectedValue = _query.SortParams.First().Value;
            }

            if (_query.GetMetricsAndDimensions.Count() > 0)
            {
                queryNotCompleted = true;
                DataBindSortByDropDown();
            }

            if (CurrentUserAccount != null)
            {
                DataBindAccountsDropDown();
                DataBindSegmentsDropDown();
            }

            this._query.AccountId.OrderBy(x => x);

            if (this._query.AccountId.Count > 0)
            {
                string aID = this._query.AccountId.First().Value;
                if (_currentUserAccount.Entrys.Find(p => p.AccountId == aID) != null)
                    comboBoxAccount.SelectedValue = _currentUserAccount.Entrys.Find(p => p.AccountId == aID);
                else
                    Notify("你没有任何账户");
            }
            else if (_currentUserAccount != null)
            {
                comboBoxAccount.SelectedIndex = _currentUserAccount.Entrys.Count > 0 ? 0 : -1;
            }

            if (CurrentUserAccount != null)
            {
                DataBindSitesDropDown();
            }

            SetCalendars();

            activeSize = SizeKeyType.Dimension;

            startIndexTextBox.Text = _query.StartIndex.ToString();
            maxResultsTextBox.Text = _query.MaxResults.ToString();
        }

        private void SetCalendars()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (this._query != null && _query.StartDate.Year != 1 && _query.EndDate.Year != 1)
            {
                startDate = _query.StartDate;
                endDate = _query.EndDate;
            }
            startDateCalendar.SelectedDate = startDate;
            startDateCalendar.DisplayDate = startDate;
            endDateCalendar.SelectedDate = endDate;
            endDateCalendar.DisplayDate = endDate;
        }

        private void DataBindSitesDropDown()
        {
            Binding sites = new Binding();
            List<Entry> fitering = new List<Entry>();
            foreach (Entry item in CurrentUserAccount.Entrys)
            {
                if (item.AccountId==(comboBoxAccount.SelectedItem as Entry).AccountId)
                {
                    fitering.Add(item);
                }
            }
            sites.Source = fitering;
            comboBoxView.SetBinding(ComboBox.ItemsSourceProperty, sites);
        }

        private void Notify(string message)
        {
            MainNotify.Visibility = Visibility.Visible;
            MainNotify.ErrorMessage = message;
        }

        private void DimessionAndMetrics(ColumnsData _columnsData)
        {
            IList<ColumnsItems> items = _columnsData.Items;
            IEnumerable<IGrouping<string, DimessionAndMetricsGroups>> groups = items.GroupBy(item => item.Attributes.Type, item => new DimessionAndMetricsGroups(){DataType = item.Attributes.DataType, Group=item.Attributes.Group, Id= item.ColumnsId, Status = item.Attributes.Status, UiName = item.Attributes.UiName});
            Binding dimessions = new Binding();
            Binding metrics = new Binding();
            foreach (IGrouping<string, DimessionAndMetricsGroups> group in groups)
            {
                var key = group.Key;
                foreach (DimessionAndMetricsGroups item in group)
                {
                    
                }
            }
           
        }

        private void DataBindSegmentsDropDown()
        {
            Binding segments = new Binding();  
        }

        private void DataBindAccountsDropDown()
        {
            Binding accounts = new Binding();
            List<Entry> duplicationCheck = new List<Entry>();
            accounts.Source = duplicationCheck;
            comboBoxAccount.SetBinding(ComboBox.ItemsSourceProperty, accounts);
        }

        private void DataBindSortByDropDown()
        {
            Binding sites = new Binding();
            sites.Source = _query.GetMetricsAndDimensions;
            sortBycomboBox.SetBinding(ComboBox.ItemsSourceProperty, sites);
        }

        private void BindSizeList(ListType listType)
        {
            Binding binding = new Binding();
            switch (listType)
            {
                case ListType.Dim:
                    binding.Source = _query.Dimensions;
                    dimensionsSelected.SetBinding(ListBox.ItemsSourceProperty, binding);
                    break;
                case ListType.Met:
                    binding.Source = _query.Metrics;
                    metricsSelected.SetBinding(ListBox.ItemsSourceProperty, binding);
                    break;
                case ListType.Fil:
                    binding.Source = _query.Filter;
                    activeFilters.SetBinding(ListBox.ItemsSourceProperty, binding);
                    break;
                case ListType.Sort:
                    binding.Source = _query.Sort;
                    activeSortings.SetBinding(ListBox.ItemsSourceProperty, binding);
                    break;
                case ListType.View:
                    binding.Source = _query.ViewId;
                    activeViews.SetBinding(ListBox.ItemsSourceProperty, binding);
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DimessionsExpander_Expanded(object sender, RoutedEventArgs e)
        {
            MetricsExpander.IsExpanded = false;
            FilterExpander.IsExpanded = false;
            SortExpander.IsExpanded = false;
            View_Expander.IsExpanded = false;
            DimensionsView.Visibility = Visibility.Visible;
            DimensionsView.BeginAnimation(TreeView.WidthProperty, AnimationPropertyWidth);
            DimensionsView.BeginAnimation(TreeView.HeightProperty, AnimationPropertyHeight);
            SetCheckedItems(_query.Dimensions, DimensionsView.tress.Items[0] as SizeViewModel);
            dimensionsMayHaveChanged = true;
        }

        private void SetCheckedItems(Dictionary<string, string> dictionary, SizeViewModel sizeViewModel)
        {
            foreach (SizeViewModel category in sizeViewModel.Children)
            {
                foreach (SizeViewModel size in category.Children)
                {
                    if (dictionary.Keys.Contains(size.Name))
                    {
                        size.IsChecked = true;
                    }
                }
            }
        }

        private DoubleAnimation AnimationPropertyWidth
        {
            get
            {
                return _animatePropertyWidth != null ? _animatePropertyWidth :
                    new DoubleAnimation(0.0, 543.0, new Duration(TimeSpan.FromSeconds(0.2))) { DecelerationRatio = 0.9 };
            }
            set { _animatePropertyWidth = value; }
        }

        public DoubleAnimation AnimationPropertyHeight
        {
            get
            {
                return _animatePropertyHeight != null ? _animatePropertyHeight :
                    new DoubleAnimation(0.0, 259.0, new Duration(TimeSpan.FromSeconds(0.2))) { DecelerationRatio = 0.2 };
            }
            set
            {
                _animatePropertyHeight = value;
            }
        }

        private void FilterExpander_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void DimessionsExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            _query.Dimensions = GetCheckedItems(DimensionsView.tress.Items[0] as SizeViewModel);
            BindSizeList(ListType.Dim);
        }

        private Dictionary<string, string> GetCheckedItems(SizeViewModel customTreeItems)
        {
            Dictionary<string, string> checkedSizes = new Dictionary<string, string>();
            foreach (SizeViewModel item in (customTreeItems).Children)
            {
                foreach (SizeViewModel subItem in item.Children)
                {
                    if (subItem.IsChecked == true)
                    {
                        checkedSizes.Add(subItem.Name, subItem.Value);
                    }
                }
            }
            return checkedSizes;
        }
    }
}
