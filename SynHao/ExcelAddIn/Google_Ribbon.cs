using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Analytics.Authorization;

namespace ExcelAddIn
{
    public partial class Google_Ribbon
    {
        #region 字段
        private const string queryInfoIdentifier = "queryInfo[";

        UserAccount _user;
        #endregion

        private void Google_Ribbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void buttonAccount_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}
