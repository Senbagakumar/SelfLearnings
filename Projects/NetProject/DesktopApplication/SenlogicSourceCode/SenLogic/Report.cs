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
using System.Net.Mail;
using System.Drawing.Printing;
using System.Configuration;
using System.Drawing.Text;

namespace SenLogic
{

    public partial class Report : Form
    {
   
        public Report()
        {
            InitializeComponent();
            
           
        }
        private DataTable GetRakeNoList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select distinct RakeNo from Temp", null, dtVendor, "Temp");
            return dtVendor.Tables[0];
        }

        private  void GetRakeSourceList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select distinct RakeNo from Temp where Gross > 0.00 and Tare > 0.00", null, dtVendor, "Temp");

            lstRakeNumber.Items.Clear();
            // lstRakeNumber.Items.Add("Select Temp No");
            foreach (DataRow src in dtVendor.Tables[0].Rows)
            {
                lstRakeNumber.Items.Add(src[0].ToString());
            }
            lstRakeNumber.Focus();
        }

        private void LoadSourceList()
        {
            lstRakeNumber.Items.Clear();
           // lstRakeNumber.Items.Add("Select Temp No");
            foreach (DataRow src in GetRakeNoList().Rows)
            {
                lstRakeNumber.Items.Add(src[0].ToString());
            }
            lstRakeNumber.Focus();
        }

        private void LoadCommodityProductList()
        {
            cmbMaterialName.Items.Clear();
            cmbMaterialName.Items.Add("Select Commodity");
            foreach (DataRow src in GetProductCommodityList().Rows)
            {
                cmbMaterialName.Items.Add(src[0].ToString());
            }
            cmbMaterialName.Focus();
        }
        private DataTable GetProductCommodityList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("select ProdName from Product", null, dtVendor, "Product");
            return dtVendor.Tables[0];
        }

        private void btnRakeReport_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(1);
            GetRakeSourceList();
            
        }

        private void btnRakeSummary_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(2);            
            GetRakeSummaryDetails();
            chkTextFile.Visible = false;
        }

        private void LoadDestList()
        {
            cmbMaterialName.Items.Clear();
            cmbMaterialName.Items.Add("Select Destination");
            foreach (DataRow src in GetDestList().Rows)
            {
                cmbMaterialName.Items.Add(src[0].ToString());
            }
            cmbMaterialName.Focus();
        }

        private DataTable GetDestList()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select DestName from Destination", null, dtVendor, "Destination");
            return dtVendor.Tables[0];
        }

        private void btnMaterialReport_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(3);           
            LoadCommodityProductList();
            
        }

        private void btnDesReport_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(4);
            LoadDestList();
        }

        private void ClearControl()
        {
            txtFromDate.Clear();
            txtToDate.Clear();
            
        }

        private void btnDateWiseReport_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(5);
            txtFromDate.Focus();
        }

        private void btnWeighBill_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(6);
            LoadSourceList();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            EnableDisbleButton(7);
        }
        private void EnableDisbleButton(int btnNo)
        {
            switch (btnNo)
            {
                case 1:
                    btnWeighBill.Enabled = false;
                    btnDateWiseReport.Enabled = false;
                    btnDesReport.Enabled = false;
                    btnMaterialReport.Enabled = false;
                    btnRakeSummary.Enabled = false;
                    btnRakeReport.Enabled = true;

                    RakeControlsEnable();
                    
                    break;
                case 2:
                    btnWeighBill.Enabled = false;
                    btnDateWiseReport.Enabled = false;
                    btnDesReport.Enabled = false;
                    btnMaterialReport.Enabled = false;
                    btnRakeSummary.Enabled = true;
                    btnRakeReport.Enabled = false;
                    DisableControl();
                    break;
                case 3:
                    btnWeighBill.Enabled = false;
                    btnDateWiseReport.Enabled = false;
                    btnDesReport.Enabled = false;
                    btnMaterialReport.Enabled = true;
                    btnRakeSummary.Enabled = false;
                    btnRakeReport.Enabled = false;


                    MaterialControlsEnable();
                    ClearControl();

                    break;
                case 4:
                    btnWeighBill.Enabled = false;
                    btnDateWiseReport.Enabled = false;
                    btnDesReport.Enabled = true;
                    btnMaterialReport.Enabled = false;
                    btnRakeSummary.Enabled = false;
                    btnRakeReport.Enabled = false;

                    MaterialControlsEnable();
                    
                    ClearControl();

                    break;
                case 5:
                    btnWeighBill.Enabled = false;
                    btnDateWiseReport.Enabled = true;
                    btnDesReport.Enabled = false;
                    btnMaterialReport.Enabled = false;
                    btnRakeSummary.Enabled = false;
                    btnRakeReport.Enabled = false;

                    DateControlsEnable();

                    break;
                case 6:
                    btnWeighBill.Enabled = true;
                    btnDateWiseReport.Enabled = false;
                    btnDesReport.Enabled = false;
                    btnMaterialReport.Enabled = false;
                    btnRakeSummary.Enabled = false;
                    btnRakeReport.Enabled = false;
                    RakeControlsEnable();

                    break;
               
                default:
                    btnWeighBill.Enabled = true;
                    btnDateWiseReport.Enabled = true;
                    btnDesReport.Enabled = true;
                    btnMaterialReport.Enabled = true;
                    btnRakeSummary.Enabled = true;
                    btnRakeReport.Enabled = true;
                    dgRakeView.DataSource = null;
                    btnPrint.Enabled = false;
                    DisableControl();
                    break;

            }
        }

        private void DisableControl()
        {
         
            labMaterialName.Visible = false;
            labFromDate.Visible = false;
            labToDate.Visible = false;

            cmbMaterialName.Visible = false;
            txtFromDate.Visible = false;
            txtToDate.Visible = false;

            labRakeNo.Visible = false;
            txtRakeNo.Visible = false;
            labEsc.Visible = false;
            lstRakeNumber.Visible = false;
        
        }
        private void RakeControlsEnable()
        {
            labMaterialName.Visible = false;
            labFromDate.Visible = false;
            labToDate.Visible = false;

            cmbMaterialName.Visible = false;
            txtFromDate.Visible = false;
            txtToDate.Visible = false;

            labRakeNo.Visible = true;
            txtRakeNo.Visible = true;
            labEsc.Visible = true;
            lstRakeNumber.Visible = true;
        }

        private void MaterialControlsEnable()
        {
            labMaterialName.Visible = true;
            labFromDate.Visible = true;
            labToDate.Visible = true;

            cmbMaterialName.Visible = true;
            txtFromDate.Visible = true;
            txtToDate.Visible = true;

            labRakeNo.Visible = false;
            txtRakeNo.Visible = false;
            labEsc.Visible = false;
            lstRakeNumber.Visible = false;
        }

        private void DateControlsEnable()
        {
            labMaterialName.Visible = false;
            labFromDate.Visible = true;
            labToDate.Visible = true;

            cmbMaterialName.Visible = false;
            txtFromDate.Visible = true;
            txtToDate.Visible = true;

            labRakeNo.Visible = false;
            txtRakeNo.Visible = false;
            labEsc.Visible = false;
            lstRakeNumber.Visible = false;
        }

        private void Report_Load(object sender, EventArgs e)
        {
         
            btnPrint.Enabled = false;
            EnableDisbleButton(7);
            this.KeyUp += Report_KeyUp;
        }

        void Report_KeyUp(object sender, KeyEventArgs e)
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

   

        DataTable dttemp = null;
        private void AssignDefaultColumnsForRakeNoDetails()
        {
            //Slno, Wagon Type, Wagon No, C.Cap,P.C.C,Gross,Tare,Net,U.Load,O.Load
            dttemp = new DataTable();
            dttemp.Columns.Add("Slno");
            dttemp.Columns.Add("Wagon Type");
            dttemp.Columns.Add("Wagon No");
            dttemp.Columns.Add("C.Cap");
            dttemp.Columns.Add("P.C.C");
            dttemp.Columns.Add("Gross");
            dttemp.Columns.Add("Tare");
            dttemp.Columns.Add("Net");
            dttemp.Columns.Add("U.Load");
            dttemp.Columns.Add("O.Load");
            dgRakeView.DataSource = dttemp;
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00";
            foreach (DataGridViewColumn column in dgRakeView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            
        }

        private void GetRakeNoDetailsByRakeNo(string RakeNo)
        {
            
            var db = new DBEngine();
            var dtVendor = new DataSet();
            string rakeQuery = string.Format("Select Slno,WagonType As [Wagon Type],WagonNo As [Wagon No],CC As [C Cap],PCC,Gross,Tare,Net,UL AS [U Load],OL AS [O Load] from Temp where RakeNo={0} order by val(Slno)", RakeNo);
            db.ExecuteQuery(rakeQuery, null, dtVendor, "RakeList");
            //return dtVendor.Tables[0];
            //dttemp.Rows.Add(dtVendor.Tables[0].Rows);
            
            //dgRakeView.DataSource = dtVendor.Tables[0];

            var dtSum = new DataSet();
            string sumQuery = string.Format("Select sum(CC) As [C Cap], Sum(PCC) As PCC, Sum(Gross) As Gross, Sum(Tare) As Tare, Sum(Net) As Net, Sum(UL) As [U Load], Sum(OL) As [O Load] from Temp Where RakeNo={0}",RakeNo);
            db.ExecuteQuery(sumQuery, null, dtSum, "SumRakeList");

            if (dtVendor.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dtVendor.Tables[0].NewRow();
                dr[0] = "Total";
                dr[3] = dtSum.Tables[0].Rows[0][0].ToString();
                dr[4] = dtSum.Tables[0].Rows[0][1].ToString();
                dr[5] = dtSum.Tables[0].Rows[0][2].ToString();
                dr[6] = dtSum.Tables[0].Rows[0][3].ToString();
                dr[7] = dtSum.Tables[0].Rows[0][4].ToString();
                dr[8] = dtSum.Tables[0].Rows[0][5].ToString();
                dr[9] = dtSum.Tables[0].Rows[0][6].ToString();
                dtVendor.Tables[0].Rows.Add(dr);
            }

            dgRakeView.DataSource = dtVendor.Tables[0];

            //for (int i = 0; i < dtVendor.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dr = dttemp.NewRow(); dr["Slno"] = dtVendor.Tables[0].Rows[i]["Slno"].ToString();
            //    //Wagon Type, Wagon No, C.Cap,P.C.C,Gross,Tare,Net,U.Load,O.Load
            //    dr["Wagon Type"] = dtVendor.Tables[0].Rows[i]["WagonType"].ToString();
            //    dr["Wagon No"] = dtVendor.Tables[0].Rows[i]["WagonNo"].ToString();
            //    dr["C.Cap"] = dtVendor.Tables[0].Rows[i]["CC"].ToString();
            //    dr["P.C.C"] = dtVendor.Tables[0].Rows[i]["PCC"].ToString();
            //    dr["Gross"] = dtVendor.Tables[0].Rows[i]["Gross"].ToString();
            //    dr["Tare"] = dtVendor.Tables[0].Rows[i]["Tare"].ToString();
            //    dr["Net"] = dtVendor.Tables[0].Rows[i]["Net"].ToString();
            //    dr["U.Load"] = dtVendor.Tables[0].Rows[i]["UL"].ToString();
            //    dr["O.Load"] = dtVendor.Tables[0].Rows[i]["OL"].ToString();
                
            //    dttemp.Rows.Add(dr);

            //}
            if (dgRakeView.Rows.Count > 0)
            {
                btnPrint.Enabled = true;
                dgRakeView.FirstDisplayedScrollingRowIndex = dgRakeView.Rows.Count - 1;
                dgRakeView.Columns[3].DefaultCellStyle.Format = "00.00";
                dgRakeView.Columns[4].DefaultCellStyle.Format = "00.00";
                dgRakeView.Columns[5].DefaultCellStyle.Format = "00.00";
                dgRakeView.Columns[6].DefaultCellStyle.Format = "00.00";
                dgRakeView.Columns[7].DefaultCellStyle.Format = "00.00";
                dgRakeView.Columns[8].DefaultCellStyle.Format = "00.00";
                dgRakeView.Columns[9].DefaultCellStyle.Format = "00.00";
                
            }
        }

        private void GetRakeSummaryDetailsDefaultColumn()
        {
            //Slno, Rake No, Date, Time, Wagon, C.Cpy, P.C.C, Gross, Tare, Net,U.Load,O.Load,Commodity,From,To
            dttemp = new DataTable();
            dttemp.Columns.Add("Slno");
            dttemp.Columns.Add("Rake No");
            dttemp.Columns.Add("Date");            
            dttemp.Columns.Add("Wagon");
            dttemp.Columns.Add("C.Cpy");
            dttemp.Columns.Add("P.C.C");
            dttemp.Columns.Add("Gross");
            dttemp.Columns.Add("Tare");
            dttemp.Columns.Add("Net");
            dttemp.Columns.Add("U.Load");
            dttemp.Columns.Add("O.Load");
            dttemp.Columns.Add("Commodity");
            dttemp.Columns.Add("From");
            dttemp.Columns.Add("To");
            dgRakeView.DataSource = dttemp;
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00";
            foreach (DataGridViewColumn column in dgRakeView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public string DRToString(string value)
        {
            double dvalue = double.Parse(value);
            return dvalue.ToString("00.00");
        }
        private void GetRakeSummaryDetails()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            //db.ExecuteQuery("Select Slno,RakeNo AS [Rake No],Rdate AS [Date],Timeout AS [Time],WagonNo AS [Wagon],CC AS [C Cpy],PCC,Gross,Tare,Net,UL As [U Load],OL AS [O Load],Product AS Commodity,[From],[To] from Temp", null, dtVendor, "RakeSummary");
            db.ExecuteQuery("Select RakeNo AS [Rake No], Format(rdate,'dd/mm/yyyy') As [Date], count(WagonNo) AS [Wagon], Sum(CC) AS [C Cpy],Sum(PCC) As PCC,Sum(Gross) AS Gross,Sum(Tare) As Tare,Sum(Net) AS Net,Sum(UL) As [U Load],Sum(OL) AS [O Load], Product AS Commodity, [From] , [To] from Temp group by RakeNo,Product,[From],[To],Format(rdate,'dd/mm/yyyy')", null, dtVendor, "RakeSummary");

            //return dtVendor.Tables[0];

            

             GetRakeSummaryDetailsDefaultColumn();
             for (int i = 0; i < dtVendor.Tables[0].Rows.Count; i++)
             {
                 DataRow dr = dttemp.NewRow();
                 dr["Slno"] = (i + 1).ToString();
                 ////Slno, Rake No, Date, Time, Wagon, C.Cpy, P.C.C, Gross, Tare, Net,U.Load,O.Load,Commodity,From,To
                 dr["Rake No"] = dtVendor.Tables[0].Rows[i][0].ToString();
                 dr["Date"] = dtVendor.Tables[0].Rows[i][1].ToString();
                 dr["Wagon"] = dtVendor.Tables[0].Rows[i][2].ToString();
                 dr["C.Cpy"] = DRToString(dtVendor.Tables[0].Rows[i][3].ToString());
                 dr["P.C.C"] = DRToString(dtVendor.Tables[0].Rows[i][4].ToString());
                 dr["Gross"] = DRToString(dtVendor.Tables[0].Rows[i][5].ToString());
                 dr["Tare"] = DRToString(dtVendor.Tables[0].Rows[i][6].ToString());
                 dr["Net"] = DRToString(dtVendor.Tables[0].Rows[i][7].ToString());
                 dr["U.Load"] = DRToString(dtVendor.Tables[0].Rows[i][8].ToString());
                 dr["O.Load"] = DRToString(dtVendor.Tables[0].Rows[i][9].ToString());
                 dr["Commodity"] = dtVendor.Tables[0].Rows[i][10].ToString();
                 dr["From"] = dtVendor.Tables[0].Rows[i][11].ToString();
                 dr["To"] = dtVendor.Tables[0].Rows[i][12].ToString();
                 dttemp.Rows.Add(dr);

             }


            var dtSum = new DataSet();
            string sumQuery = string.Format("Select sum(CC) As [C Cap], Sum(PCC) As PCC, Sum(Gross) As Gross, Sum(Tare) As Tare, Sum(Net) As Net, Sum(UL) As [U Load], Sum(OL) As [O Load] from Temp");
            db.ExecuteQuery(sumQuery, null, dtSum, "SumRakeList");


            if (dttemp.Rows.Count > 0)
            {
                DataRow dr = dttemp.NewRow();
                dr[0] = "Total";
                dr[4] = DRToString(dtSum.Tables[0].Rows[0][0].ToString());
                dr[5] = DRToString(dtSum.Tables[0].Rows[0][1].ToString());
                dr[6] = DRToString(dtSum.Tables[0].Rows[0][2].ToString());
                dr[7] = DRToString(dtSum.Tables[0].Rows[0][3].ToString());
                dr[8] = DRToString(dtSum.Tables[0].Rows[0][4].ToString());
                dr[9] = DRToString(dtSum.Tables[0].Rows[0][5].ToString());
                dr[10] = DRToString(dtSum.Tables[0].Rows[0][6].ToString());
                dttemp.Rows.Add(dr);
            }
            dgRakeView.DataSource = dttemp;

            AssignDefaultCellStyleForGrid(4, 11);

           
            if (dgRakeView.Rows.Count > 0)
                dgRakeView.FirstDisplayedScrollingRowIndex = dgRakeView.Rows.Count - 1;

        }

        private void GetMaterialDesReportColumn()
        {
            //Slno,Dateout,Rake No,Commodity,C.C, P.C.C,Gross,Tare,Net,U.Load,O.Load
            dttemp = new DataTable();
            dttemp.Columns.Add("Slno");
            dttemp.Columns.Add("Dateout");
            dttemp.Columns.Add("Rake No");
            dttemp.Columns.Add("Commodity");            
            dttemp.Columns.Add("C.C");
            dttemp.Columns.Add("P.C.C");
            dttemp.Columns.Add("Gross");
            dttemp.Columns.Add("Tare");
            dttemp.Columns.Add("Net");
            dttemp.Columns.Add("U.Load");
            dttemp.Columns.Add("O.Load");
            
            dgRakeView.DataSource = dttemp;
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00";
            foreach (DataGridViewColumn column in dgRakeView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void GetMaterialDetails(string MaterialName,string fromDate,string toDate)
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            string materialQuery = string.Format("Select Slno,rdate AS [Date/Time],RakeNo As [Rake No],Product As [Commodity],CC,PCC,Gross,Tare,Net,UL AS [U Load],OL As [O Load] from Temp where Product='{0}' and (rdate >= #{1}#) and (rdate < #{2}#) order by val(Slno)", MaterialName, fromDate, toDate);

            db.ExecuteQuery(materialQuery, null, dtVendor, "MaterialDeails");
           
            
           // return dtVendor.Tables[0];

            var dtSum = new DataSet();
            string sumQuery = string.Format("Select sum(CC) As [C Cap], Sum(PCC) As PCC, Sum(Gross) As Gross, Sum(Tare) As Tare, Sum(Net) As Net, Sum(UL) As [U Load], Sum(OL) As [O Load] from Temp where Product='{0}' and (rdate >= #{1}#) and (rdate < #{2}#)", MaterialName, fromDate, toDate);
            db.ExecuteQuery(sumQuery, null, dtSum, "SumRakeList");

            if (dtVendor.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dtVendor.Tables[0].NewRow();
                dr[0] = "Total";
                dr[4] = dtSum.Tables[0].Rows[0][0].ToString();
                dr[5] = dtSum.Tables[0].Rows[0][1].ToString();
                dr[6] = dtSum.Tables[0].Rows[0][2].ToString();
                dr[7] = dtSum.Tables[0].Rows[0][3].ToString();
                dr[8] = dtSum.Tables[0].Rows[0][4].ToString();
                dr[9] = dtSum.Tables[0].Rows[0][5].ToString();
                dr[10] = dtSum.Tables[0].Rows[0][6].ToString();
                dtVendor.Tables[0].Rows.Add(dr);
            }
            dgRakeView.DataSource = dtVendor.Tables[0];
            AssignDefaultCellStyleForGrid(4, 10);

            //Slno,Dateout,Rake No,Commodity,C.C, P.C.C,Gross,Tare,Net,U.Load,O.Load
            ////Rake no,commodity,C.C,P.C.C,U.Load,O.Load
            //AssignGetMaterialDestinationDetails(dtVendor);

        }

        private void AssignGetMaterialDestinationDetails(DataSet dtVendor)
        {
            //Rake no,commodity,C.C,P.C.C,U.Load,O.Load
            for (int i = 0; i < dtVendor.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dttemp.NewRow(); dr["Slno"] = dtVendor.Tables[0].Rows[i]["Slno"].ToString();

                dr["Dateout"] = dtVendor.Tables[0].Rows[i]["Dateout"].ToString();
                dr["Rake No"] = dtVendor.Tables[0].Rows[i]["RakeNo"].ToString();
                dr["Commodity"] = dtVendor.Tables[0].Rows[i]["Product"].ToString();
                dr["C.C"] = dtVendor.Tables[0].Rows[i]["CC"].ToString();
                dr["P.C.C"] = dtVendor.Tables[0].Rows[i]["PCC"].ToString();
                dr["Gross"] = dtVendor.Tables[0].Rows[i]["Gross"].ToString();
                dr["Tare"] = dtVendor.Tables[0].Rows[i]["Tare"].ToString();
                dr["Net"] = dtVendor.Tables[0].Rows[i]["Net"].ToString();
                dr["U.Load"] = dtVendor.Tables[0].Rows[i]["UL"].ToString();
                dr["O.Load"] = dtVendor.Tables[0].Rows[i]["OL"].ToString();

                dttemp.Rows.Add(dr);
            }
            if(dgRakeView.Rows.Count > 0)
                dgRakeView.FirstDisplayedScrollingRowIndex = dgRakeView.Rows.Count - 1;
        }

        private void AssignDefaultCellStyleForGrid(int from, int to)
        {
            for(int i=from; i<=to;i++)
            {
                dgRakeView.Columns[i].DefaultCellStyle.Format = "00.00";
            }
        }
        private void GetDestinationDetails(string Destination, string fromDate, string toDate)
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            string destinationQuery = string.Format("Select Slno,rdate AS [Date/Time],RakeNo AS [Rake No],Product As [Commodity],CC,PCC,Gross,Tare,Net,UL AS [U Load],OL AS [O Load] from Temp where To='{0}' and (rdate >= #{1}#) and (rdate < #{2}#) order by val(Slno)", Destination, fromDate, toDate);

            db.ExecuteQuery(destinationQuery, null, dtVendor, "DestinationDetails");
            //dgRakeView.DataSource = dtVendor.Tables[0];

            var dtSum = new DataSet();
            string sumQuery = string.Format("Select sum(CC) As [C Cap], Sum(PCC) As PCC, Sum(Gross) As Gross, Sum(Tare) As Tare, Sum(Net) As Net, Sum(UL) As [U Load], Sum(OL) As [O Load] from Temp where To='{0}' and (rdate >= #{1}#) and (rdate < #{2}#)", Destination, fromDate, toDate);
            db.ExecuteQuery(sumQuery, null, dtSum, "SumRakeList");

            if (dtVendor.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dtVendor.Tables[0].NewRow();
                dr[0] = "Total";
                dr[4] = dtSum.Tables[0].Rows[0][0].ToString();
                dr[5] = dtSum.Tables[0].Rows[0][1].ToString();
                dr[6] = dtSum.Tables[0].Rows[0][2].ToString();
                dr[7] = dtSum.Tables[0].Rows[0][3].ToString();
                dr[8] = dtSum.Tables[0].Rows[0][4].ToString();
                dr[9] = dtSum.Tables[0].Rows[0][5].ToString();
                dr[10] = dtSum.Tables[0].Rows[0][6].ToString();
                dtVendor.Tables[0].Rows.Add(dr);
            }
            dgRakeView.DataSource = dtVendor.Tables[0];

            AssignDefaultCellStyleForGrid(4, 10);
            

            if (dgRakeView.Rows.Count > 0)
                dgRakeView.FirstDisplayedScrollingRowIndex = dgRakeView.Rows.Count - 1;
           // return dtVendor.Tables[0];

            //AssignGetMaterialDestinationDetails(dtVendor);
        }

        private void AssignGetDateDetailsDefaultCoumn()
        {
            //Dateout,Rake No,Wagon,C.C, P.C.C,Gross,Tare,Net,U.Load,O.Load
            dttemp = new DataTable();
            dttemp.Columns.Add("Slno");
            dttemp.Columns.Add("Dateout");
            dttemp.Columns.Add("Rake No");            
            dttemp.Columns.Add("Wagon");
            dttemp.Columns.Add("C.C");
            dttemp.Columns.Add("P.C.C");
            dttemp.Columns.Add("Gross");
            dttemp.Columns.Add("Tare");
            dttemp.Columns.Add("Net");
            dttemp.Columns.Add("U.Load");
            dttemp.Columns.Add("O.Load");            
            dgRakeView.DataSource = dttemp;
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00";
            foreach (DataGridViewColumn column in dgRakeView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void GetMaterialDetails(string fromDate, string toDate)
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            string materialQuery = string.Format("Select Slno,Rdate AS [Date/Time],RakeNo AS [Rake No],Wagonno AS [Wagon],CC,PCC,Gross,Tare,Net,UL AS [U Load],OL AS [O Load] from Temp where (rdate >= #{0}#) and (rdate < #{1}#) order by val(Slno)", fromDate, toDate);

            db.ExecuteQuery(materialQuery, null, dtVendor, "MaterialDeails");
            dgRakeView.DataSource = dtVendor.Tables[0];


            var dtSum = new DataSet();
            string sumQuery = string.Format("Select sum(CC) As [C Cap], Sum(PCC) As PCC, Sum(Gross) As Gross, Sum(Tare) As Tare, Sum(Net) As Net, Sum(UL) As [U Load], Sum(OL) As [O Load] from Temp where (rdate >= #{0}#) and (rdate < #{1}#)", fromDate, toDate);
            db.ExecuteQuery(sumQuery, null, dtSum, "SumRakeList");

            if (dtVendor.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dtVendor.Tables[0].NewRow();
                dr[0] = "Total";
                dr[4] = dtSum.Tables[0].Rows[0][0].ToString();
                dr[5] = dtSum.Tables[0].Rows[0][1].ToString();
                dr[6] = dtSum.Tables[0].Rows[0][2].ToString();
                dr[7] = dtSum.Tables[0].Rows[0][3].ToString();
                dr[8] = dtSum.Tables[0].Rows[0][4].ToString();
                dr[9] = dtSum.Tables[0].Rows[0][5].ToString();
                dr[10] = dtSum.Tables[0].Rows[0][6].ToString();
                dtVendor.Tables[0].Rows.Add(dr);
            }

            AssignDefaultCellStyleForGrid(4, 10);
            //return dtVendor.Tables[0];
            ////Dateout,Rake No,Wagon,C.C, P.C.C,Gross,Tare,Net,U.Load,O.Load

            //for (int i = 0; i < dtVendor.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dr = dttemp.NewRow(); dr["Slno"] = dtVendor.Tables[0].Rows[i]["Slno"].ToString();

            //    dr["Dateout"] = dtVendor.Tables[0].Rows[i]["Dateout"].ToString();
            //    dr["Rake No"] = dtVendor.Tables[0].Rows[i]["RakeNo"].ToString();
            //    dr["Wagon"] = dtVendor.Tables[0].Rows[i]["Wagonno"].ToString();
            //    dr["C.C"] = dtVendor.Tables[0].Rows[i]["CC"].ToString();
            //    dr["P.C.C"] = dtVendor.Tables[0].Rows[i]["PCC"].ToString();
            //    dr["Gross"] = dtVendor.Tables[0].Rows[i]["Gross"].ToString();
            //    dr["Tare"] = dtVendor.Tables[0].Rows[i]["Tare"].ToString();
            //    dr["Net"] = dtVendor.Tables[0].Rows[i]["Net"].ToString();
            //    dr["U.Load"] = dtVendor.Tables[0].Rows[i]["UL"].ToString();
            //    dr["O.Load"] = dtVendor.Tables[0].Rows[i]["OL"].ToString();

            //    dttemp.Rows.Add(dr);
            //}
            if (dgRakeView.Rows.Count > 0)
                dgRakeView.FirstDisplayedScrollingRowIndex = dgRakeView.Rows.Count - 1;

        }


        private void GetWeighBillDetailsDefaultColumn()
        {
           // Slno,Date,WagonNo,Weight,Speed,Remark,In/Out

            dttemp = new DataTable();
            dttemp.Columns.Add("Slno");
            dttemp.Columns.Add("Date");
            dttemp.Columns.Add("WagonNo");
            dttemp.Columns.Add("Weight");
            dttemp.Columns.Add("Speed");
            dttemp.Columns.Add("Remark");
            dttemp.Columns.Add("In/Out");
           
            dgRakeView.DataSource = dttemp;
            
            

            foreach (DataGridViewColumn column in dgRakeView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
         
            
            
        }

        private void GetWeighBillDetails(string RakeNo)
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            //string rakeQuery = string.Format("Select Slno,rdate As [Date/Time],WagonNo,Gross As Weight,spd As Speed,spdRemarks As Remark,direction As [In/Out] from Temp where RakeNo={0} order by val(Slno)", RakeNo);

            string rakeQuery = string.Format("Select Slno,WagonNo,Gross As Weight,spd As Speed,spdRemarks As Remark from Temp where RakeNo={0} order by val(Slno)", RakeNo);

            db.ExecuteQuery(rakeQuery, null, dtVendor, "WeighDetails");
           // return dtVendor.Tables[0];
            //Slno,Date,WagonNo,Weight,Speed,Remark,In/Out

            var dtSum = new DataSet();
            string sumQuery = string.Format("Select Sum(Gross) As Gross from Temp where RakeNo={0}", RakeNo);
            db.ExecuteQuery(sumQuery, null, dtSum, "SumRakeList");

            if (dtVendor.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dtVendor.Tables[0].NewRow();
                dr[0] = "Total";
                dr[2] = dtSum.Tables[0].Rows[0][0].ToString(); //                
                dtVendor.Tables[0].Rows.Add(dr);
            }

            dgRakeView.DataSource = dtVendor.Tables[0];

            //for (int i = 0; i < dtVendor.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dr = dttemp.NewRow(); dr["Slno"] = dtVendor.Tables[0].Rows[i]["Slno"].ToString();

            //    dr["Date"] = dtVendor.Tables[0].Rows[i]["rdate"].ToString();                
            //    dr["WagonNo"] = dtVendor.Tables[0].Rows[i]["WagonNo"].ToString();              
            //    dr["Weight"] =(double)(dtVendor.Tables[0].Rows[i]["Gross"]);
            //    dr["Speed"] = dtVendor.Tables[0].Rows[i]["spd"].ToString();
            //    dr["Remark"] = dtVendor.Tables[0].Rows[i]["spdRemarks"].ToString();
            //    dr["In/Out"] = dtVendor.Tables[0].Rows[i]["direction"].ToString();
                
            //    dttemp.Rows.Add(dr);
            //}
            if (dgRakeView.Rows.Count > 0)
            {
                btnPrint.Enabled = true;
                dgRakeView.FirstDisplayedScrollingRowIndex = dgRakeView.Rows.Count - 1;

                AssignDefaultCellStyleForGrid(3, 4);
           

            }

        }





        private void txtRakeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if(txtRakeNo.Text !=string.Empty)
                {
                    if (btnRakeReport.Enabled)
                    {
                        AssignDefaultColumnsForRakeNoDetails();
                        GetRakeNoDetailsByRakeNo(txtRakeNo.Text);
                    }
                    else
                    {
                        GetWeighBillDetailsDefaultColumn();
                        GetWeighBillDetails(txtRakeNo.Text);
                    }

                   chkTextFile.Visible = false;
                }
            }
        }

        private void lstRakeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lstRakeNumber.SelectedItem == null)
                return;

            txtRakeNo.Text = lstRakeNumber.SelectedItem.ToString();
            txtRakeNo.Focus();
            lstRakeNumber.Visible = false;
            
            
        }

        private void cmbMaterialName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if ( cmbMaterialName.SelectedIndex > 0 && cmbMaterialName.SelectedItem !=null && !string.IsNullOrEmpty(cmbMaterialName.SelectedItem.ToString()))
                {
                    txtFromDate.Focus();
                }
            }
        }

        private void txtFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DateTime fromDate = DateTime.MinValue;
                DateTime.TryParse(txtFromDate.Text,out fromDate);
                if (fromDate != DateTime.MinValue)
                    txtToDate.Focus();
                else
                    txtFromDate.Focus();
            }
        }

        private void txtToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbMaterialName.Visible && cmbMaterialName.SelectedItem == null)
                return;

            if (e.KeyChar == (char)Keys.Enter)
            {
                DateTime toDate = DateTime.MinValue;
                DateTime.TryParse(txtToDate.Text,out toDate);
                if (toDate != DateTime.MinValue)
                {
                    if (cmbMaterialName.Visible)
                    {
                        GetMaterialDesReportColumn();
                        if(!btnMaterialReport.Enabled)
                            GetDestinationDetails(cmbMaterialName.SelectedItem.ToString(), txtFromDate.Text, txtToDate.Text);
                        else
                           GetMaterialDetails(cmbMaterialName.SelectedItem.ToString(), txtFromDate.Text, txtToDate.Text);
                    }
                    else
                    {
                        AssignGetDateDetailsDefaultCoumn();
                        GetMaterialDetails(txtFromDate.Text, txtToDate.Text);
                    }

                    chkTextFile.Visible = false;
                }
                else
                    txtToDate.Focus();
            }
         
        }

        private void chkTextFile_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTextFile.Checked)
                btnOk.Visible = true;
            else
                btnOk.Visible = false;
        }

        private void chkExcelFile_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExcelFile.Checked)
                btnOk.Visible = true;
            else
                btnOk.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmDefaultImage().Show();
        }

        private DataTable RakeBillPrintData()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            string rakeQuery = string.Format("Select Slno,rdate,WagonNo,Gross,Tare,PCC,CC,spd,Product,[From],[To],spdRemarks,direction,UL,OL,wagontype,net,Timein,Timeout from Temp where RakeNo={0} order by val(Slno)", txtRakeNo.Text);

            db.ExecuteQuery(rakeQuery, null, dtVendor, "WeighDetails");
            return dtVendor.Tables[0];
        }
     
        private void Print56()
        {

            //PrinterSettings printerSettings = new PrinterSettings();
            //IQueryable<PaperSize> paperSizes = printerSettings.PaperSizes.Cast<PaperSize>().AsQueryable();
            //PaperSize a4rotated = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A4).First();
            

            //var psettings = new PageSettings();
            //psettings.PrinterSettings = printerSettings;
            //psettings.PaperSize = a4rotated;

            PrintDocument pd = new PrintDocument();
            //pd.DefaultPageSettings = psettings;
            pd.PrintPage += new PrintPageEventHandler(PrintImage56);

            if (!Convert.ToBoolean(Shared_Variables.printPreviewEnable))
                pd.Print();
            else
            {
                PrintPreviewDialog pview = new PrintPreviewDialog();
                pview.Document = pd;
                pview.ShowDialog();
            }


        }


        private DataTable RemoveDoubleEntryWagon(DataTable doubleE, DataRow[] drs)
        {
            foreach (DataRow row in drs)
                doubleE.Rows.Remove(row);

            return doubleE;
        }

        private DataTable CheckDoubleEntryWagonNo(DataTable doubleEntryWagonNo)
        {
            DataRow[] dr = doubleEntryWagonNo.Select("WagonNo = 'BVZ'");
            RemoveDoubleEntryWagon(doubleEntryWagonNo, dr);
            dr = doubleEntryWagonNo.Select("WagonNo = 'BVZ1'");
            RemoveDoubleEntryWagon(doubleEntryWagonNo, dr);
            dr = doubleEntryWagonNo.Select("WagonNo = 'BVZ2'");
            RemoveDoubleEntryWagon(doubleEntryWagonNo, dr);
            dr = doubleEntryWagonNo.Select("WagonNo = 'BVZ3'");
            RemoveDoubleEntryWagon(doubleEntryWagonNo, dr);
            dr = doubleEntryWagonNo.Select("WagonNo = 'BVZ4'");
            RemoveDoubleEntryWagon(doubleEntryWagonNo, dr);
            dr = doubleEntryWagonNo.Select("WagonNo = 'BVZ5'");
            RemoveDoubleEntryWagon(doubleEntryWagonNo, dr);
            return doubleEntryWagonNo;
          
        }

        int printRowCount56 = 0; int pageRowCount56 = 40; int RowCount56 = 0; bool is56Header = false; int k = 1;
        double CC56 = 0.0, PCC56 = 0.0, Gross56 = 0.0, Tare56 = 0.0, Net56 = 0.0, Ul56 = 0.0, Ol56 = 0.0;
        void PrintImage56(object o, PrintPageEventArgs e)
        {
            try
            {
                
                int width = 200;
                int height = 20;
                int origin = 100;
                
                int GRDH = 25;
                int y = 0;              
                int Corigin = 60;
                int GNRDW = 100;

                string inout = string.Empty;
                string intime = string.Empty;
                string leavingtime = string.Empty;
                string date = string.Empty;
                string product = string.Empty;
                string from = string.Empty;
                string to = string.Empty;
                string wagontype = string.Empty;

                DataTable result = RakeBillPrintData();
                result = CheckDoubleEntryWagonNo(result);
                //Slno,rdate,WagonNo,Gross,Tare,PCC,CC,spd,Product,From,To,spdRemarks,direction,UL,OL,wagontypes,net

                //date = result.Rows[0][1].ToString().Split(' ')[0];

                DateTime timeSpan = DateTime.MinValue;

                if (!string.IsNullOrEmpty(result.Rows[0]["Timein"].ToString()))
                    timeSpan = DateTime.Parse(result.Rows[0]["Timein"].ToString());
                else
                    timeSpan = DateTime.Parse(result.Rows[0][1].ToString());

                intime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);
                date = timeSpan.ToString(Shared_Variables.DateFormat);


                if (!string.IsNullOrEmpty(result.Rows[result.Rows.Count - 1]["Timeout"].ToString()))
                    timeSpan = DateTime.Parse(result.Rows[result.Rows.Count - 1]["Timeout"].ToString());
                else
                    timeSpan = DateTime.Parse(result.Rows[result.Rows.Count - 1][1].ToString());

                leavingtime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);



                inout = result.Rows[0][12].ToString();
                product = result.Rows[0][8].ToString();
                from = result.Rows[0][9].ToString();
                to = result.Rows[0][10].ToString();
                wagontype = result.Rows[0][15].ToString();


                StringFormat str = new StringFormat();
                str.Alignment = StringAlignment.Near;
                str.LineAlignment = StringAlignment.Center;
                str.Trimming = StringTrimming.EllipsisCharacter;
                Pen dashed_pen = new Pen(Color.Black, 1.5f);
                

                if(!is56Header)                
                    height = HeaderFor56PrintImage(e, width, height, origin, GRDH, Corigin, GNRDW, intime, date, product, from, to, wagontype, str, Shared_Variables.dotLine, leavingtime);               
                else
                {
                    height = Shared_Variables.SpaceValueForFinalCopy;

                    height += 90;   
                    e.Graphics.DrawString("Cont...Page  - " + k + " of Rake No ..                          : " + txtRakeNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 300, GRDH), str);
                    height += 10;
                }
                

                

                for (int i = printRowCount56; i < result.Rows.Count; i++)
                {                    

                    DataRow dgvr = result.Rows[i];
                    ////Slno,rdate,WagonNo,Gross,Tare,PCC,CC,spd,Product,From,To,spdRemarks,direction,UL,OL,wagontypes,net
                    double cc = double.Parse(dgvr[6].ToString()); double tare = double.Parse(dgvr[4].ToString()); double net = double.Parse(dgvr[16].ToString());
                    double pcc = double.Parse(dgvr[5].ToString()); double gross = double.Parse(dgvr[3].ToString()); double ul = double.Parse(dgvr[13].ToString());
                    double ol = double.Parse(dgvr[14].ToString());
                    height += Shared_Variables.SpaceValueForFinalCopy;//dgvr[0].ToString()

                    e.Graphics.DrawString((i+1).ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr[2].ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 60, height, GNRDW+90, GRDH), str);
                    e.Graphics.DrawString(cc.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 230, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(pcc.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 290, height, GNRDW, GRDH), str);

                    e.Graphics.DrawString(gross.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(tare.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 430, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(net.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 500, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(ul.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 570, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(ol.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 690, height, GNRDW, GRDH), str);

                    CC56 += cc; PCC56 += pcc; Gross56 += gross;
                    Tare56 += tare; Net56 += net; Ul56 += ul;
                    Ol56 += ol;

                    printRowCount56++; RowCount56++;
                    if (pageRowCount56 <= RowCount56)
                    {
                        RowCount56 = RowCount56 - pageRowCount56;
                        e.HasMorePages = true;
                        is56Header = true;
                        k++;
                        return;
                    }
                    else
                    {
                        e.HasMorePages = false;
                        is56Header = false;
                    }


                }
                height += Shared_Variables.SpaceValueForFinalCopy;
                e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                height += Shared_Variables.SpaceValueForFinalCopy;

                e.Graphics.DrawString("TOTAL :", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
                e.Graphics.DrawString(CC56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 230, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(PCC56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 290, height, GNRDW, GRDH), str);

                e.Graphics.DrawString(Gross56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(Tare56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 430, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(Net56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 500, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(Ul56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 570, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(Ol56.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 690, height, GNRDW, GRDH), str);

                height += Shared_Variables.SpaceValueForFinalCopy;

                e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
               
                height += 50;
                e.Graphics.DrawString("All Weights are in Tonnes", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 100, GRDH), str);
                e.Graphics.DrawString("Authorised Signature", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);

                printRowCount56 = 0;
                RowCount56 = 0;
                CC56 = 0.0; PCC56 = 0.0; Gross56 = 0.0; Tare56 = 0.0; Net56 = 0.0; Ul56 = 0.0; Ol56 = 0.0;
                k = 1;
                is56Header = false; e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show("Complete Tare Entry First and Generate Report");
            }
        }

        private int HeaderFor56PrintImage(PrintPageEventArgs e, int width, int height, int origin, int GRDH, int Corigin, int GNRDW, string intime, string date, string product, string from, string to, string wagontype, StringFormat str, string dotLine,string leavingTime)
        {
            var dt = new DataSet();
            new DBEngine().ExecuteQuery("select Report1 from ScreenCommon", null, dt, "ReportHeader");
            string[] headers = dt.Tables[0].Rows[0][0].ToString().Split(',');

            if (headers.Length > 1)
            {
                e.Graphics.DrawString(headers[0], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 400, GRDH), str);
                height += 20;
                e.Graphics.DrawString(headers[1], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(350 - headers[1].Length, height, width + 300, GRDH), str);
            }
            else
                e.Graphics.DrawString(headers[0], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 300, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;
            e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("RAKE No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 140, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(txtRakeNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 150, height, GNRDW, GRDH), str);


            e.Graphics.DrawString("Wagon Type", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 450, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 540, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(wagontype, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 550, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("Weighment Date", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 140, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(date, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 150, height, GNRDW + 100, GRDH), str);           


            e.Graphics.DrawString("Material", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 450, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 540, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(product, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 550, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("Consigner", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 140, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(from, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 150, height, GNRDW + 100, GRDH), str);


            e.Graphics.DrawString("Consignee", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 450, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 540, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(to, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 550, height, GNRDW, GRDH), str);


            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("Print Date", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 140, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(DateTime.Now.ToString(Shared_Variables.DateFormat), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 150, height, GNRDW + 100, GRDH), str);


            e.Graphics.DrawString("Print Time", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 450, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 540, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 550, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("IN Time", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 140, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(intime, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 150, height, GNRDW + 100, GRDH), str);


            e.Graphics.DrawString("Leaving Time", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 450, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 540, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(leavingTime, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 550, height, GNRDW, GRDH), str);
            

            height += Shared_Variables.SpaceValueForFinalCopy;
            e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("SL", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("WAGON", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 60, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("CARRY", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 230, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("PER", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 290, height, GNRDW, GRDH), str);

            e.Graphics.DrawString("GROSS", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("TARE", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 430, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("NET", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 500, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("UNDER", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 570, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("OVER", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 690, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;

            e.Graphics.DrawString("NO", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("NO.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 60, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("CAP.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 230, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("C.C", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 290, height, GNRDW, GRDH), str);

            e.Graphics.DrawString("WT.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("WT.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 430, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("WT.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 500, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("LOAD.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 570, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("LOAD.", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 690, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValueForFinalCopy;


            e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
            return height;
        }

        private void Print78()
        {

            PrinterSettings printerSettings = new PrinterSettings();
            IQueryable<PaperSize> paperSizes = printerSettings.PaperSizes.Cast<PaperSize>().AsQueryable();
            PaperSize a4rotated = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A3).First();

            var psettings = new PageSettings();
            psettings.PrinterSettings = printerSettings;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings = psettings;
            pd.PrintPage += new PrintPageEventHandler(PrintImage78);

            //pd.Print();

            PrintPreviewDialog pview = new PrintPreviewDialog();
            pview.Document = pd;
            pview.ShowDialog();


        }
        void PrintImage78(object o, PrintPageEventArgs e)
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
                    e.Graphics.DrawString(headers[0], new Font(FontFamily.GenericSerif, 20, FontStyle.Regular), Brushes.Black, new RectangleF(200 - headers[0].Length, height, width + 400, GRDH), str);
                    height += 20;
                    e.Graphics.DrawString(headers[1], new Font(FontFamily.GenericSerif, 20, FontStyle.Regular), Brushes.Black, new RectangleF(300 - headers[1].Length, height, width + 300, GRDH), str);
                }
                else
                    e.Graphics.DrawString(headers[0], new Font(FontFamily.GenericSerif, 20, FontStyle.Regular), Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 300, GRDH), str);

                height += 20;
                e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
                height += 20;

                e.Graphics.DrawString("RAKE No:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString(txtRakeNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("DIRECTION:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);
                e.Graphics.DrawString("IN", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

                height += 20;

                e.Graphics.DrawString("WEIGHMENT ON:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
                e.Graphics.DrawString("09-12-2015", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("TIME IN:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);
                e.Graphics.DrawString("12:45", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

                height += 20;
                e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                height += 20;

                e.Graphics.DrawString("Sl.No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                e.Graphics.DrawString("Wagon No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 100, height, GNRDW, GRDH), str);
                e.Graphics.DrawString("Weight", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);
                e.Graphics.DrawString("Speed", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 400, height, GNRDW, GRDH), str);
                e.Graphics.DrawString("Remarks", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 600, height, GNRDW, GRDH), str);

                height += 20;
                e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                double Weight = 0.0;
                foreach (DataGridViewRow dgvr in dgRakeView.Rows)
                {
                    height += 20;

                    e.Graphics.DrawString("1", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString("X", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 100, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString("80.00", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString("07.20", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 400, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString("X", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 600, height, GNRDW, GRDH), str);

                    Weight += double.Parse(dgvr.Cells[2].Value.ToString());
                }
                height += 20;
                e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                height += 20;

                e.Graphics.DrawString("TOTAL (MT):", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
                e.Graphics.DrawString(Weight.ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 200, height, GNRDW, GRDH), str);


                e.Graphics.DrawString("Train Leaving Time:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
                e.Graphics.DrawString(DateTime.Now.ToString("hh:mm"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

                height += 20;
                e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
                height += 50;
                e.Graphics.DrawString("All Weights are in Tonnes", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 100, GRDH), str);
                e.Graphics.DrawString("Author Signature", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);

            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable WeighbillPrintData()
        {
            var db = new DBEngine();
            
            var dtVendor = new DataSet();
            string rakeQuery = string.Format("Select Slno,rdate,WagonNo,Gross,spd,spdRemarks,direction,Timein,Timeout,WagonTime from Temp where RakeNo={0} order by val(Slno)", txtRakeNo.Text);

            db.ExecuteQuery(rakeQuery, null, dtVendor, "WeighDetails");
            return dtVendor.Tables[0];
        }
        private void Print34()
        {

            //PrinterSettings printerSettings = new PrinterSettings();
            //IQueryable<PaperSize> paperSizes = printerSettings.PaperSizes.Cast<PaperSize>().AsQueryable();
            //PaperSize a4rotated = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A3).First();

            //var psettings = new PageSettings();
            //psettings.PrinterSettings = printerSettings;

            PrintDocument pd = new PrintDocument();
            //pd.DefaultPageSettings = psettings;
            pd.PrintPage += new PrintPageEventHandler(PrintImage34);

            if (!Convert.ToBoolean(Shared_Variables.printPreviewEnable))
            {
                PaperSize ps = new PaperSize("sd", pd.DefaultPageSettings.PaperSize.Width, (141 + (dgRakeView.Rows.Count * 15) + 200));

                int mode = Convert.ToInt32(ConfigurationManager.AppSettings["Mode"]);
                pd.PrinterSettings.DefaultPageSettings.PrinterResolution = new PrinterResolution() { Kind = (PrinterResolutionKind)mode };
                pd.DefaultPageSettings.PrinterResolution = new PrinterResolution() { Kind = (PrinterResolutionKind)mode };

                pd.DefaultPageSettings.PaperSize = ps;
                pd.Print();
            }
            else
            {
                PaperSize ps = new PaperSize("sd", pd.DefaultPageSettings.PaperSize.Width, (141 + (dgRakeView.Rows.Count * 15) + 200));

                pd.DefaultPageSettings.PaperSize = ps;

                int mode = Convert.ToInt32(ConfigurationManager.AppSettings["Mode"]);
                pd.PrinterSettings.DefaultPageSettings.PrinterResolution=new PrinterResolution() { Kind=(PrinterResolutionKind)mode };
                pd.DefaultPageSettings.PrinterResolution = new PrinterResolution() { Kind =(PrinterResolutionKind)mode };
               
                PrintPreviewDialog pview = new PrintPreviewDialog();
                pview.Document = pd;
                pview.ShowDialog();
            }


        }
        int printRowCount34 = 0; int pageRowCount34 = 60; int RowCount34 = 0; bool is34header = false; int j = 1;
        double Weight34 = 0.0; int FooterDotLine1 = 0; int FooterDotLine2 = 0; int FooterTotal = 0; int FooterSign = 0; int currentpage = 0;
        void PrintImage34(object o, PrintPageEventArgs e)
        {
            try
            {

                int width = 200;
                int height = 20;
                int origin = 100;

                int GRDH = 25;
                



                int Corigin = 60;

                int GNRDW = 100;

                string inout = string.Empty;
                string intime = string.Empty;
                string leavingtime = string.Empty;
                string date = string.Empty;

                DataTable result=WeighbillPrintData();
                DateTime timeSpan = DateTime.MinValue;

                if (!string.IsNullOrEmpty(result.Rows[0]["Timein"].ToString()))
                {
                    timeSpan = DateTime.Parse(result.Rows[0]["Timein"].ToString());
                    intime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);
                    timeSpan = DateTime.Parse(result.Rows[result.Rows.Count - 1]["Timein"].ToString());
                    leavingtime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);
                }
                else if (!string.IsNullOrEmpty(result.Rows[0]["Timeout"].ToString()))
                {
                    timeSpan = DateTime.Parse(result.Rows[0]["Timeout"].ToString());
                    intime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);
                    timeSpan = DateTime.Parse(result.Rows[result.Rows.Count - 1]["Timeout"].ToString());
                    leavingtime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);
                }
                else
                {
                    timeSpan = DateTime.Parse(result.Rows[0][1].ToString());
                    intime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);
                    timeSpan = DateTime.Parse(result.Rows[result.Rows.Count - 1][1].ToString());
                    leavingtime = timeSpan.ToString(Shared_Variables.TimeFormatWithSeconds);

                }


                date = timeSpan.ToString(Shared_Variables.DateFormat);
                inout = result.Rows[0][6].ToString();
                StringFormat str = new StringFormat();
                str.Alignment = StringAlignment.Near;
                str.LineAlignment = StringAlignment.Center;
                str.Trimming = StringTrimming.EllipsisCharacter;
                Pen dashed_pen = new Pen(Color.Black, 1.5f);
                



                if (!is34header)
                   HeaderFor34PrintImage(e, width, ref height, origin, GRDH, Corigin, GNRDW, inout, intime, date, str);                
                else
                {                     
                    height = 1;
                  //  height = 15;
                   // e.Graphics.DrawString("Cont...Page  - " + j + " of Rake No ..                          : " + txtRakeNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 300, GRDH), str);
                    //height += 40;
                }
                
                //foreach (DataRow dgvr in result.Rows)
                for (int i = printRowCount34; i < result.Rows.Count; i++)
                {
                    DataRow dgvr = result.Rows[i];

                    height += Shared_Variables.SpaceValue;
                    double weight = double.Parse(dgvr[3].ToString());
                    double speed = double.Parse(dgvr[4].ToString());
                    string time = Convert.ToDateTime(dgvr["WagonTime"].ToString()).ToString(Shared_Variables.TimeFormatWithSeconds);
                    //Slno,rdate,WagonNo,Gross,spd,spdRemarks,direction
                    e.Graphics.DrawString(dgvr[0].ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr[2].ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 100, height, GNRDW+100, GRDH), str);
                    e.Graphics.DrawString(time, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 210, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(weight.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(speed.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 480, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr[5].ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 600, height, GNRDW, GRDH), str);

                    Weight34 += weight;

                    printRowCount34++; //RowCount34++;

                    if (CheckHeight_1(height, currentpage))
                    {
                        e.HasMorePages = true;
                        is34header = true;
                        currentpage++;
                        j++;
                        return;
                    }

                    //if (pageRowCount34 <= RowCount34)
                    //{
                    //    RowCount34 = RowCount34 - pageRowCount34;
                    //    e.HasMorePages = true;
                    //    is34header = true;
                    //    j++;
                    //    return;
                    //}
                    //else
                    //{
                    //    e.HasMorePages = false;
                    //    is34header = false;
                    //}

                }

                //if (result.Rows.Count > 55 && result.Rows.Count < 60 && printRowCount34 <= result.Rows.Count)
                //{
                //    printRowCount34 = 1000;
                //    e.HasMorePages = true;
                //    is34header = true;
                //    return;
                //}
                //else
                //{
                //    e.HasMorePages = false;
                //}


                //height += Shared_Variables.SpaceValue;
                //e.Graphics.DrawString(dotLine, new Font(FontFamily.GenericSerif, 20, FontStyle.Regular), Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

                //height += Shared_Variables.SpaceValue;

                //e.Graphics.DrawString("TOTAL (MT):", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
                //e.Graphics.DrawString(Weight34.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 300, height, GNRDW, GRDH), str);


                //e.Graphics.DrawString("Train Leaving Time:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
                //e.Graphics.DrawString(leavingtime, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

                //height += Shared_Variables.SpaceValue;
                //e.Graphics.DrawString(dotLine, new Font(FontFamily.GenericSerif, 20, FontStyle.Regular), Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
                //height += 50;
                //e.Graphics.DrawString("All Weights are in Tonnes", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 100, GRDH), str);
                //e.Graphics.DrawString("Authorised Signature", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);


                if(FooterDotLine1 == 0)
                    height += 18;

                if (!CheckHeight_1(height, currentpage))
                {
                    if (FooterDotLine1 == 0)
                    {
                        
                        e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
                        FooterDotLine1 = 1;
                    }
                }
                else
                {
                    e.HasMorePages = true;
                    is34header = true;
                    currentpage++;
                    return;

                }
               
                if(FooterTotal == 0)
                    height += 18;

                if (!CheckHeight_1(height, currentpage))
                {
                    if (FooterTotal == 0)
                    {
                       
                        e.Graphics.DrawString("TOTAL (MT):", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin+100, height, GNRDW + 60, GRDH), str);
                        e.Graphics.DrawString(Weight34.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);


                        e.Graphics.DrawString("Train Leaving Time:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
                        e.Graphics.DrawString(leavingtime, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

                        FooterTotal = 1;
                    }

                }
                else
                {
                    is34header = true;
                    e.HasMorePages = true;
                    currentpage++;
                    return;
                }

                if(FooterDotLine2 == 0)
                    height += 18;
                
                if (!CheckHeight_1(height, currentpage))
                {
                    if (FooterDotLine2 == 0)
                    {
                       
                        e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
                        FooterDotLine2 = 1;
                    }
                }
                else
                {
                    e.HasMorePages = true;
                    is34header = true;
                    currentpage++;
                    return;
                }

                if(FooterSign == 0)
                    height += 50;

                if (!CheckHeight_1(height, currentpage))
                {
                    if (FooterSign == 0)
                    {
                        
                        e.Graphics.DrawString("All Weights are in Tonnes", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 100, GRDH), str);
                        e.Graphics.DrawString("Authorised Signature", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
                        FooterSign = 1;
                    }
                }
                else
                {
                    e.HasMorePages = true;
                    is34header = true;
                    currentpage++;
                    return;
                }  


                is34header = false; printRowCount34 = 0; RowCount34 = 0; j = 1;
                e.HasMorePages = false;
                Weight34 = 0.0;
                FooterDotLine1 = 0; FooterDotLine2 = 0; FooterTotal = 0; FooterSign = 0; currentpage = 0;
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckHeight(int height, int currentPage)
        {
            int value = 0;
            value = currentpage == 0 ? 1041 : 1145;
            if (height > value)
                return true;
            else
                return false;
        }

        private bool CheckHeight_1(int height, int currentPage)
        {
            Func<string, string> appReader = key => ConfigurationManager.AppSettings[key];
            string FirstPageSizeValue = appReader("FirstPageSize");
            string SecondPageSizeValue = appReader("SecondPageSize");

            int value = 0;
            value = currentpage == 0 ? Convert.ToInt16(FirstPageSizeValue) : Convert.ToInt16(SecondPageSizeValue);
            if (height > value)
                return true;
            else
                return false;
        }
        private void HeaderFor34PrintImage(PrintPageEventArgs e, int width, ref int height, int origin, int GRDH, int Corigin, int GNRDW, string inout, string intime, string date, StringFormat str)
        {
            var dt = new DataSet();
            new DBEngine().ExecuteQuery("select Report1 from ScreenCommon", null, dt, "ReportHeader");
            string[] headers = dt.Tables[0].Rows[0][0].ToString().Split(',');

            //------------------

            if (headers.Length > 1)
            {
                e.Graphics.DrawString(headers[0], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 400, GRDH), str);
                height += 20; 
                e.Graphics.DrawString(headers[1], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(350 - headers[1].Length, height, width + 300, GRDH), str);
            }
            else
                e.Graphics.DrawString(headers[0], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 300, GRDH), str);

            height += Shared_Variables.SpaceValue; 
            e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
            height += Shared_Variables.SpaceValue; 

            e.Graphics.DrawString("RAKE No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 110, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(txtRakeNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 120, height, GNRDW, GRDH), str);


            e.Graphics.DrawString("DIRECTION", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 620, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(inout, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 630, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValue; 

            e.Graphics.DrawString("WEIGHMENT ON", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 110, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(date, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 120, height, GNRDW, GRDH), str);          

            e.Graphics.DrawString("TIME IN", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 620, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(intime, Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 630, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValue;

            e.Graphics.DrawString("PRINTING DATE", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 110, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(DateTime.Now.ToString(Shared_Variables.DateFormat), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 120, height, GNRDW, GRDH), str);

            e.Graphics.DrawString("PRINTING TIME", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 480, height, GNRDW+10, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 620, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 630, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValue; 
            e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

            height += Shared_Variables.SpaceValue;

            e.Graphics.DrawString("Sl.No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("Wagon No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 100, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("Time", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 220, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("Weight", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 350, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("Speed", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 480, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("Remarks", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 600, height, GNRDW, GRDH), str);

            height += Shared_Variables.SpaceValue;
            e.Graphics.DrawString(Shared_Variables.dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(btnWeighBill.Enabled && dgRakeView !=null && dgRakeView.Rows.Count > 0)
            {
                Print34();
            }
            if(btnRakeReport.Enabled && dgRakeView !=null && dgRakeView.Rows.Count > 0)
            {
                Print56();
            }
        }

    }
}
