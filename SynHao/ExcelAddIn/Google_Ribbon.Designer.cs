namespace ExcelAddIn
{
    partial class Google_Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Google_Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.Google = this.Factory.CreateRibbonGroup();
            this.buttonAccount = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.buttonQuery = this.Factory.CreateRibbonButton();
            this.buttonUpdate = this.Factory.CreateRibbonButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.SettingButton = this.Factory.CreateRibbonButton();
            this.separator3 = this.Factory.CreateRibbonSeparator();
            this.AboutButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.Google.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.Google);
            this.tab1.Label = "数据";
            this.tab1.Name = "tab1";
            // 
            // Google
            // 
            this.Google.Items.Add(this.buttonAccount);
            this.Google.Items.Add(this.separator1);
            this.Google.Items.Add(this.buttonQuery);
            this.Google.Items.Add(this.buttonUpdate);
            this.Google.Items.Add(this.separator2);
            this.Google.Items.Add(this.SettingButton);
            this.Google.Items.Add(this.separator3);
            this.Google.Items.Add(this.AboutButton);
            this.Google.Label = "谷歌数据";
            this.Google.Name = "Google";
            // 
            // buttonAccount
            // 
            this.buttonAccount.Label = "账户";
            this.buttonAccount.Name = "buttonAccount";
            this.buttonAccount.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAccount_Click_1);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // buttonQuery
            // 
            this.buttonQuery.Label = "新的请求";
            this.buttonQuery.Name = "buttonQuery";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Label = "数据更新";
            this.buttonUpdate.Name = "buttonUpdate";
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // SettingButton
            // 
            this.SettingButton.Label = "设置";
            this.SettingButton.Name = "SettingButton";
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            // 
            // AboutButton
            // 
            this.AboutButton.Label = "关于我们";
            this.AboutButton.Name = "AboutButton";
            // 
            // Google_Ribbon
            // 
            this.Name = "Google_Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Google_Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.Google.ResumeLayout(false);
            this.Google.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Google;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAccount;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonQuery;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUpdate;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SettingButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AboutButton;
    }

    partial class ThisRibbonCollection
    {
        internal Google_Ribbon Google_Ribbon
        {
            get { return this.GetRibbon<Google_Ribbon>(); }
        }
    }
}
