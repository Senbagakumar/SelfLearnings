using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SenLogic
{
    public partial class MasterPage : Form
    {
        #region " Common Varible "
        private Login frmLogin = null;
        private InitialForm frmInitialFrom = null;
        private ManualTareEntry frmUser = null;
        private SystemSettings frmAccessSetting = null;
      
        private frmDefaultImage frmMaterials = null;
        private StaticWeighment frmVehicles = null;
        private SettingPage frmCompanySettings = null;
     
        private Report frmReport = null;
        private View frmView = null;
      
        
        #endregion

        public MasterPage()
        {
            InitializeComponent();
            Shared_Variables.GetConnectionString = System.Configuration.ConfigurationSettings.AppSettings.Get("ConnectionString");
           // Shared_Variables.LineNumber = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("LineNumber"));
            
        }
        private void MasterPage_Load(object sender, EventArgs e)
        {
            Launch_Login();
            this.WindowState = FormWindowState.Maximized; 
            this.Hide();
            this.tmrDateTime.Start();

        }
        private void Launch_Login()
        {
            this.lblStatus.Width = this.Width - 50;
            frmInitialFrom = new InitialForm();
            frmInitialFrom.FormClosed += new FormClosedEventHandler(frmInitialFrom_FormClosed);
            frmInitialFrom.Show();
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                this.lblDateTime.Text = DateTime.Now.ToString("g");
            }
            catch (Exception ex)
            { Shared_Functions.Logged_ErrorMessage(ex.Message); }
        }
        void frmInitialFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmInitialFrom.Dispose();
            frmLogin = new Login();
            frmLogin.FormClosed += new FormClosedEventHandler(frmLogin_FormClosed);
            frmLogin.Show();
        }
        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            frmLogin.Dispose();         
            //this.Show();
            frmMaterials = new frmDefaultImage();
            frmMaterials.FormClosed += new FormClosedEventHandler(frmMaterials_FormClosed);
            frmMaterials.Show();
            //if (!(this.ActiveMdiChild == null))
            //{
            //    foreach (Form form in this.MdiChildren)
            //    {
            //        if ((form.Name == "Users"))
            //        {
            //            form.Activate();
            //            return;
            //        }
            //    }
            //}
            //Users home = new Users();
            //home.MdiParent = this;
            //home.Show();          
           


        }
        

        private void eXITToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uSERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmUser == null)
            {
                frmUser = new ManualTareEntry();
                frmUser.FormClosed += new FormClosedEventHandler(frmUser_FormClosed);
                frmUser.MdiParent = this;
                frmUser.Show();
            }
            else
            {
                frmUser.BringToFront();
            }
        }

        void frmUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmUser.Dispose();
            frmUser = null;
        }

        private void aCCESSSETTINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmAccessSetting == null)
            {
                frmAccessSetting = new SystemSettings();
                frmAccessSetting.FormClosed += new FormClosedEventHandler(frmAccessSetting_FormClosed);
                frmAccessSetting.MdiParent = this;
                frmAccessSetting.Show();
            }
            else
            {
                frmAccessSetting.BringToFront();
            }
        }
        void frmAccessSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmAccessSetting.Dispose();
            frmAccessSetting = null;
        }

        



        private void frmMaterials_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMaterials.Dispose();
            frmMaterials = null;

            frmCompanySettings = new SettingPage();
            frmCompanySettings.FormClosed += new FormClosedEventHandler(frmCompanySettings_FormClosed);
            frmCompanySettings.MdiParent = this;
            frmCompanySettings.Show();
        }
        private void mnuMaterials_Click(object sender, EventArgs e)
        {
            if (frmMaterials == null)
            {
                frmMaterials = new frmDefaultImage();
                frmMaterials.FormClosed += new FormClosedEventHandler(frmMaterials_FormClosed);
                frmMaterials.MdiParent = this;
                frmMaterials.Show();
            }
        }

        private void frmVehicles_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmVehicles.Dispose();
            frmVehicles = null;
        }

        private void mnuVehicles_Click(object sender, EventArgs e)
        {
            if (frmVehicles == null)
            {
                frmVehicles = new StaticWeighment();
                frmVehicles.FormClosed += new FormClosedEventHandler(frmVehicles_FormClosed);
                frmVehicles.MdiParent = this;
                frmVehicles.Show();
            }
        }

        private void frmCompanySettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmCompanySettings.Dispose();
            frmCompanySettings = null;
        }
        private void mnuCompanySettings_Click(object sender, EventArgs e)
        {

            if (frmCompanySettings == null)
            {
                frmCompanySettings = new SettingPage();
                frmCompanySettings.FormClosed += new FormClosedEventHandler(frmCompanySettings_FormClosed);
                frmCompanySettings.MdiParent = this;
                frmCompanySettings.Show();
            }

        }

    

        private void rEPORTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmReport == null)
            {
                frmReport = new Report();
                frmReport.FormClosed += new FormClosedEventHandler(frmReport_FormClosed);
                frmReport.MdiParent = this;
                frmReport.Show();
            }
            else
            {
                frmReport.BringToFront();
            }
        }

        void frmReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmReport.Dispose();
            frmReport = null;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (frmView == null)
            {
                frmView = new View();
                frmView.FormClosed += new FormClosedEventHandler(frmView_FormClosed);
                frmView.MdiParent = this;
                frmView.Show();
            }
            else
            {
                frmView.BringToFront();
            }
        }

        void frmView_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmView.Dispose();
            frmView = null;
        }



      
     
    }
}
