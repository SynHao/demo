using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Controls.CustomTreeView
{
    /// <summary>
    /// CustomTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class CustomTreeView : UserControl
    {
        public enum DataType{Dimensions, Metrics};
        static readonly DependencyProperty Type;

        public CustomTreeView()
        {
            InitializeComponent();
        }

        public DataType ItemDataType
        {
            get
            {
                return (DataType)this.GetValue(Type);
            }
            set
            {
                this.SetValue(Type, value);
            }
        }

        static CustomTreeView()
        {
            Type = DependencyProperty.Register("Type",
                typeof(DataType), typeof(CustomTreeView), new FrameworkPropertyMetadata(OnTypeChanged));
        }

        private static void OnTypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            dependencyObject.SetValue(CustomTreeView.Type, (DataType)e.NewValue);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ItemDataType == DataType.Dimensions ?
                Resources["dimProvider"] : Resources["metProvider"];
        }

        private void UncheckAll_Click(object sender, RoutedEventArgs e)
        {
            SizeViewModel root = this.tress.Items[0] as SizeViewModel;
            root.IsChecked = false;
        }
    }
}
