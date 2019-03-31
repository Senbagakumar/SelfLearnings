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
    public partial class ManualTareEntry : Form
    {
        public ManualTareEntry()
        {
            InitializeComponent();
        }
        private DataTable GetRakeNoList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Distinct RakeNo from temp where Tare=0", null, dtVendor, "Rake");
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

        private void ManualTareEntry_Load(object sender, EventArgs e)
        {
            //LoadSourceList();
            this.KeyUp += ManualTareEntry_KeyUp;
        }

        void ManualTareEntry_KeyUp(object sender, KeyEventArgs e)
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
                    string query = string.Format("Select slno,wagonno,wagontype,tare,pcc,cc,gross,net,UL,OL from temp where RakeNo={0} and Tare=0 order by val(slno)", txtRakeNo.Text);
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
            for (int i = 0; i<dgRakeView.Rows.Count - 1; i++)
            {
                //slno,wagonno,wagontype,tare,pcc,cc,gross,net,ul,ols

                //double pccValue =(double)dgRakeView.Rows[i].Cells["pcc"].Value;

               // dgRakeView.Rows[i].Cells["pcc"].Value = pccValue.ToString("00.00");
                dgRakeView.Rows[i].Cells["tare"].Value = "0";
                dgRakeView.Rows[i].Cells["CC"].Value = "0";
                dgRakeView.Rows[i].Cells["net"].Value="0";
            }
            dgRakeView.Columns[3].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[4].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[5].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[6].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[7].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[8].DefaultCellStyle.Format = "00.00";
            dgRakeView.Columns[9].DefaultCellStyle.Format = "00.00";
           
        }

        private void txtWagonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtWagonNo.Text = txtWagonNo.Text.ToString().ToUpper();
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
        int rownumb = 0; int rowno = 0;
        private void txtCCWt_KeyPress(object sender, KeyPressEventArgs e)
        {
             rownumb = Convert.ToInt16(txtSlno.Text);
                rownumb = rownumb - 1;

            if (e.KeyChar == (char)Keys.Enter)
            {
                
                if (dgRakeView.Rows.Count - 1 > rownumb)
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

                    double pccValue =(double)dgRakeView.Rows[rownumb].Cells["pcc"].Value;
                    double grossValue = (double)dgRakeView.Rows[rownumb].Cells["gross"].Value;
                    double tarevalue = (double)dgRakeView.Rows[rownumb].Cells["tare"].Value;


                    dgRakeView.Rows[rownumb].Cells["net"].Value = (grossValue - tarevalue).ToString();
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

                    //txtCCWt.Text = "50.00";
                    //txtTareWt.Text = "50.00";
                    //txtTareWt.Text = "HI";

                   // txtSlno.Text = (rownumb + 2).ToString();

                    //if ((rownumb + 2) > (rowno + 2 ))
                    //    txtSlno.Text = (rownumb + 2).ToString();
                    //else
                    //    txtSlno.Text = (rowno+1).ToString();

                    rownumb++;
                    rowno++;

                   // txtSlno.Text = (rownumb + 1).ToString();

                    if((rownumb+1) >= (rowno+1))
                    {
                        txtSlno.Text = (rownumb + 1).ToString();
                    }
                    else
                    {
                        txtSlno.Text = (rowno).ToString();
                        rownumb = rowno;
                        rowno = rowno - 1;
                    }


                }

                if (dgRakeView.Rows.Count - 1 == rowno)
                {
                    if(MessageBox.Show("Do you want to Save all the records","TareEntry",MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        btnSave.Focus();
                    }
                }

                
            }
           

        }



        private void Print()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Save all the records", "TareEntry", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //string insertQuery = string.Empty;
                //string updateQuery = string.Empty;
                DataTable resultData = (DataTable)dgRakeView.DataSource;
                //insertQuery = "insert into TempTare(Slno,RakeNo,WagonNo,WagonType,Tare,Gross,Net,PCC) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                //updateQuery = "update TempGross Set WagonNo='{0}',Tare='{1}',Net='{2}' where Slno={3} and RakeNo={4}";

                //foreach (DataRow dr in resultData.Rows)
                //{

                //    string insertBuilder = string.Format(insertQuery, dr[0],txtRakeNo.Text,dr[1],dr[2],dr[3],dr[5],dr[6],dr[4]);
                //    var db = new DBEngine();
                //    db.ExecuteNonQuery(insertBuilder);
                //    insertBuilder = string.Format(updateQuery, dr[1], dr[4], dr[6], dr[0], txtRakeNo.Text);
                //    db.ExecuteNonQuery(insertBuilder);
                    
                    
                //}
                string query = string.Format("Select ID,Slno,RakeNo,WagonNo,WagonType,Tare,Gross,dateout,timeout,spd,PCC,[From],[Product],[To],Net,UL,OL,CC from Temp where RakeNo={0} order by val(Slno)", txtRakeNo.Text);
                var db = new DBEngine();
                btnSave.Visible = false;
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

                for (int i=0; i<resultData.Rows.Count; i++)
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

        private void txtSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSlno.Text))
                return;
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtWagonNo.Focus();
            }
        }

      
    }
}
