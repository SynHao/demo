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

namespace UI.Controls.NotifyError
{
    /// <summary>
    /// NotifyError.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyError : UserControl
    {
        static readonly DependencyProperty Message;

        public string ErrorMessage
        {
            get { return (string)this.GetValue(Message); }
            set
            {
                this.MessageLabel.Text = value;
                this.SetValue(Message, value);
            }
        }
        public NotifyError()
        {
            InitializeComponent();
        }
    }
}
