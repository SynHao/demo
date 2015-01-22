namespace SynHaoExcel
{
    partial class GoogleRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public GoogleRibbon()
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
            this.TabData = this.Factory.CreateRibbonTab();
            this.grpOAuth = this.Factory.CreateRibbonGroup();
            this.btnOAuth = this.Factory.CreateRibbonButton();
            this.TabData.SuspendLayout();
            this.grpOAuth.SuspendLayout();
            // 
            // TabData
            // 
            this.TabData.Groups.Add(this.grpOAuth);
            this.TabData.Label = "数据处理";
            this.TabData.Name = "TabData";
            // 
            // grpOAuth
            // 
            this.grpOAuth.Items.Add(this.btnOAuth);
            this.grpOAuth.Label = "账户";
            this.grpOAuth.Name = "grpOAuth";
            // 
            // btnOAuth
            // 
            this.btnOAuth.Label = "账户登录";
            this.btnOAuth.Name = "btnOAuth";
            this.btnOAuth.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOAuth_Click);
            // 
            // GoogleRibbon
            // 
            this.Name = "GoogleRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.TabData);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.GoogleRibbon_Load);
            this.TabData.ResumeLayout(false);
            this.TabData.PerformLayout();
            this.grpOAuth.ResumeLayout(false);
            this.grpOAuth.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabData;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpOAuth;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnOAuth;
    }

    partial class ThisRibbonCollection
    {
        internal GoogleRibbon GoogleRibbon
        {
            get { return this.GetRibbon<GoogleRibbon>(); }
        }
    }
}
