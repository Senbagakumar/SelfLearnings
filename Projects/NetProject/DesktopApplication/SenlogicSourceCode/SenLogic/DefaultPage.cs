using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SenLogic
{
    public partial class frmDefaultImage : Form
    {
        #region " Common Varible "
        DBEngine objDB = null;
        DataSet dtVendor = null;
        DataSet dtCustomer = null;
        DataSet dtVehicle = null;
        DataSet dtCompany = null;
        DataSet dtPlace = null;
        
        int CompanyId;
        long SelectCompanyId = 0;

        #endregion
        public frmDefaultImage()
        {
            InitializeComponent();
            LoadSourceList();
            LoadDestList();
            LoadCommodityProductList();
            LoadWagList();
        }

        private void SaveDefaultValue()
        {
            string insertQuery = "insert into [Default](Product, Source,Destination,Type) values('{0}','{1}','{2}','{3}')";
            string updateQuery = "update [Default] set Product='{0}', Source='{1}', Destination='{2}', Type='{3}'";
            string selectQuery = "select count(*) from [Default]";
            int cnt = 0;
            string query = string.Empty;
            string value = string.Empty;

           

            using (var db = new DBEngine())
            {
                cnt = (int)db.ExecuteScalar(string.Format(selectQuery), null);
            }
            if (cnt > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, cmbCommodity.SelectedItem.ToString(), cmbSource.SelectedItem.ToString(),cmbDestination.SelectedItem.ToString(),lstWagonType.SelectedItem.ToString()));
            }
        }

        private bool LoadPortingValue()
        {
            var db = new DBEngine();
            var dtPorting = new DataSet();
            db.ExecuteQuery("Select IsStaticDynamic from ScreenCommon", null, dtPorting, "Porting");
            if (dtPorting.Tables[0] != null && dtPorting.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][0].ToString()))
            {
              
                if (!string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][0].ToString()))
                {
                    return Convert.ToBoolean(dtPorting.Tables[0].Rows[0][0].ToString());
                   
                }
            }
            return false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPortingSet())
            {
                SaveDefaultValue();
                this.Hide();
                if (LoadPortingValue())
                    new StaticWeighment().Show();                   
                else
                    new View().Show();
            }
            else
            {
                MessageBox.Show("Please Set the Settings First");                
            }
        }

        private bool IsPortingSet()
        {
            var db = new DBEngine();
            var dtPorting = new DataSet();
            db.ExecuteQuery("Select Commtrack1, commBaud1 from ScreenCommon", null, dtPorting, "Porting");
            if(dtPorting.Tables[0]!=null && dtPorting.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][1].ToString()))
                return true;
            else
                return false;
        }

        private void LoadWagList()
        {
            lstWagonType.Items.Clear();
            foreach (DataRow src in GetWagonList().Rows)
            {
                lstWagonType.Items.Add(src[0].ToString());
            }
        }

        private DataTable GetWagonList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Wtype from Wagon", null, dtVendor, "Wagon");
            return dtVendor.Tables[0];
        }

        private void LoadCommodityProductList()
        {
            cmbCommodity.Items.Clear();
            foreach (DataRow src in GetProductCommodityList().Rows)
            {
                cmbCommodity.Items.Add(src[0].ToString());
            }
        }
        private DataTable GetProductCommodityList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("select ProdName from Product", null, dtVendor, "Product");
            return dtVendor.Tables[0];
        }

        private void LoadDestList()
        {
            cmbDestination.Items.Clear();
            foreach (DataRow src in GetDestList().Rows)
            {
                cmbDestination.Items.Add(src[0].ToString());
            }
        }

        private DataTable GetDestList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select DestName from Destination", null, dtVendor, "Destination");
            return dtVendor.Tables[0];
        }

        private void LoadSourceList()
        {
            cmbSource.Items.Clear();
            foreach (DataRow src in GetSourceList().Rows)
            {
                cmbSource.Items.Add(src[0].ToString());
            }
        }
        private DataTable GetSourceList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select SourName from Source", null, dtVendor, "Vendors");
            return dtVendor.Tables[0];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            pnlUtilities.Visible = false;
            pnlDefault.Visible = true;
        }


        private void CompanyName_Load(object sender, EventArgs e)
        {
            pnlImage.Hide();

            if (cmbSource.Items.Count > 0)
                cmbSource.SelectedIndex = 0;
            if (cmbDestination.Items.Count > 0)
                cmbDestination.SelectedIndex = 0;
            if (cmbCommodity.Items.Count > 0)
                cmbCommodity.SelectedIndex = 0;

            pnlDefault.Visible = false;
            pnlUtilities.Visible = true;

            if (lstWagonType.Items.Count > 0)
                lstWagonType.SelectedIndex = 0;

            lblCommodity.Text = string.Empty;
            lblDestination.Text = string.Empty;
            lblSource.Text = string.Empty;
            lblWagonType.Text = "******";
        }
      

        private void btnUtilites_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SettingPage().Show();
        }
               

        private void btnWagonEntry_Click(object sender, EventArgs e)
        {
            pnlUtilities.Visible = false;
            pnlDefault.Visible = true;
            pnlWagonEntryMenu.Visible = true;           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            pnlUtilities.Visible = false;
            pnlDefault.Visible = true;
            pnlImage.Show();          
        }

        private void btnWagonEntry_Leave(object sender, EventArgs e)
        {
          //  pnlWagonEntryMenu.Visible = false;
        }

        private void btnWagonEntry_MouseLeave(object sender, EventArgs e)
        {
            //btnWagonEntry_Leave(sender, e);
            pnlWagonEntryMenu.Visible = false;
        }

        private void pnlWagonEntryMenu_Enter(object sender, EventArgs e)
        {
            pnlWagonEntryMenu.Visible = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Report().Show();
        }

        private void btnConfirmExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            pnlImage.Hide();
        }

        private void btnMTEntry_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ManualTareEntry().Show();
        }

        private void cmbCommodity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                lblCommodity.Text = cmbCommodity.SelectedItem.ToString();
                cmbSource.Focus();
            }
        }

        private void cmbSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                lblSource.Text = cmbSource.SelectedItem.ToString();
                cmbDestination.Focus();
            }
        }

        private void cmbDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                lblDestination.Text = cmbDestination.SelectedItem.ToString();
                lstWagonType.Focus();
            }
        }

        private void lstWagonType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (lstWagonType.Items.Count > 0)
                {
                    if (lstWagonType.SelectedItem.ToString().Length <= 3)
                        lblWagonType.Text = lstWagonType.SelectedItem.ToString() + "        ";
                    else
                        lblWagonType.Text = lstWagonType.SelectedItem.ToString();

                    btnSave.Focus();
                }
            }
        }

        private void pnlWagonEntryMenu_MouseEnter(object sender, EventArgs e)
        {
            pnlWagonEntryMenu_Enter(sender, e);
        }

        private void cmbCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommodity.SelectedItem!=null)
                lblCommodity.Text = cmbCommodity.SelectedItem.ToString();
        }

        private void cmbDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDestination.SelectedItem != null)
                lblDestination.Text = cmbDestination.SelectedItem.ToString();
        }

        private void cmbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSource.SelectedItem != null)
                lblSource.Text = cmbSource.SelectedItem.ToString();
        }

        private void lstWagonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstWagonType.Items.Count > 0)
            {
                if (lstWagonType.SelectedItem.ToString().Length <= 3)
                    lblWagonType.Text = lstWagonType.SelectedItem.ToString() + "        ";
                else
                    lblWagonType.Text = lstWagonType.SelectedItem.ToString();

                
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            //Process.Start("Shutdown", "/s /t 0");
            var psi = new ProcessStartInfo("Shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
            Application.ExitThread();
            Application.Exit();

        }

        private void btnManualEdit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmManualTareEdit().Show();
        }
    }
}
