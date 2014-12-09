namespace SynHaoData
{
    partial class RibbonData : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonData()
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
            this.tabData = this.Factory.CreateRibbonTab();
            this.grpOAuth = this.Factory.CreateRibbonGroup();
            this.btnLogin = this.Factory.CreateRibbonButton();
            this.tabData.SuspendLayout();
            this.grpOAuth.SuspendLayout();
            // 
            // tabData
            // 
            this.tabData.Groups.Add(this.grpOAuth);
            this.tabData.Label = "数据导入";
            this.tabData.Name = "tabData";
            // 
            // grpOAuth
            // 
            this.grpOAuth.Items.Add(this.btnLogin);
            this.grpOAuth.Label = "授权";
            this.grpOAuth.Name = "grpOAuth";
            // 
            // btnLogin
            // 
            this.btnLogin.Label = "点击授权";
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogin_Click);
            // 
            // RibbonData
            // 
            this.Name = "RibbonData";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabData);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonData_Load);
            this.tabData.ResumeLayout(false);
            this.tabData.PerformLayout();
            this.grpOAuth.ResumeLayout(false);
            this.grpOAuth.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabData;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpOAuth;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLogin;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonData RibbonData
        {
            get { return this.GetRibbon<RibbonData>(); }
        }
    }
}
