namespace Expander
{
    partial class RibbonExpander : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonExpander()
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
            this.m_oTabExpander = this.Factory.CreateRibbonTab();
            this.m_oGroupExpand = this.Factory.CreateRibbonGroup();
            this.m_oButtonAllExpand = this.Factory.CreateRibbonButton();
            this.m_oLabelOptions = this.Factory.CreateRibbonLabel();
            this.m_oCheckBoxAutoExpand = this.Factory.CreateRibbonCheckBox();
            this.m_oButtonLoadProfile = this.Factory.CreateRibbonButton();
            this.m_oButtonSetProfDefault = this.Factory.CreateRibbonButton();
            this.m_oGroupExpandMarkdown = this.Factory.CreateRibbonGroup();
            this.m_oButtonExpandMarkdown = this.Factory.CreateRibbonButton();
            this.m_oTabExpander.SuspendLayout();
            this.m_oGroupExpand.SuspendLayout();
            this.m_oGroupExpandMarkdown.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_oTabExpander
            // 
            this.m_oTabExpander.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.m_oTabExpander.Groups.Add(this.m_oGroupExpand);
            this.m_oTabExpander.Groups.Add(this.m_oGroupExpandMarkdown);
            this.m_oTabExpander.Label = "Expander";
            this.m_oTabExpander.Name = "m_oTabExpander";
            // 
            // m_oGroupExpand
            // 
            this.m_oGroupExpand.Items.Add(this.m_oButtonAllExpand);
            this.m_oGroupExpand.Items.Add(this.m_oLabelOptions);
            this.m_oGroupExpand.Items.Add(this.m_oCheckBoxAutoExpand);
            this.m_oGroupExpand.Items.Add(this.m_oButtonLoadProfile);
            this.m_oGroupExpand.Items.Add(this.m_oButtonSetProfDefault);
            this.m_oGroupExpand.Label = "Snippets";
            this.m_oGroupExpand.Name = "m_oGroupExpand";
            // 
            // m_oButtonAllExpand
            // 
            this.m_oButtonAllExpand.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.m_oButtonAllExpand.Label = "Tout Déplier";
            this.m_oButtonAllExpand.Name = "m_oButtonAllExpand";
            this.m_oButtonAllExpand.OfficeImageId = "AutoCorrect";
            this.m_oButtonAllExpand.ScreenTip = "Déplier tous les Snippets dans le Document";
            this.m_oButtonAllExpand.ShowImage = true;
            this.m_oButtonAllExpand.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AllExpand_Click);
            // 
            // m_oLabelOptions
            // 
            this.m_oLabelOptions.Label = "Options";
            this.m_oLabelOptions.Name = "m_oLabelOptions";
            // 
            // m_oCheckBoxAutoExpand
            // 
            this.m_oCheckBoxAutoExpand.Checked = true;
            this.m_oCheckBoxAutoExpand.Label = "Déplier Automatiquement";
            this.m_oCheckBoxAutoExpand.Name = "m_oCheckBoxAutoExpand";
            this.m_oCheckBoxAutoExpand.ScreenTip = "Déplier Automatiquement les Snippets";
            this.m_oCheckBoxAutoExpand.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AutoExpand_Click);
            // 
            // m_oButtonLoadProfile
            // 
            this.m_oButtonLoadProfile.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.m_oButtonLoadProfile.Label = "Load Profile";
            this.m_oButtonLoadProfile.Name = "m_oButtonLoadProfile";
            this.m_oButtonLoadProfile.OfficeImageId = "FileSave";
            this.m_oButtonLoadProfile.ShowImage = true;
            this.m_oButtonLoadProfile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoadProfile_Click);
            // 
            // m_oButtonSetProfDefault
            // 
            this.m_oButtonSetProfDefault.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.m_oButtonSetProfDefault.Label = "Set Profile as Default";
            this.m_oButtonSetProfDefault.Name = "m_oButtonSetProfDefault";
            this.m_oButtonSetProfDefault.OfficeImageId = "FileOpen";
            this.m_oButtonSetProfDefault.ShowImage = true;
            this.m_oButtonSetProfDefault.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SetProfileDefault_Click);
            // 
            // m_oGroupExpandMarkdown
            // 
            this.m_oGroupExpandMarkdown.Items.Add(this.m_oButtonExpandMarkdown);
            this.m_oGroupExpandMarkdown.Label = "Markdown";
            this.m_oGroupExpandMarkdown.Name = "m_oGroupExpandMarkdown";
            // 
            // m_oButtonExpandMarkdown
            // 
            this.m_oButtonExpandMarkdown.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.m_oButtonExpandMarkdown.Label = "Mettre en Forme avec Markdown";
            this.m_oButtonExpandMarkdown.Name = "m_oButtonExpandMarkdown";
            this.m_oButtonExpandMarkdown.OfficeImageId = "OutlineExpand";
            this.m_oButtonExpandMarkdown.ShowImage = true;
            // 
            // RibbonExpander
            // 
            this.Name = "RibbonExpander";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.m_oTabExpander);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonExpander_Load);
            this.m_oTabExpander.ResumeLayout(false);
            this.m_oTabExpander.PerformLayout();
            this.m_oGroupExpand.ResumeLayout(false);
            this.m_oGroupExpand.PerformLayout();
            this.m_oGroupExpandMarkdown.ResumeLayout(false);
            this.m_oGroupExpandMarkdown.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab m_oTabExpander;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton m_oButtonAllExpand;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox m_oCheckBoxAutoExpand;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup m_oGroupExpandMarkdown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton m_oButtonExpandMarkdown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton m_oButtonLoadProfile;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton m_oButtonSetProfDefault;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup m_oGroupExpand;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel m_oLabelOptions;
    }
}
