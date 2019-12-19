namespace Expander
{
    partial class RIBBON_EXPANDER : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RIBBON_EXPANDER()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.MENU_EXPANDER = this.Factory.CreateRibbonTab();
            this.GROUP_EXPAND_TEXT = this.Factory.CreateRibbonGroup();
            this.BOX_EXPAND_TEXT = this.Factory.CreateRibbonBox();
            this.BUTTON_ALL_EXPAND = this.Factory.CreateRibbonButton();
            this.CHECKBOX_AUTOEXPAND = this.Factory.CreateRibbonCheckBox();
            this.BUTTON_LOAD_PROFILE = this.Factory.CreateRibbonButton();
            this.BUTTON_SET_PROFILE_DEFAULT = this.Factory.CreateRibbonButton();
            this.GROUP_EXPAND_MARKDOWN = this.Factory.CreateRibbonGroup();
            this.BUTTON_EXPAND_MARKDOWN = this.Factory.CreateRibbonButton();
            this.MENU_EXPANDER.SuspendLayout();
            this.GROUP_EXPAND_TEXT.SuspendLayout();
            this.BOX_EXPAND_TEXT.SuspendLayout();
            this.GROUP_EXPAND_MARKDOWN.SuspendLayout();
            this.SuspendLayout();
            // 
            // MENU_EXPANDER
            // 
            this.MENU_EXPANDER.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.MENU_EXPANDER.Groups.Add(this.GROUP_EXPAND_TEXT);
            this.MENU_EXPANDER.Groups.Add(this.GROUP_EXPAND_MARKDOWN);
            this.MENU_EXPANDER.Label = "Expander";
            this.MENU_EXPANDER.Name = "MENU_EXPANDER";
            // 
            // GROUP_EXPAND_TEXT
            // 
            this.GROUP_EXPAND_TEXT.Items.Add(this.BOX_EXPAND_TEXT);
            this.GROUP_EXPAND_TEXT.Label = " Expand Text";
            this.GROUP_EXPAND_TEXT.Name = "GROUP_EXPAND_TEXT";
            // 
            // BOX_EXPAND_TEXT
            // 
            this.BOX_EXPAND_TEXT.Items.Add(this.BUTTON_ALL_EXPAND);
            this.BOX_EXPAND_TEXT.Items.Add(this.CHECKBOX_AUTOEXPAND);
            this.BOX_EXPAND_TEXT.Items.Add(this.BUTTON_LOAD_PROFILE);
            this.BOX_EXPAND_TEXT.Items.Add(this.BUTTON_SET_PROFILE_DEFAULT);
            this.BOX_EXPAND_TEXT.Name = "BOX_EXPAND_TEXT";
            // 
            // BUTTON_ALL_EXPAND
            // 
            this.BUTTON_ALL_EXPAND.Label = "All Expand";
            this.BUTTON_ALL_EXPAND.Name = "BUTTON_ALL_EXPAND";
            this.BUTTON_ALL_EXPAND.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AllExpand_Click);
            // 
            // CHECKBOX_AUTOEXPAND
            // 
            this.CHECKBOX_AUTOEXPAND.Label = "AutoExpand";
            this.CHECKBOX_AUTOEXPAND.Name = "CHECKBOX_AUTOEXPAND";
            this.CHECKBOX_AUTOEXPAND.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AutoExpand_Click);
            // 
            // BUTTON_LOAD_PROFILE
            // 
            this.BUTTON_LOAD_PROFILE.Label = "Load Profile";
            this.BUTTON_LOAD_PROFILE.Name = "BUTTON_LOAD_PROFILE";
            this.BUTTON_LOAD_PROFILE.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoadProfile_Click);
            // 
            // BUTTON_SET_PROFILE_DEFAULT
            // 
            this.BUTTON_SET_PROFILE_DEFAULT.Label = "Set Profile as Default";
            this.BUTTON_SET_PROFILE_DEFAULT.Name = "BUTTON_SET_PROFILE_DEFAULT";
            this.BUTTON_SET_PROFILE_DEFAULT.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SetProfileDefault_Click);
            // 
            // GROUP_EXPAND_MARKDOWN
            // 
            this.GROUP_EXPAND_MARKDOWN.Items.Add(this.BUTTON_EXPAND_MARKDOWN);
            this.GROUP_EXPAND_MARKDOWN.Label = "Expand Markdown";
            this.GROUP_EXPAND_MARKDOWN.Name = "GROUP_EXPAND_MARKDOWN";
            // 
            // BUTTON_EXPAND_MARKDOWN
            // 
            this.BUTTON_EXPAND_MARKDOWN.Label = "Expand Markdown";
            this.BUTTON_EXPAND_MARKDOWN.Name = "BUTTON_EXPAND_MARKDOWN";
            // 
            // RIBBON_EXPANDER
            // 
            this.Name = "RIBBON_EXPANDER";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.MENU_EXPANDER);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonExpander_Load);
            this.MENU_EXPANDER.ResumeLayout(false);
            this.MENU_EXPANDER.PerformLayout();
            this.GROUP_EXPAND_TEXT.ResumeLayout(false);
            this.GROUP_EXPAND_TEXT.PerformLayout();
            this.BOX_EXPAND_TEXT.ResumeLayout(false);
            this.BOX_EXPAND_TEXT.PerformLayout();
            this.GROUP_EXPAND_MARKDOWN.ResumeLayout(false);
            this.GROUP_EXPAND_MARKDOWN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab MENU_EXPANDER;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GROUP_EXPAND_TEXT;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BUTTON_ALL_EXPAND;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CHECKBOX_AUTOEXPAND;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GROUP_EXPAND_MARKDOWN;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BUTTON_EXPAND_MARKDOWN;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BUTTON_LOAD_PROFILE;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BUTTON_SET_PROFILE_DEFAULT;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox BOX_EXPAND_TEXT;
    }
}
