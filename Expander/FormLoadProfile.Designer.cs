namespace Expander
{
    partial class FormLoadProfile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_oProgressBar = new System.Windows.Forms.ProgressBar();
            this.m_oButtonCancel = new System.Windows.Forms.Button();
            this.m_oBackWorkLoadData = new System.ComponentModel.BackgroundWorker();
            this.m_oBackWorkLoadFile = new System.ComponentModel.BackgroundWorker();
            this.m_oBackWorkValidateFile = new System.ComponentModel.BackgroundWorker();
            this.m_oButtonOK = new System.Windows.Forms.Button();
            this.m_oTextBoxLog = new System.Windows.Forms.TextBox();
            this.m_oLabelStatus = new System.Windows.Forms.Label();
            this.m_oButtonDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_oProgressBar
            // 
            this.m_oProgressBar.Location = new System.Drawing.Point(12, 29);
            this.m_oProgressBar.Name = "m_oProgressBar";
            this.m_oProgressBar.Size = new System.Drawing.Size(432, 16);
            this.m_oProgressBar.TabIndex = 3;
            // 
            // m_oButtonCancel
            // 
            this.m_oButtonCancel.Location = new System.Drawing.Point(369, 51);
            this.m_oButtonCancel.Name = "m_oButtonCancel";
            this.m_oButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.m_oButtonCancel.TabIndex = 2;
            this.m_oButtonCancel.Text = "Annuler";
            this.m_oButtonCancel.UseVisualStyleBackColor = true;
            this.m_oButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // m_oBackWorkLoadData
            // 
            this.m_oBackWorkLoadData.WorkerReportsProgress = true;
            this.m_oBackWorkLoadData.WorkerSupportsCancellation = true;
            this.m_oBackWorkLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackWorkLoadData_DoWork);
            this.m_oBackWorkLoadData.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackWorkLoadData_ProgressChanged);
            this.m_oBackWorkLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackWorkLoadData_RunWorkerCompleted);
            // 
            // m_oBackWorkLoadFile
            // 
            this.m_oBackWorkLoadFile.WorkerReportsProgress = true;
            this.m_oBackWorkLoadFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackWorkLoadFile_DoWork);
            this.m_oBackWorkLoadFile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackWorkLoadFile_RunWorkerCompleted);
            // 
            // m_oBackWorkValidateFile
            // 
            this.m_oBackWorkValidateFile.WorkerReportsProgress = true;
            this.m_oBackWorkValidateFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackWorkValidateFile_DoWork);
            this.m_oBackWorkValidateFile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackWorkValidateFile_RunWorkerCompleted);
            // 
            // m_oButtonOK
            // 
            this.m_oButtonOK.Enabled = false;
            this.m_oButtonOK.Location = new System.Drawing.Point(288, 51);
            this.m_oButtonOK.Name = "m_oButtonOK";
            this.m_oButtonOK.Size = new System.Drawing.Size(75, 23);
            this.m_oButtonOK.TabIndex = 9;
            this.m_oButtonOK.Text = "OK";
            this.m_oButtonOK.UseVisualStyleBackColor = true;
            this.m_oButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // m_oTextBoxLog
            // 
            this.m_oTextBoxLog.Enabled = false;
            this.m_oTextBoxLog.Location = new System.Drawing.Point(12, 80);
            this.m_oTextBoxLog.Multiline = true;
            this.m_oTextBoxLog.Name = "m_oTextBoxLog";
            this.m_oTextBoxLog.ReadOnly = true;
            this.m_oTextBoxLog.Size = new System.Drawing.Size(432, 102);
            this.m_oTextBoxLog.TabIndex = 10;
            // 
            // m_oLabelStatus
            // 
            this.m_oLabelStatus.AutoSize = true;
            this.m_oLabelStatus.Location = new System.Drawing.Point(12, 9);
            this.m_oLabelStatus.Name = "m_oLabelStatus";
            this.m_oLabelStatus.Size = new System.Drawing.Size(0, 17);
            this.m_oLabelStatus.TabIndex = 11;
            // 
            // m_oButtonDetails
            // 
            this.m_oButtonDetails.Location = new System.Drawing.Point(12, 51);
            this.m_oButtonDetails.Name = "m_oButtonDetails";
            this.m_oButtonDetails.Size = new System.Drawing.Size(120, 23);
            this.m_oButtonDetails.TabIndex = 12;
            this.m_oButtonDetails.Text = "Voir détails";
            this.m_oButtonDetails.UseVisualStyleBackColor = true;
            this.m_oButtonDetails.Click += new System.EventHandler(this.ButtonDetails_Click);
            // 
            // FormLoadProfile
            // 
            this.ClientSize = new System.Drawing.Size(456, 194);
            this.Controls.Add(this.m_oButtonDetails);
            this.Controls.Add(this.m_oLabelStatus);
            this.Controls.Add(this.m_oTextBoxLog);
            this.Controls.Add(this.m_oButtonOK);
            this.Controls.Add(this.m_oProgressBar);
            this.Controls.Add(this.m_oButtonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLoadProfile";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chargement du profile";
            this.Shown += new System.EventHandler(this.FormLoadProfile_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar m_oProgressBar;
        private System.Windows.Forms.Button m_oButtonCancel;
        private System.ComponentModel.BackgroundWorker m_oBackWorkLoadFile;
        private System.ComponentModel.BackgroundWorker m_oBackWorkValidateFile;
        private System.ComponentModel.BackgroundWorker m_oBackWorkLoadData;
        private System.Windows.Forms.Button m_oButtonOK;
        private System.Windows.Forms.TextBox m_oTextBoxLog;
        private System.Windows.Forms.Label m_oLabelStatus;
        private System.Windows.Forms.Button m_oButtonDetails;
    }
}