using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace SenLogic
{
    public partial class StaticWeighment : Form
    {
        #region " Common Varible "
        DataGridViewPrinter MyDataGridViewPrinter;
        System.Drawing.Font f_title;
        #endregion

        public StaticWeighment()
        {
            InitializeComponent();
        }

        private DataTable GetDefaultData()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Product, Source,Destination,Type from [Default]", null, dtVendor, "Default");
            return dtVendor.Tables[0];
        }

        string WagonType, From, To, Product;

        private string GetRakeNumberFromTempTare()
        {
            using (var db = new DBEngine())
            {
                return db.ExecuteQuery("Select Max(RakeNo) from TempTare");
            }
        }

        private string GetRakeNumberFromTempGross()
        {
            using (var db = new DBEngine())
            {
                return db.ExecuteQuery("Select Max(RakeNo) from TempGross");
            }
        }
        private void StaticWeighment_Load(object sender, EventArgs e)
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            lblTitle.Text = db.ExecuteQuery("Select Screen1,Commtrack1, commBaud1 from ScreenCommon");

            txtDate.Text = DateTime.Now.ToString(Shared_Variables.DateFormat);


            DataTable dt = GetDefaultData();
            WagonType = dt.Rows[0][3].ToString();
            From = dt.Rows[0][1].ToString();
            To = dt.Rows[0][2].ToString();
            Product = dt.Rows[0][0].ToString();

            string tempTare = GetRakeNumberFromTempTare();
            string tempGross = GetRakeNumberFromTempGross();

            if (string.IsNullOrEmpty(tempTare) && string.IsNullOrEmpty(tempGross))
            {
                txtRakeNo.Text = DateTime.Now.Year + "000" + "001";
            }
            else
            {
                if (string.IsNullOrEmpty(tempTare))
                    tempTare = "0";

                if (string.IsNullOrEmpty(tempGross))
                    tempGross = "0";

                if (int.Parse(tempTare) > int.Parse(tempGross))
                {
                    txtRakeNo.Text = tempTare.ToString();
                }
                else
                {
                    txtRakeNo.Text = (int.Parse(tempGross) + 1).ToString();
                }
            }

             
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmDefaultImage().Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            txtTime.Text = dt.ToString("hh:mm:ss");
         
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                f_title = new System.Drawing.Font("Arial", 15, FontStyle.Bold); //GraphicsUnit.Point);
                DataTable dt = new DataTable();
                //dt = CommonFunctions.GetDataSet("Select * from Printermaster where id='1'").Tables[0];
                StringBuilder header = new StringBuilder();
                //header.Append(dt.Rows[0][1].ToString() + "\n" + dt.Rows[0][2].ToString() + "\n" + dt.Rows[0][3].ToString());
                header.Append(Environment.NewLine);
                header.Append("-----------------------------------------------------------------------------------------------------------");
                header.Append(Environment.NewLine);
                header.Append("Rake No\t :  " + txtRakeNo.Text);
                header.Append(Environment.NewLine);
                header.Append("Direction\t:  " + ""  + "\n"); //In
                header.Append(Environment.NewLine);
                var cdt = DateTime.Now;
                header.Append("Weighment Date :  " + txtDate.Text + " "+ txtTime.Text);
                header.Append(Environment.NewLine);
                header.Append("Print Date            :  " + cdt.ToString("g"));
                header.Append(Environment.NewLine);
                header.Append("------------------------------------------------------------------------------------------------------------");
                header.Append(Environment.NewLine);
                header.Append(Environment.NewLine);
                StringBuilder footer = new StringBuilder();
                footer.Append(Environment.NewLine);
                footer.Append(Environment.NewLine);
                footer.Append("NS=>Normal Speed,MS=>Marginal Speed,OS=>Over Speed");
                footer.Append(Environment.NewLine);
                footer.Append("Come Again");
                footer.Append(Environment.NewLine);
                footer.Append("All The Best");
                footer.Append(Environment.NewLine);
                footer.Append(Environment.NewLine);
                //if (op.ToLower() != "admin")
                //    footer.Append("Operator : " + op);
                //else
                footer.Append("Operator:" + "Operator");
                footer.Append(Environment.NewLine);
                footer.Append("Signature:");
                footer.Append(Environment.NewLine);
                if (SetupThePrinting(header.ToString(), footer.ToString()))
                {
                    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                    MyPrintPreviewDialog.Document = printDocument1;
                    MyPrintPreviewDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message.ToString(), ex.Message.GetType().ToString());
            }
        }

        private bool SetupThePrinting(string ti, string fo)
        {

            printDocument1.DefaultPageSettings.Margins = new Margins(35, 3, 30, 10);


            if (MessageBox.Show("Do you want the report to be centered on the page", "InvoiceManager - Center on Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MyDataGridViewPrinter = new DataGridViewPrinter(staticDataGrid, printDocument1, true, true, ti, true, fo, f_title, Color.Black, true, true, true);
            else
                MyDataGridViewPrinter = new DataGridViewPrinter(staticDataGrid, printDocument1, false, true, ti, true, fo, f_title, Color.Black, true, false, true);

            return true;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void btnEndofWeighment_Click(object sender, EventArgs e)
        {

        }
      



       
      
    }
}
