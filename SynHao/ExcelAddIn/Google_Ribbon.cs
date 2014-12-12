using System.Linq;
using Microsoft.Office.Tools.Ribbon;
using Analytics.Authorization;
using UI;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ExcelAddIn
{
    public partial class Google_Ribbon
    {
        #region 字段
        private const string queryInfoIdentifier = "queryInfo[";

        UserAccount _user;
        Login _login;
        // ExecutionProgress _executionProgressWindow;
        #endregion

        private bool IsActiveCellUpdatable
        {
            get { return !string.IsNullOrEmpty(GetQueryExcelParamValueFromActiveCell("queryString")[1]); }
        }

        public Range ActiveCell
        {
            get
            {
                return ExcelAddIn.Globals.ThisAddIn.Application.ActiveCell;
            }
        }

        private string[] GetQueryExcelParamValueFromActiveCell(string name)
        {
            if (ActiveCell.Value2 == null)
            {
                return new string[2];
            }
            string activeValue = ActiveCell.Value2.ToString();
            if (activeValue.Contains(queryInfoIdentifier))
            {
                string paramString = activeValue.Substring(activeValue.IndexOf(queryInfoIdentifier));
                int lastBracket = paramString.Length;
                paramString = paramString.Replace(queryInfoIdentifier, string.Empty);
                paramString = paramString.Substring(0, paramString.Length - 1);

                string[] paramArray = paramString.Split(';');
                int arrayLength = paramArray.Length;
                if (arrayLength > 4)
                {
                    int i = arrayLength - 4;
                    for (int j = 0; j < i; j++)
                    {
                        paramArray[0] = paramArray[0] + ";" + paramArray[j + 1];
                    }
                }

                string[] rowsParam = new string[2];
                rowsParam[1] = paramArray.Where(p => p.StartsWith(name)).First().ToString();
                rowsParam[1] = rowsParam[1].Substring(rowsParam[1].IndexOf('=') + 1).Trim();
                return rowsParam;
            }
            return new string[2];
        }

        #region 事件
        private void Google_Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            ExcelAddIn.Globals.ThisAddIn.Application.SheetSelectionChange +=
                new AppEvents_SheetSelectionChangeEventHandler(Application_SheetSelectionChange);
            ExcelAddIn.Globals.ThisAddIn.Application.SheetChange +=
                new AppEvents_SheetChangeEventHandler(Application_SheetSelectionChange);
        }

        void Application_SheetSelectionChange(object Sh, Range Target)
        {
            buttonUpdate.Enabled = IsActiveCellUpdatable;
        }

        #endregion

        private void buttonAccount_Click_1(object sender, RibbonControlEventArgs e)
        {
            InitLogin();
            System.Diagnostics.Process.Start("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=587309872788-g295nn514r7jf2k79g8108rp6cr08hor.apps.googleusercontent.com&redirect_uri=urn:ietf:wg:oauth:2.0:oob:auto&scope=https://www.googleapis.com/auth/analytics.readonly&login_hint=email");

        }



        private void InitLogin()
        {
            ExcelAddIn.Settings settings = ExcelAddIn.Settings.Default;

            this._login = new Login();
            _login.authSuccessful += new Login.AuthSuccessful(User_Successful_Login);
            //_login.
            _login.Show();
        }

        void User_Successful_Login(string authToken)
        {
            AccountManager accMan = new AccountManager();
            MessageBox.Show("授权成功", "授权成功", MessageBoxButtons.OK);
        }
    }
}
