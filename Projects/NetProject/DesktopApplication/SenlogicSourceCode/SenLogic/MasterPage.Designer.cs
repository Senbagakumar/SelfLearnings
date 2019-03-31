namespace SenLogic
{
    partial class MasterPage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPage));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mASTERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaterials = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVehicles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVendors = new System.Windows.Forms.ToolStripMenuItem();
            this.placesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tICKETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.uSERSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aCCESSSETTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCompanySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeneralSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tmrDateTime = new System.Windows.Forms.Timer(this.components);
            this.companyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mASTERToolStripMenuItem,
            this.tICKETToolStripMenuItem,
            this.rEPORTSToolStripMenuItem,
            this.mnuCompany,
            this.eXITToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // mASTERToolStripMenuItem
            // 
            this.mASTERToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCustomers,
            this.mnuMaterials,
            this.mnuVehicles,
            this.mnuVendors,
            this.placesToolStripMenuItem,
            this.customFieldsToolStripMenuItem,
            this.companyToolStripMenuItem});
            this.mASTERToolStripMenuItem.Name = "mASTERToolStripMenuItem";
            this.mASTERToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.mASTERToolStripMenuItem.Text = "MASTERS";
            // 
            // mnuCustomers
            // 
            this.mnuCustomers.Name = "mnuCustomers";
            this.mnuCustomers.Size = new System.Drawing.Size(152, 22);
            this.mnuCustomers.Text = "Customers";
            
            // 
            // mnuMaterials
            // 
            this.mnuMaterials.Name = "mnuMaterials";
            this.mnuMaterials.Size = new System.Drawing.Size(152, 22);
            this.mnuMaterials.Text = "Materials";
            this.mnuMaterials.Click += new System.EventHandler(this.mnuMaterials_Click);
            // 
            // mnuVehicles
            // 
            this.mnuVehicles.Name = "mnuVehicles";
            this.mnuVehicles.Size = new System.Drawing.Size(152, 22);
            this.mnuVehicles.Text = "Vehicles";
            this.mnuVehicles.Click += new System.EventHandler(this.mnuVehicles_Click);
            // 
            // mnuVendors
            // 
            this.mnuVendors.Name = "mnuVendors";
            this.mnuVendors.Size = new System.Drawing.Size(152, 22);
            this.mnuVendors.Text = "Vendors";
            
            // 
            // placesToolStripMenuItem
            // 
            this.placesToolStripMenuItem.Name = "placesToolStripMenuItem";
            this.placesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.placesToolStripMenuItem.Text = "Places";
            
            // 
            // customFieldsToolStripMenuItem
            // 
            this.customFieldsToolStripMenuItem.Name = "customFieldsToolStripMenuItem";
            this.customFieldsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.customFieldsToolStripMenuItem.Text = "CustomFields";
            
            // 
            // tICKETToolStripMenuItem
            // 
            this.tICKETToolStripMenuItem.Name = "tICKETToolStripMenuItem";
            this.tICKETToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.tICKETToolStripMenuItem.Text = "TICKET";
            
            // 
            // rEPORTSToolStripMenuItem
            // 
            this.rEPORTSToolStripMenuItem.Name = "rEPORTSToolStripMenuItem";
            this.rEPORTSToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.rEPORTSToolStripMenuItem.Text = "REPORTS";
            this.rEPORTSToolStripMenuItem.Click += new System.EventHandler(this.rEPORTSToolStripMenuItem_Click);
            // 
            // mnuCompany
            // 
            this.mnuCompany.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSERSToolStripMenuItem,
            this.aCCESSSETTINGToolStripMenuItem,
            this.mnuCompanySettings,
            this.mnuGeneralSetting,
            this.viewToolStripMenuItem});
            this.mnuCompany.Name = "mnuCompany";
            this.mnuCompany.Size = new System.Drawing.Size(58, 20);
            this.mnuCompany.Text = "ADMIN";
            // 
            // uSERSToolStripMenuItem
            // 
            this.uSERSToolStripMenuItem.Name = "uSERSToolStripMenuItem";
            this.uSERSToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.uSERSToolStripMenuItem.Text = "USERS";
            this.uSERSToolStripMenuItem.Click += new System.EventHandler(this.uSERSToolStripMenuItem_Click);
            // 
            // aCCESSSETTINGToolStripMenuItem
            // 
            this.aCCESSSETTINGToolStripMenuItem.Name = "aCCESSSETTINGToolStripMenuItem";
            this.aCCESSSETTINGToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aCCESSSETTINGToolStripMenuItem.Text = "ACCESS SETTING";
            this.aCCESSSETTINGToolStripMenuItem.Click += new System.EventHandler(this.aCCESSSETTINGToolStripMenuItem_Click);
            // 
            // mnuCompanySettings
            // 
            this.mnuCompanySettings.Name = "mnuCompanySettings";
            this.mnuCompanySettings.Size = new System.Drawing.Size(173, 22);
            this.mnuCompanySettings.Text = "COMPANY";
            this.mnuCompanySettings.Click += new System.EventHandler(this.mnuCompanySettings_Click);
            // 
            // mnuGeneralSetting
            // 
            this.mnuGeneralSetting.Name = "mnuGeneralSetting";
            this.mnuGeneralSetting.Size = new System.Drawing.Size(173, 22);
            this.mnuGeneralSetting.Text = "GENERAL SETTING";
            
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.viewToolStripMenuItem.Text = "VIEW";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // eXITToolStripMenuItem1
            // 
            this.eXITToolStripMenuItem1.Name = "eXITToolStripMenuItem1";
            this.eXITToolStripMenuItem1.Size = new System.Drawing.Size(42, 20);
            this.eXITToolStripMenuItem1.Text = "EXIT";
            this.eXITToolStripMenuItem1.Click += new System.EventHandler(this.eXITToolStripMenuItem1_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblDateTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Status";
            // 
            // lblDateTime
            // 
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(71, 17);
            this.lblDateTime.Text = "lblDateTime";
            // 
            // tmrDateTime
            // 
            this.tmrDateTime.Tick += new System.EventHandler(this.tmrDateTime_Tick);
            
            
            // 
            // MasterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MasterPage";
            this.Text = "WeighBridge";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MasterPage_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel lblDateTime;
        private System.Windows.Forms.Timer tmrDateTime;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuCompany;
        private System.Windows.Forms.ToolStripMenuItem uSERSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aCCESSSETTINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tICKETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mASTERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomers;
        private System.Windows.Forms.ToolStripMenuItem mnuVendors;
        private System.Windows.Forms.ToolStripMenuItem mnuMaterials;
        private System.Windows.Forms.ToolStripMenuItem mnuVehicles;
        private System.Windows.Forms.ToolStripMenuItem mnuCompanySettings;
        private System.Windows.Forms.ToolStripMenuItem mnuGeneralSetting;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem placesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem companyToolStripMenuItem;
    }
}



