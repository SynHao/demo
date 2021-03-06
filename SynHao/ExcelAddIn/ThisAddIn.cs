﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Data;

namespace ExcelAddIn
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Settings settings = Settings.Default;
            // 上传时要更改
            if (settings.FirstStartup)
            {
                settings.Dimessions = Analytics.Data.Validation.XmlLoader.Dimensions;
                settings.Metrics = Analytics.Data.Validation.XmlLoader.Metrics;
                settings.FirstStartup = false;
                settings.Save();
            }
            Analytics.Settings.Instance.AutoEscapeFilter = settings.AutoEscapeFilter;
            Analytics.Settings.Instance.UseProxy = settings.UseProxy;
            Analytics.Settings.Instance.ProxyAddress = settings.ProxyAddress;
            Analytics.Settings.Instance.ProxyPassword = settings.ProxyPassword;
            Analytics.Settings.Instance.ProxyPort = settings.ProxyPort;
            Analytics.Settings.Instance.ProxyUsername = settings.ProxyUsername;
            Analytics.Settings.Instance.RequestTimeout = settings.RequestTimeout;
            Analytics.Settings.Instance.MetricsXml = settings.Metrics;
            Analytics.Settings.Instance.DimensionsXml = settings.Dimessions;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
