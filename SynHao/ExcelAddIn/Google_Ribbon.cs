using System.Linq;
using Microsoft.Office.Tools.Ribbon;
using Analytics.Authorization;
using UI;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Net;
using Analytics.Data;
using System.Collections.Generic;
using System;

namespace ExcelAddIn
{
    public partial class Google_Ribbon
    {
        #region 字段
        private const string queryInfoIdentifier = "queryInfo[";

       QueryBuilder _queryBuilderWindow;
       ExecutionProgress _executionProgressWindow;
       Report _currentReport;
       ReportManager _reportManager;
        UserAccount _user;
        Login _login;
        // ExecutionProgress _executionProgressWindow;

        string _cellValue = "";
        string _sFirstFoundAddress = "";
        static List<string> _addressQueries;
        List<Query> _listQueries;
        #endregion

        private byte[] randomBytes = { 4, 32, 62, 9, 145, 5 };


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

        private void buttonAccount_Click(object sender, RibbonControlEventArgs e)
        {
            InitLogin();
        }

        private void InitLogin()
        {
            ExcelAddIn.Settings settings = ExcelAddIn.Settings.Default;

            this._login = new Login(_user);
            _login.authSuccessful += new Login.AuthSuccessful(User_Successful_Login);

            // 处理RefreshToken
            if (settings.Token != null)
            {
                // MessageBox.Show(settings.Token);
                AccountManager accM = new AccountManager();
                string code = settings.Token;
                HttpStatusCode status;
                string authToken = accM.Authenticate("refresh_token=" + code, out status, out _user);
            }
            else // 直接授权
            {
                System.Diagnostics.Process.Start("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=" +
                    settings.ClientId + "&redirect_uri=urn:ietf:wg:oauth:2.0:oob&scope=" +
                    "https://www.googleapis.com/auth/analytics.readonly" + "&login_hint=email");
                if (_login.ShowDialog().GetValueOrDefault(false))
                {
                    if (_login.ActiveUser != null)
                    {
                        settings.Token = _login.ActiveUser.RefreshToken;
                        _user = _login.ActiveUser;
                    }
                    settings.Save();
                }
            }
            //_login.
        }

        void User_Successful_Login(string authToken)
        {
            AccountManager accMan = new AccountManager();
            MessageBox.Show("授权成功", "授权成功", MessageBoxButtons.OK);
        }

        private void buttonQuery_Click(object sender, RibbonControlEventArgs e)
        {
            LaunchQueryBuilder(new Query());
        }

        private void LaunchQueryBuilder(Query query)
        {
            if (_user != null && !string.IsNullOrEmpty(_user.AccessToken))
            {
                _queryBuilderWindow = new QueryBuilder(_user, query);
                _queryBuilderWindow.queryComplete += new QueryBuilder.QueryComplete(queryComplete);
                _queryBuilderWindow.ShowDialog();
            }
        }

        private void queryComplete(Query query)
        {
            _listQueries = new List<Query>();
            List<Query> queries = new List<Query>();

            //_gaView = _accM.GetGoogleAnalyticsViewData(userAccount, "~all", "~all");

            foreach (Item item in query.Ids)
            {
                _listQueries.Add(query);
            }

            ExecuteQuery(_listQueries, false);
        }

        private void ExecuteQuery(List<Query> _listQueries, bool p)
        {
            throw new System.NotImplementedException();
        }
    }
}
