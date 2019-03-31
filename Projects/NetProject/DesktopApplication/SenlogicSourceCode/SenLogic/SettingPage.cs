using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace SenLogic
{
    public partial class SettingPage : Form
    {
        #region " Common Varible "
        DBEngine objDB = null;
        DataSet dtCompany = null;
        long CompanyId;
        string FilePath;
        #endregion

        public SettingPage()
        {
            InitializeComponent();
            txtCaliberationDate.Text = DateTime.Now.ToString(Shared_Variables.DateFormat);
            ApplyScreenValues();
        }


        private void ApplyScreenValues()
        {
            DataTable screenValues = GetScreenValues();
            if (screenValues.Rows != null && screenValues.Rows.Count > 0)
            {
                txtScreen1.Text = screenValues.Rows[0][0].ToString();
                txtScreen2.Text = screenValues.Rows[0][1].ToString();
                txtReport1.Text = screenValues.Rows[0][2].ToString();
            }
        }
        #region SourcePage
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var db = new DBEngine();
            string insertQuery = "insert into source(SourName) values('{0}')";

            var cmd = db.CreateCommand(string.Format(insertQuery,txtSource.Text));
            cmd.ExecuteNonQuery();        
            txtSource.Clear();
            MessageBox.Show("Record is Saved Successfully");
            LoadSourceList();
            
        }       

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadSourceList();
        }
       
        private void LoadSourceList()
        {
            lstSourceList.Items.Clear();
            lstSourceList.Columns.Clear();
            lstSourceList.Columns.Add("SOURCE",300);
            foreach (DataRow src in GetSourceList().Rows)
            {
                lstSourceList.Items.Add(src[0].ToString());                
            }
        }
        private DataTable GetSourceList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select SourName from Source", null, dtVendor, "Vendors");
            return dtVendor.Tables[0];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {           

            int count = lstSourceList.CheckedIndices.Count;
            if (count == 0)
                return;

            var selectedItems = lstSourceList.CheckedItems.Cast<ListViewItem>().Select(x => x.Text);

            string items = getSelectedItemsWithDelimiter(selectedItems);
            
            string deleteQuery = "delete * from source where SourName in ('{0}')";

            using(var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(deleteQuery, items));
            }           
            MessageBox.Show("Delete The Selected Record Successfully");
            LoadSourceList();
        }

        private static string getSelectedItemsWithDelimiter(IEnumerable<string> selectedItems)
        {
            string items = string.Empty;
            string delemiter = "','";

            foreach (string s in selectedItems)
            {
                if (items == string.Empty)
                    items = s;
                else
                    items = items + delemiter + s;
            }
            return items;
        }
        #endregion

        #region Destination
        private void btnDestAdd_Click(object sender, EventArgs e)
        {
            var db = new DBEngine();
            string insertQuery = "insert into Destination(DestName) values('{0}')";

            var cmd = db.CreateCommand(string.Format(insertQuery, txtDestination.Text));
            cmd.ExecuteNonQuery();
            txtDestination.Clear();
            MessageBox.Show("Record is Saved Successfully");
            LoadDestList();
        }

        private void btnDestView_Click(object sender, EventArgs e)
        {
            LoadDestList();
        }

        private void LoadDestList()
        {
            lstDestination.Items.Clear();
            lstDestination.Columns.Clear();
            lstDestination.Columns.Add("DESTINATION", 400);
            foreach (DataRow src in GetDestList().Rows)
            {
                lstDestination.Items.Add(src[0].ToString());
            }
        }


        private DataTable GetScreenValues()
        {
            //Select Screen1,Screen2,Report1 from ScreenCommon

            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Screen1,Screen2,Report1 from ScreenCommon", null, dtVendor, "ScreenValues");
            return dtVendor.Tables[0];

        }

        private DataTable GetDestList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select DestName from Destination", null, dtVendor, "Destination");
            return dtVendor.Tables[0];
        }

        private void btnDestDelete_Click(object sender, EventArgs e)
        {
            if(lstDestination.CheckedIndices.Count == 0)
                return;
            
            var items = lstDestination.CheckedItems.Cast<ListViewItem>().Select(x => x.Text);
            string selectedItems = getSelectedItemsWithDelimiter(items);

            using(var db = new DBEngine())
            {
               db.ExecuteNonQuery("delete * from Destination where DestName in ('"+ selectedItems +"')");
            }           
            MessageBox.Show("Delete The Selected Record Successfully");
            LoadDestList();
        }
        #endregion

        #region WagonLibrary
        private void btnWagonLibAdd_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into WagonMaster(WagonNo,CaliDate,WagonTare,WagonCC) values('{0}','{1}','{2}','{3}')";
            using(var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(insertQuery, txtWagonNo.Text, txtCaliberationDate.Text, txtPrintedTareWt.Text, txtCaringCapacity.Text));
            }
           
            txtDestination.Clear();
            MessageBox.Show("Record is Saved Successfully");
            LoadWagonList();
        }

        private void btnWagLibView_Click(object sender, EventArgs e)
        {
            LoadWagonList();
        }

        private void bntWagLibDelete_Click(object sender, EventArgs e)
        {
            //if (lstDestination.SelectedItem == null)
            //    return;
            var db = new DBEngine();
           // var cmd = db.CreateCommand("delete * from WagonMaster where WagonNo='" + lstDestination.SelectedItem.ToString().Trim() + "'");
            //cmd.ExecuteNonQuery();
            MessageBox.Show("Delete The Selected Record Successfully");
            GetWagonMasterList();
        }

        private void LoadWagonList()
        {
            dataWagLibGrid.DataSource = GetWagonMasterList();
        }

        private DataTable GetWagonMasterList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select WagonNo,CaliDate,WagonTare,WagonCC from WagonMaster", null, dtVendor, "WagonMaster");
            return dtVendor.Tables[0];
        }
