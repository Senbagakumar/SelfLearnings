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
    public partial class frmManualTareEdit : Form
    {
        #region " Common Varible "
        DBEngine objDB = null;
        DataSet dtVendor = null;
        DataSet dtCustomer = null;
        DataSet dtVehicle = null;
        DataSet dtCompany = null;
        DataSet dtPlace = null;
        DataSet dtMaterial = null;
        int CompanyId;
        long SelectMaterialId = 0;

        #endregion

        public frmManualTareEdit()
        {
            InitializeComponent();
        }

        private DataTable GetRakeNoList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Distinct RakeNo from temp where Tare is not null", null, dtVendor, "Rake");
            return dtVendor.Tables[0];
        }
        private void LoadSourceList()
        {
            lstRakeNumber.Items.Clear();
            // lstRakeNumber.Items.Add("Select Rake No");
            foreach (DataRow src in GetRakeNoList().Rows)
            {
                lstRakeNumber.Items.Add(src[0].ToString());
            }

            if (lstRakeNumber.Items.Count > 0)
                lstRakeNumber.SelectedIndex = lstRakeNumber.Items.Count - 1;

            lstRakeNumber.Focus();
        }

        private void frmManualTareEdit_Load(object sender, EventArgs e)
        {
            //LoadSourceList();
            this.KeyUp += frmManualTareEdit_KeyUp;
        }

        void frmManualTareEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                LoadSourceList();
                lstRakeNumber.Visible = true;
                lstRakeNumber.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                lstRakeNumber.Visible = false;
            }
        }

        private void lstRakeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (lstRakeNumber.SelectedItem != null)
                {
                    txtRakeNo.Text = lstRakeNumber.SelectedItem.ToString();
                    txtRakeNo.Focus();
                    lstRakeNumber.Visible = false;
                }
            }
        }

        private void txtRakeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtRakeNo.Text))
                {
                    string query = string.Format("Select slno,wagonno,wagontype,tare,pcc,cc,gross,net,UL,OL from temp where RakeNo={0} and Tare is Not null order by val(slno)", txtRakeNo.Text);
                    var db = new DBEngine();
                    var dtVendor = new DataSet();
                    db.ExecuteQuery(query, null, dtVendor, "Rake");

                    dgRakeView.DataSource = dtVendor.Tables[0];
                    AssignDefaultValueForEmptyColumn();

                    //dgRakeView.Columns["datein"].Visible = false;
                    //dgRakeView.Columns["timein"].Visible = false;
                    txtWagonNo.Focus();
                    txtSlno.Text = "1";

                }
            }
        }
        private void AssignDefaultValueForEmptyColumn()
        {           
            dgRakeView.Columns[3].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[4].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[5].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[6].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[7].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[8].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[9].DefaultCellStyle.Format = "00.00";

        }

        private void txtSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtWagonNo.Focus();
            }
        }

        private void txtWagonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTareWt.Focus();
            }
        }

        private void txtTareWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCCWt.Focus();
            }
        }

        private void txtCCWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int rownumb=Convert.ToInt16(txtSlno.Text);
            rownumb = rownumb - 1;

            

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgRakeView.Rows.Count -1 > rownumb)
                {
                    //slno,wagonno,wagontype,tare,pcc,cc,gross,net,datein,timein,UL,OL

                    if (string.IsNullOrEmpty(txtTareWt.Text) || txtTareWt.Text.Trim() == ".")
                        txtTareWt.Text = "00.00";

                    if (string.IsNullOrEmpty(txtCCWt.Text) || txtCCWt.Text.Trim() == ".")
                        txtCCWt.Text = "00.00";


                    dgRakeView.Rows[rownumb].Cells["wagonno"].Value = txtWagonNo.Text;
                    dgRakeView.Rows[rownumb].Cells["tare"].Value = txtTareWt.Text;
                    dgRakeView.Rows[rownumb].Cells["cc"].Value = txtCCWt.Text;
                    dgRakeView.Rows[rownumb].Cells["pcc"].Value = txtCCWt.Text;

                    double pccValue = (double)dgRakeView.Rows[rownumb].Cells["pcc"].Value;
                    double grossValue = (double)dgRakeView.Rows[rownumb].Cells["gross"].Value;

                    double tarevalue = (double)dgRakeView.Rows[rownumb].Cells["tare"].Value;
                    dgRakeView.Rows[rownumb].Cells["net"].Value = (grossValue - tarevalue).ToString();

                    //dgRakeView.Rows[rownumb].Cells["net"].Value = (grossValue - Convert.ToDouble(txtTareWt.Text)).ToString();

                    double netValue = (double)dgRakeView.Rows[rownumb].Cells["net"].Value;

                    if (pccValue < netValue)
                    {
                        dgRakeView.Rows[rownumb].Cells["OL"].Value = Math.Round(netValue - pccValue, 2);
                        dgRakeView.Rows[rownumb].Cells["UL"].Value = "0.0";
                    }
                    else
                    {
                        dgRakeView.Rows[rownumb].Cells["UL"].Value = Math.Round(pccValue - netValue, 2);
                        dgRakeView.Rows[rownumb].Cells["OL"].Value = "0.0";
                    }

                    //  dgRakeView.Rows[rownumb].Cells[7].Value = (pccValue - Convert.ToDouble(txtTareWt.Text)).ToString();

                    DateTime dt = DateTime.Now;

                    //  dgRakeView.Rows[rownumb].Cells["datein"].Value = dt.ToString("dd/MM/yyyy");
                    //  dgRakeView.Rows[rownumb].Cells["timein"].Value = dt.ToString("hh:mm:ss");

                    txtWagonNo.Clear();
                    txtCCWt.Clear();
                    txtTareWt.Clear();
                    txtWagonNo.Focus();

                    txtSlno.Text = (rownumb + 2).ToString();

                    rownumb++;
                }

                if (dgRakeView.Rows.Count - 1 == rownumb)
                {
                    if (MessageBox.Show("Do you want to Save all the records", "TareEntry", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        btnUpdate.Enabled = true;
                        btnUpdate.Focus();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Update all the records", "TareEntry", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               
                DataTable resultData = (DataTable)dgRakeView.DataSource;

                string query = string.Format("Select ID,Slno,RakeNo,WagonNo,WagonType,Tare,Gross,dateout,timeout,spd,PCC,[From],[Product],[To],Net,UL,OL,CC from Temp where RakeNo={0} order by val(Slno)", txtRakeNo.Text);
                var db = new DBEngine();
                btnUpdate.Visible = false;
                var dt = new System.Data.DataTable();
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(db.ConnectionString);

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(query, cn);
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(cmd);
                System.Data.OleDb.OleDbCommandBuilder ocb = new System.Data.OleDb.OleDbCommandBuilder(da);
                //cn.Open();
                da.Fill(dt);
                ocb.QuoteSuffix = "]";
                ocb.QuotePrefix = "[";

                //  var insertBuilder = new StringBuilder();

                for (int i = 0; i < resultData.Rows.Count; i++)
                {
                    //DataRow dr = dt.NewRow();
                    //dr["Slno"] = dttemp.Rows[i][0];
                    //dr["RakeNo"] = txtRakeNo.Text;
                    //dr["WagonType"] = txtWagonType.Text;
                    dt.Rows[i]["WagonNo"] = resultData.Rows[i][1];
                    dt.Rows[i]["Tare"] = resultData.Rows[i]["tare"];
                    dt.Rows[i]["Net"] = resultData.Rows[i]["net"];
                    //dt.Rows[i]["datein"] = resultData.Rows[i]["datein"];
                    //dt.Rows[i]["timein"] = resultData.Rows[i]["timein"];
                    dt.Rows[i]["UL"] = resultData.Rows[i]["UL"];
                    dt.Rows[i]["OL"] = resultData.Rows[i]["OL"];
                    dt.Rows[i]["CC"] = resultData.Rows[i]["CC"];
                    dt.Rows[i]["PCC"] = resultData.Rows[i]["PCC"];


                }
                da.Update(dt);

                MessageBox.Show("Successfully Updated");
                dgRakeView.DataSource = null;
                txtRakeNo.Clear();
                txtSlno.Clear();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmDefaultImage().Show();
        }

    }
}
