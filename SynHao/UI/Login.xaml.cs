using Analytics.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace UI
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public delegate void AuthSuccessful(string autoToken);
        public event AuthSuccessful authSuccessful;
        private UserAccount activeUser;
        private string _refreshToken;
        private byte[] randomBytes = { 4, 32, 62, 9, 145, 5 };

        public UserAccount ActiveUser
        {
            get { return activeUser; }
            set { activeUser = value; }
        }

       // public event Logout logOur;

        public Login()
        {
            InitializeComponent();

        }

        public Login(UserAccount uAcc)
        {
            InitializeComponent();
            if (uAcc != null && !String.IsNullOrEmpty(uAcc.AccessToken))
            {
                this.activeUser = uAcc;
                buttonLogin.Content = "退出";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authenticate();
        }

        internal void Authenticate()
        {
            if (activeUser == null)
            {
                string code = textCode.Text;
                AccountManager accMan = new AccountManager();
                accMan.authProgress += new AccountManager.AuthProgress(accMan_authProgress);

                HttpStatusCode status;
                string authToken = accMan.Authenticate("code=" + code, out status, out activeUser);
                if (!String.IsNullOrEmpty(authToken) && status == HttpStatusCode.OK && authToken != null)
                {
                    DialogResult = true;
                    this.Close();
                    authSuccessful(authToken);
                }
                else if (status == HttpStatusCode.ProxyAuthenticationRequired)
                {
                    MessageBox.Show("验证请求失败。看来你背后的代理。设置在设置对话框中的代理服务器，然后重试。", "验证请求失败", MessageBoxButton.OK);
                }
                else
                {
                    mainNotify.Visibility = Visibility.Visible;
                    mainNotify.ErrorMessage = status == HttpStatusCode.Forbidden ? "无效的授权码" :
                        status == HttpStatusCode.NotFound ? "不能建立连接" : "错误" + status.ToString();
                }
            }
            else
            {
                buttonLogin.Content = "授权";
                textCode.Clear();
                textCode.IsEnabled = true;
                activeUser = null;
                
            }
        }

        void accMan_authProgress(int progress, string progressMessage)
        {
            mainNotify.ErrorMessage = progressMessage;
        }

        public string RefreshToken
        {
            get { return _refreshToken; }
            set { _refreshToken = value; }
        }
    }
}