#endregion

        #region Wagon
        private void btnWagonAdd_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into Wagon(Wtype,Wpcclimit,Wtol) values('{0}','{1}','{2}')";
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(insertQuery, txtWagontype.Text, txtPccLimit.Text, txtTolerance.Text));
            }

            txtWagontype.Clear();
            txtPccLimit.Clear();
            txtTolerance.Clear();

            MessageBox.Show("Record is Saved Successfully");
            LoadWagList();
        }

        private void btnWagonView_Click(object sender, EventArgs e)
        {
            LoadWagList();
        }

        private void btnWagonDelete_Click(object sender, EventArgs e)
        {
            if (lstWagonList.CheckedIndices.Count == 0)
                return;

            var items = lstWagonList.CheckedItems.Cast<ListViewItem>().Select(x => x.Text);
            var selectedItems = getSelectedItemsWithDelimiter(items);

            var db = new DBEngine();            
            db.ExecuteNonQuery("delete * from Wagon where WType in ('" + selectedItems + "')");
            MessageBox.Show("Delete The Selected Record Successfully");
            LoadWagList();
        }

        private void LoadWagList()
        {
            lstWagonList.Items.Clear();
            lstWagonList.Columns.Clear();
            lstWagonList.Columns.Add("WTYPE",150);
            lstWagonList.Columns.Add("WPCCLIMIT",150);
            lstWagonList.Columns.Add("WTOL",100);
            int i = 0;
            foreach (DataRow src in GetWagonList().Rows)
            {
                lstWagonList.Items.Add(src[0].ToString());
                lstWagonList.Items[i].SubItems.Add(src[1].ToString());
                lstWagonList.Items[i].SubItems.Add(src[2].ToString());
                i++;
            }
        }

        private DataTable GetWagonList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Wtype,Wpcclimit,Wtol from Wagon", null, dtVendor, "Wagon");
            return dtVendor.Tables[0];
        }

        #endregion

        #region Screen
        private void btnScreenUpdate_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into ScreenCommon(Screen1,Screen2,Report1) values('{0}','{1}','{2}')";
            string updateQuery = "update ScreenCommon set Screen1='{0}', Screen2='{1}', Report1='{2}'";
            string selectQuery = "select count(*) from ScreenCommon";
            int cnt = 0;
            string query = string.Empty;
            using (var db = new DBEngine())
            {
               cnt=(int)db.ExecuteScalar(string.Format(selectQuery),null);
            }
            if (cnt > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, txtScreen1.Text, txtScreen2.Text, txtReport1.Text));
            }

            MessageBox.Show("Successfully Updated");

            txtScreen1.Clear();
            txtScreen2.Clear();
            txtReport1.Clear();

            ApplyScreenValues();
        }
        #endregion

        #region wagonTypePccLimitUpdate
        private void btnPccLimitUpdate_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into ScreenCommon(PccLimit) values('{0}')";
            string updateQuery = "update ScreenCommon set PccLimit='{0}'";
            string selectQuery = "select count(*) from ScreenCommon";
            int cnt = 0;
            string query = string.Empty;
            string value = string.Empty;

            if(radPccFixedDisable.Checked)
                value = radPccFixedDisable.Text;
            if(radPccFixedEnable.Checked)
                value=radPccFixedEnable.Text;
            if(radPccFixedType.Checked)
                value=radPccFixedType.Text;

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
                db.ExecuteNonQuery(string.Format(query, value));
            }
        }
        #endregion

        #region Commodity
        private void btnCommodityAdd_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into Product(ProdName) values('{0}')";         
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(insertQuery, txtCommodityProduct.Text));
            }
            MessageBox.Show("Commodity Product Saved Successfully");
            txtCommodityProduct.Clear();
            LoadCommodityProductList();
        }

        private void LoadCommodityProductList()
        {
            lstCommodityView.Items.Clear();
            lstCommodityView.Columns.Clear();
            lstCommodityView.Columns.Add("COMMODITY", 300);
            foreach (DataRow src in GetProductCommodityList().Rows)
            {
                lstCommodityView.Items.Add(src[0].ToString());
            }
        }
        private DataTable GetProductCommodityList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("select ProdName from Product", null, dtVendor, "Product");
            return dtVendor.Tables[0];
        }

        private void btnCommodityView_Click(object sender, EventArgs e)
        {

            LoadCommodityProductList();
        }

        private void btnCommodityDelete_Click(object sender, EventArgs e)
        {
            if(lstCommodityView.CheckedIndices.Count == 0)
                return;

            var items = lstCommodityView.CheckedItems.Cast<ListViewItem>().Select(x => x.Text);
            string selectItems = getSelectedItemsWithDelimiter(items);

            string deleteQuery = "delete *  from Product where prodname in ('{0}')";
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(deleteQuery, selectItems));
            }
            MessageBox.Show("Selected Item Deleted Successfully");
            LoadCommodityProductList();

        }
        #endregion

        private void txtSystemPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                if (txtSystemPwd.Text == "aditya")
                {
                    this.Hide();
                    new SystemSettings().Show();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmDefaultImage().Show();
        }

        private void btnLoginOk_Click(object sender, EventArgs e)
        {
            string updateQuery = "update Login set Pwd='{0}' where LoginType='{1}' and UserName='{2}'";

            string LoginType = string.Empty;    

            if (!chkLogin.Checked && !chkCalib.Checked)
                return;
           
            if(txtCurrentPwd.Text != txtConfirmPwd.Text)
                return;

            if (chkLogin.Checked)
                LoginType = chkLogin.Text;
            else
                LoginType = chkCalib.Text;

            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(updateQuery, txtCurrentPwd.Text, LoginType, txtUserName.Text));
            }
            MessageBox.Show("Successfully Updated");
        }

        private void txtOldPwd_MouseEnter(object sender, EventArgs e)
        {
            int cnt = 0;
            string selectQuery = "Select count(*) from Login where Pwd='{0}' and UserName='{1}'";
            using (var db = new DBEngine())
            {
                cnt = (int)db.ExecuteScalar(string.Format(selectQuery, txtOldPwd.Text, txtUserName.Text),null);
            }
            if(cnt <=0)
            {
                txtUserName.Clear();
                txtOldPwd.Clear();
                chkCalib.Checked = false;
                chkLogin.Checked = false;
            }
        }


        private void EnableDisblePanel(int panelNo)
        {
            switch (panelNo)
            {
                case 1:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = true;
                    break;
                case 2:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = true;
                    pnlSystemSetup.Visible = false;
                    break;
                case 3:
                    pnlLPS.Visible = true;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                case 4:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = true;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                case 5:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = true;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                case 6:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = true;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                case 7:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = true;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                case 8:
                    pnlSpeedSetup.Visible = false;
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = true;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                case 10:
                    pnlSpeedSetup.Visible = true;
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;
                default:
                    pnlLPS.Visible = false;
                    pnlMergeSetup.Visible = false;
                    pnlOLT.Visible = false;
                    pnlOverLoadSummarry.Visible = false;
                    pnlGuardVan.Visible = false;
                    pnlEngineElimination.Visible = false;
                    pnlSpeedSetup.Visible = false;
                    pnlPrintSetup.Visible = false;
                    pnlSystemSetup.Visible = false;
                    break;

            }
        }

        private void btnSystemSetup_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(1);
            AssignSystemValues();
            var dtPorting = Shared_Class.LoadPortingValue();
            if (dtPorting.Tables[0] != null && dtPorting.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][1].ToString()))
            {
                lblCommunicationPort.Text =lblCommunicationPort.Text.Trim() + dtPorting.Tables[0].Rows[0][0].ToString().Replace("Com","");
                lblBaudRate.Text = lblBaudRate.Text.Trim() + dtPorting.Tables[0].Rows[0][1].ToString();
            }
        }

        private void AssignSystemValues()
        {
            //System,Model,Serial,Supply,Scount,Fcount,Zmv,Fmv,Dcount,Dips,Cs,Cw,Cd,Sd,Tswitch,Raildis,Sw14,Sw45

            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select System,Model,Serial,Supply,Scount,Fcount,Zmv,Fmv,Dcount,Dips,Cs,Cw,Cd,Sd,Tswitch,Raildis,Sw14,Sw45 from System", null, dtVendor, "Wagon");
           
            txtSystem.Text = dtVendor.Tables[0].Rows[0]["System"].ToString();
            txtModel.Text = dtVendor.Tables[0].Rows[0]["Model"].ToString();
            txtSerialNo.Text = dtVendor.Tables[0].Rows[0]["Serial"].ToString();
            txtSupply.Text = dtVendor.Tables[0].Rows[0]["Supply"].ToString();
                       

            txtZeroCount.Text = dtVendor.Tables[0].Rows[0]["Scount"].ToString();
            txtFullCount.Text = dtVendor.Tables[0].Rows[0]["Fcount"].ToString();
            txtDriffCount.Text = dtVendor.Tables[0].Rows[0]["Dcount"].ToString();
            txtDipSwitch.Text = dtVendor.Tables[0].Rows[0]["Dips"].ToString();
            
            txtCalSwitch.Text = dtVendor.Tables[0].Rows[0]["Cs"].ToString();
            txtCalWt.Text = dtVendor.Tables[0].Rows[0]["Cw"].ToString();
            txtZeroMv.Text = dtVendor.Tables[0].Rows[0]["Zmv"].ToString();
            txtFullMv.Text = dtVendor.Tables[0].Rows[0]["Fmv"].ToString();

            txtCalibDate.Text = dtVendor.Tables[0].Rows[0]["Cd"].ToString();
            txtStampDue.Text = dtVendor.Tables[0].Rows[0]["Sd"].ToString();


            txtTrSwitch.Text = dtVendor.Tables[0].Rows[0]["Tswitch"].ToString();
            txtRailMtr.Text = dtVendor.Tables[0].Rows[0]["Raildis"].ToString();
            txtSw1.Text = dtVendor.Tables[0].Rows[0]["Sw14"].ToString();
            txtSw4.Text = dtVendor.Tables[0].Rows[0]["Sw45"].ToString();
        }

        private void btnPrintSetup_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(2);
        }

        private void btnPasswordSetup_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(3);
        }

        private void btnEngineElimination_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(4);
        }

        private void btnMergeSetup_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(5);
        }

        private void btnSpeedSetup_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(10);
        }

        private void btnOverLoadTolerance_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(6);
        }

        private void btnGuardVan_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(7);
        }

        private void btnOverLoadSummary_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(8);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            EnableDisblePanel(9);
        }

        private void SettingPage_Load(object sender, EventArgs e)
        {
            btnDestAdd.Enabled = false;
            btnWagonAdd.Enabled = false;
            btnCommodityAdd.Enabled = false;
            btnAdd.Enabled = false;
            btnWagonLibAdd.Enabled = false;
            EnableDisblePanel(9);
            pnlDeletePwd.Visible = false;

        }

        private int ScreenCommonRecordCount()
        {
            string selectQuery = "select count(*) from ScreenCommon";
            int cnt = 0;
            using (var db = new DBEngine())
            {
                cnt = (int)db.ExecuteScalar(string.Format(selectQuery), null);
            }
            return cnt;
        }

        private void btnOLSok_Click(object sender, EventArgs e)
        {
            //radOLSEnable, radOLSDisable
            bool IsOLS = false;
            if (radOLSEnable.Checked)
                IsOLS = true;

            string query = string.Empty;            
            string insertQuery = "insert into ScreenCommon(IsOverLoadSummary) values('{0}')";
            string updateQuery = "update ScreenCommon set IsOverLoadSummary='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, IsOLS));
            }

        }

        private void btnGWNPOk_Click(object sender, EventArgs e)
        {
            //radGWEnable,radGWDisable

            bool IsGV = false;
            if (radGWEnable.Checked)
                IsGV = true;

            string query = string.Empty;
            string insertQuery = "insert into ScreenCommon(IsGuardVan) values('{0}')";
            string updateQuery = "update ScreenCommon set IsGuardVan='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, IsGV));
            }

        }

        private void btnOLTok_Click(object sender, EventArgs e)
        {
            //radOLTDisable, radOLTEnable

            bool IsOLT = false;
            if (radOLTEnable.Checked)
                IsOLT = true;

            string query = string.Empty;
            string insertQuery = "insert into ScreenCommon(IsOLT) values('{0}')";
            string updateQuery = "update ScreenCommon set IsOLT='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, IsOLT));
            }

        }

        private void btMOOk_Click(object sender, EventArgs e)
        {
            //radMODisable, radMOEnable

            bool IsMerge = false;
            if (radMOEnable.Checked)
                IsMerge = true;

            string query = string.Empty;
            string insertQuery = "insert into ScreenCommon(IsMerge) values('{0}')";
            string updateQuery = "update ScreenCommon set IsMerge='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, IsMerge));
            }

        }

        private void btnEEOk_Click(object sender, EventArgs e)
        {
            //radEEEnable,radEEDisable

            bool IsEE = false;
            if (radEEEnable.Checked)
                IsEE = true;

            string query = string.Empty;
            string insertQuery = "insert into ScreenCommon(IsEE) values('{0}')";
            string updateQuery = "update ScreenCommon set IsEE='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, IsEE));
            }

        }

        private void btnSpeedOk_Click(object sender, EventArgs e)
        {
            //radOSEnable, radOSdisable

            bool IsOverSpeed = false;
            if (radOSEnable.Checked)
                IsOverSpeed = true;

            string query = string.Empty;
            string insertQuery = "insert into ScreenCommon(IsOverSpeed) values('{0}')";
            string updateQuery = "update ScreenCommon set IsOverSpeed='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, IsOverSpeed));
            }

        }

        private void btnPrintOk_Click(object sender, EventArgs e)
        {
            //radDFP, radDUOCP, radDUOP, radDNP,radDOP,radDNPSP,radDCUOP2,radDTUOP3,radCNP,radCUOP,radSTCP

            string PrinterName = string.Empty;
            if (radDFP.Checked)
                PrinterName = radDFP.Text;
            if (radDUOCP.Checked)
                PrinterName = radDUOCP.Text;
            if (radDUOP.Checked)
                PrinterName = radDUOP.Text;
            if (radDNP.Checked)
                PrinterName = radDNP.Text;
            if (radDOP.Checked)
                PrinterName = radDOP.Text;
            if (radDNPSP.Checked)
                PrinterName = radDNPSP.Text;

            if (radDCUOP2.Checked)
                PrinterName = radDCUOP2.Text;
            if (radDTUOP3.Checked)
                PrinterName = radDTUOP3.Text;
            if (radCNP.Checked)
                PrinterName = radCNP.Text;
            if (radCUOP.Checked)
                PrinterName = radCUOP.Text;
            if (radSTCP.Checked)
                PrinterName = radSTCP.Text;

            string query = string.Empty;
            string insertQuery = "insert into ScreenCommon(PrinterName) values('{0}')";
            string updateQuery = "update ScreenCommon set PrinterName='{0}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, PrinterName));
            }

        }

        private void txtDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDestAdd.Enabled = true;
                btnDestAdd.Focus();
            }
        }

        private void txtCommodityProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnCommodityAdd.Enabled = true;
                btnCommodityAdd.Focus();
            }
        }

        private void txtWagontype_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPccLimit.Focus();
            }
        }

        private void txtPccLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTolerance.Focus();
            }
        }

        private void txtTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnWagonAdd.Enabled = true;
                btnWagonAdd.Focus();
            }
        }

        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAdd.Enabled = true;
                btnAdd.Focus();
                
            }
        }

        private void txtWagonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCaliberationDate.Focus();
            }
        }

        private void txtCaliberationDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPrintedTareWt.Focus();
            }
        }

        private void txtPrintedTareWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCaringCapacity.Focus();
            }
        }

        private void txtCaringCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnWagonLibAdd.Enabled = true;
                btnWagonLibAdd.Focus();
            }
        }

        private void txtDeletePwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtDeletePwd.Text)) return;
                if(txtDeletePwd.Text == "SENLOGIC")
                     pnlDeletePwd.Visible = true;
            }
        }

        private void txtDeletingPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtDeletePwd.Text)) return;


                if (MessageBox.Show("Do You Want To Delete Full Rake Information !!! ?", "DeleteWindow", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                string query = string.Format("Delete * from Temp where RakeNo={0}", txtDeletingPwd.Text);
                using (var db = new DBEngine())
                {
                    db.ExecuteNonQuery(query);
                    txtDeletingPwd.Clear();
                }
                MessageBox.Show("Rake Is Deleted");
            }

        }

        private void txtInitializePwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtInitializePwd.Text)) return;

                if(txtInitializePwd.Text == "ADI03")
                {
                    if (MessageBox.Show("Do You want To Initialize The Database !!! ?", "InitializeWindow", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    string query = string.Format("Delete * from Temp");
                    using (var db = new DBEngine())
                    {
                        db.ExecuteNonQuery(query);                        
                    }
                    MessageBox.Show("Successfully Initialized Database ...!!!");
                    pnlDeletePwd.Visible = false;
                }
            }
        }


        private string SourceFileLocation()
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory; 
            ofd.AddExtension = true;
            ofd.Title = "Select Backup Mdb File";
            ofd.DefaultExt = "mdb";
            ofd.Filter = "DataBaseFiles (*.mdb)|*.mdb";
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return string.Empty;
        }

        private string TaregetFileLocation()
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            sfd.AddExtension = true;
            sfd.Title = "Save Backup Mdb File";
            sfd.DefaultExt = "mdb";
            sfd.Filter = "DataBaseFiles (*.mdb)|*.mdb";
            sfd.CheckPathExists = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            return string.Empty;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You want To Take The Database Backup !!! ?", "Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string SourcePath = SourceFileLocation();
            string TargetPath = TaregetFileLocation();
            File.Copy(SourcePath, TargetPath);
            MessageBox.Show("SuccessFully Take The Selected Backup File");
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You want To Restore The Database Backup !!! ?", "Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string SourcePath = SourceFileLocation();
            string mdbPartialString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password=SDI08";
            string mdbConnectionString = string.Format(mdbPartialString, SourcePath);
            WeighmentDataRestore(mdbConnectionString);

        }

        private  void WeighmentDataRestore(string mdbConnectionString)
        {
            string toquery = string.Format("Select ID,Slno,Rakeno,Wagonno,Gross,Tare,Net,CC,PCC,UL,OL,POL,chwt,[from],[product],[to],dateout,timeout,wagontype,rdate,datein,timein,spd,spdremarks,direction from Temp");
            string fromQuery = "Select Slno,Rakeno,Wagonno,Gross,Tare,Net,CC,PCC,UL,OL,POL,chwt,[from],[product],[to],dateout,timeout,wagontype,rdate,datein,timein,spd,spdremarks,direction from Temp";

            var fromdt = new System.Data.DataTable();
            System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(mdbConnectionString);
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(fromQuery, cn);
            da.Fill(fromdt);

            var db = new DBEngine();
            var todt = new System.Data.DataTable();
            System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(db.ConnectionString);

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(toquery, con);
            System.Data.OleDb.OleDbDataAdapter toDa = new System.Data.OleDb.OleDbDataAdapter(cmd);
            System.Data.OleDb.OleDbCommandBuilder ocb = new System.Data.OleDb.OleDbCommandBuilder(toDa);

            da.Fill(todt);
            ocb.QuoteSuffix = "]";
            ocb.QuotePrefix = "[";



            for (int i = 0; i < fromdt.Rows.Count; i++)
            {
                DataRow dr = todt.NewRow();
                //Slno,Rakeno,Wagonno,Gross,Tare,Net,CC,PCC,UL,OL,POL,chwt,from,product,to,dateout,timeout,wagontype,rdate,datein,timein,spd,spdremarks,direction
                dr["Slno"] = fromdt.Rows[i]["Slno"].ToString();
                dr["Rakeno"] = fromdt.Rows[i]["Rakeno"].ToString();
                dr["Wagonno"] = fromdt.Rows[i]["Wagonno"].ToString();
                dr["Gross"] = fromdt.Rows[i]["Gross"].ToString();
                dr["Tare"] = fromdt.Rows[i]["Tare"].ToString();
                dr["Net"] = fromdt.Rows[i]["Net"].ToString();
                dr["CC"] = fromdt.Rows[i]["CC"].ToString();
                dr["PCC"] = fromdt.Rows[i]["PCC"].ToString();
                dr["UL"] = fromdt.Rows[i]["UL"].ToString();
                dr["OL"] = fromdt.Rows[i]["OL"].ToString();



                dr["POL"] = fromdt.Rows[i]["POL"].ToString();
                dr["chwt"] = fromdt.Rows[i]["chwt"].ToString();
                dr["from"] = fromdt.Rows[i]["from"].ToString();
                dr["product"] = fromdt.Rows[i]["product"].ToString();
                dr["to"] = fromdt.Rows[i]["to"].ToString();

                if (!string.IsNullOrEmpty(fromdt.Rows[i]["dateout"].ToString()))
                {
                    dr["dateout"] = fromdt.Rows[i]["dateout"].ToString();
                    dr["timeout"] = fromdt.Rows[i]["timeout"].ToString();
                }
                dr["wagontype"] = fromdt.Rows[i]["wagontype"].ToString();
                dr["rdate"] = fromdt.Rows[i]["rdate"].ToString();

                if (!string.IsNullOrEmpty(fromdt.Rows[i]["datein"].ToString()))
                {
                    dr["datein"] = fromdt.Rows[i]["datein"].ToString();
                    dr["timein"] = fromdt.Rows[i]["timein"].ToString();
                }
                dr["spd"] = fromdt.Rows[i]["spd"].ToString();
                dr["spdremarks"] = fromdt.Rows[i]["spdremarks"].ToString();
                dr["direction"] = fromdt.Rows[i]["direction"].ToString();

                todt.Rows.Add(dr);
            }
            toDa.Update(todt);
            MessageBox.Show("Weighment Data Saved Successfully");
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            pnlDeletePwd.Visible = false;
        }

        private void btnSettingsOk_Click(object sender, EventArgs e)
        {
            //System,Model,Serial,Supply,Scount,Fcount,Zmv,Fmv,Dcount,Dips,Cs,Cw,Cd,Sd,Tswitch,Raildis,Sw14,Sw45
            //System,Model,Serial,Supply,Scount,Fcount,Zmv,Fmv,Dcount,Dips,Cs,Cw,Cd,Sd,Tswitch,Raildis,Sw14,Sw45 from System
            string query = "update System set [System]='{0}',[Model]='{1}',[Serial]='{2}',[Supply]='{3}',[Scount]='{4}',[Fcount]='{5}',Zmv='{6}',Fmv='{7}',Dcount='{8}',Dips='{9}',Cs='{10}',Cw='{11}',Cd='{12}',Sd='{13}',Tswitch='{14}',Raildis='{15}',Sw14='{16}',Sw45='{17}'";
            using (var db = new DBEngine())
            {
                db.ExecuteNonQuery(string.Format(query, txtSystem.Text, txtModel.Text, txtSerialNo.Text, txtSupply.Text, txtZeroCount.Text, txtFullCount.Text, txtZeroMv.Text, txtFullMv.Text, txtDriffCount.Text, txtDipSwitch.Text, txtCalSwitch.Text, txtCalWt.Text, txtCalibDate.Text, txtStampDue.Text, txtTrSwitch.Text, txtRailMtr.Text, txtSw1.Text,txtSw4.Text ));
            }
            MessageBox.Show("Successfully Updated");


        }

        private void btnSettingsPrint_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult confirm = MessageBox.Show("Are You Sure, You want to Print the records", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (confirm == DialogResult.Yes)
                {
                    Print();
                }
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message.ToString(), ex.Message.GetType().ToString());
            }
        }

        private void Print()
        {

            //PrinterSettings printerSettings = new PrinterSettings();
            //IQueryable<PaperSize> paperSizes = printerSettings.PaperSizes.Cast<PaperSize>().AsQueryable();
            //PaperSize a4rotated = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A3).First();

            //var psettings = new PageSettings();
            //psettings.PrinterSettings = printerSettings;

            PrintDocument pd = new PrintDocument();
            //pd.DefaultPageSettings = psettings;
            pd.PrintPage += new PrintPageEventHandler(PrintImage);

            pd.Print();

            //PrintPreviewDialog pview = new PrintPreviewDialog();
            //pview.Document = pd;
            //pview.ShowDialog();


        }
        void PrintImage(object o, PrintPageEventArgs e)
        {
            try
            {

                int width = 200;
                int height = 100;
                int origin = 100;

                int GRDH = 25;
                int y = 0;



                int Corigin = 60;

                int GNRDW = 100;



                StringFormat str = new StringFormat();
                str.Alignment = StringAlignment.Near;
                str.LineAlignment = StringAlignment.Center;
                str.Trimming = StringTrimming.EllipsisCharacter;
                Pen dashed_pen = new Pen(Color.Black, 1.5f);

                var dt = new DataSet();
                new DBEngine().ExecuteQuery("select Report1 from ScreenCommon", null, dt, "ReportHeader");
                string[] headers = dt.Tables[0].Rows[0][0].ToString().Split(',');

                string dotLine = "-----------------------------------------------------------------------------------------------------------";

                if (headers.Length > 1)
                {
                    e.Graphics.DrawString(headers[0], Shared_Variables.GetArialFont, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 400, GRDH), str);
                    height += 20;
                    e.Graphics.DrawString(headers[1], Shared_Variables.GetArialFont, Brushes.Black, new RectangleF(350 - headers[1].Length, height, width + 300, GRDH), str);
                }
                else
                    e.Graphics.DrawString(headers[0], Shared_Variables.GetArialFont, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 300, GRDH), str);

                height += 20;
                e.Graphics.DrawString(dotLine, Shared_Variables.GetArialFont, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                height += 20;

                e.Graphics.DrawString("System:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtSystem.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Cal Wt:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtCalWt.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Model:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
                e.Graphics.DrawString(txtModel.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Zero Mv:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtZeroMv.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Serial No:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtSerialNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Full Mv:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtFullMv.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Supply:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtSupply.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Calib Date:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtCalibDate.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Zero Count:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtZeroCount.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Stamp Due:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtStampDue.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Full Count:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtFullCount.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Tr Switch:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtTrSwitch.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Drift Count:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtDriffCount.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Rail Mtr:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtRailMtr.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Dip Switch:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtDipSwitch.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Sw 1 - 4:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtSw1.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Cal Switch:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtCalSwitch.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Sw 4 - 5:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtSw4.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

                height += 20;


                e.Graphics.DrawString(dotLine, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                height += 60;
              
                e.Graphics.DrawString("All Weights are in Tonnes", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 100, GRDH), str);
                e.Graphics.DrawString("Authorized Signature", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);

            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

    }
}
