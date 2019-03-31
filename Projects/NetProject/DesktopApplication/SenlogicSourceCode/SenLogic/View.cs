using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;
using System.Configuration;

namespace SenLogic
{
    public partial class View : Form
    {

        
        int lastsno = 0, axcount = 1000;//,pend=0;
        private string[] cmbKeys = new string[8];
        private bool IsCmbKeys = false;
        string  input;//,input1;
        int weightlength = 0;
        
        double weight, weight1 = 0.0f;
        double speed, speed1 = 0.0f;

        TimeSpan fromTime = TimeSpan.MinValue;
        TimeSpan toTime = TimeSpan.MinValue;
        
        Random rand = new Random();
        DataTable dttest = new DataTable();
        DataTable dttemp = new DataTable();
        string From, To, Product;
        private const string cmbKeysvalue="ctrlF122627";
        bool isCommunicationError = true;
        bool isManualEntry = false;
        public View()
        {
            InitializeComponent();

            try
            {

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                weightlength = Convert.ToInt16(KeyReader("Weightlength"));


                var db = new DBEngine();
                var dtVendor = new DataSet();
                db.ExecuteQuery("Select * from ScreenCommon", null, dtVendor, "Vendors");
                
        


                ////ds = CommonFunctions.GetDataSet("Select * from commaster");
                dt = dtVendor.Tables[0];
                DataRow dr = dt.Rows[0] as DataRow;
                short p = Convert.ToInt16(dr["CommTrack1"].ToString().Replace("Com","").Trim());
                ////com2.CommPort = p;
                com2.CommPort = p;
                // This port is already open, then close.
                if (com2.PortOpen)
                    com2.PortOpen = false;

                // Trigger the OnComm event whenever data is received
                com2.RThreshold = 1;

                // Set the port to 9600 baud, no parity bit, 8 data bits, 1 stop bit (all standard)

                com2.Settings = dr["CommBaud1"].ToString();
               //// com2.Settings = "4800,n,8,1";

                // Force the DTR line high, used sometimes to hang up modems
                //com.DTREnable = true;

                com2.RTSEnable = true;

                // No handshaking is used
                com2.Handshaking = MSCommLib.HandshakeConstants.comNone;

                // Use this line instead for byte array input, best for most communications
                com2.InputMode = MSCommLib.InputModeConstants.comInputModeText;

                // Read the entire waiting data when com.Input is used
                com2.InputLen = 0;

                // Don't discard nulls, 0x00 is a useful byte
                com2.NullDiscard = false;

                // Attach the event handler
                //	com.OnComm += new System.EventHandler(this.OnComm);

                com2.PortOpen = true;
            }
            catch (SystemException ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show("Com Error Check The ComSetting\n" + ex.Message, ex.Message.GetType().ToString());
            }
        }

       
        private DataTable GetDefaultData()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery("Select Product, Source,Destination,Type from [Default]", null, dtVendor, "Default");
            return dtVendor.Tables[0];
        }


        private string GetRakeNumberFromTempTare()
        {

            using (var db = new DBEngine())
            {
               return db.ExecuteQuery("Select Max(RakeNo) from Temp");

            }
        }

        private string GetRakeNumberFromTempGross()
        {

            using (var db = new DBEngine())
            {
                return db.ExecuteQuery("Select Max(RakeNo) from TempGross");

            }
        }


        private void View_Load(object sender, EventArgs e)
        {            

            var db = new DBEngine();
            lblTitle.Text = db.ExecuteQuery("Select Screen1,Commtrack1, commBaud1 from ScreenCommon");

            disableControl();
            AssignRestCommandToComPort();

            DataTable dt = GetDefaultData();
            txtWagonType.Text = dt.Rows[0][3].ToString();
            From = dt.Rows[0][1].ToString();
            To = dt.Rows[0][2].ToString();
            Product = dt.Rows[0][0].ToString();


            AssignRakeNumber();
            


            this.KeyUp += View_KeyUp;
            this.KeyPress +=View_KeyPress;

            AssignDefaultColumns();
            
        }

        private void AssignRestCommandToComPort()
        {
            try
            {
                com2.Output = RegsetCommand();
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
            }
        }

        private string RegsetCommand()
        {
            char[] hj = new char[4];
            hj[0] = Convert.ToChar(2);
            hj[1] = Convert.ToChar(109);
            hj[2] = Convert.ToChar(64);
            hj[3] = Convert.ToChar(3);

            string s = new String(hj);
            return s;
        }


        int i = 0;
        void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbKeys[0] !=null && cmbKeys[0] != "ctrl")
            {
                cmbKeys = new string[8];
                i = 0;
                return;
            }

            if (cmbKeys[1] != null && cmbKeys[1] != "F12")
            {
                cmbKeys = new string[8];
                i = 0;
                return;
            }

            if (e.Shift && e.Control && e.KeyCode == Keys.F4)
            {
               IsCmbKeys = true;
               AssignRestCommandToComPort();
               btnEndofWeighment.Enabled = true;
               isCommunicationError = true;
            }
            if(!IsCmbKeys)
            {                
                if (e.Control)
                {
                    cmbKeys[i] = "ctrl";
                    i++;
                }
                if(e.KeyCode == Keys.F12)
                {
                    cmbKeys[i] = "F12";
                    i++;
                }
            }
            if(e.KeyCode == Keys.Enter)
            {
               
                string values = string.Join("", cmbKeys);

                if (values.Length != cmbKeysvalue.Length)
                {
                    i = 0;
                    cmbKeys = new string[8];
                    return;
                }
                if (values == cmbKeysvalue)
                {

                    txtDirection.Text = "OUT";
                    txtDirection.ReadOnly = false;
                    isManualEntry = true;

                    DateTime dateTime = DateTime.Now;
                    txtRakeArrivalDate.Text = dateTime.ToString(Shared_Variables.DateFormat);                  

                    pnlManualGross.Visible = true;
                    txtFromTime.Visible = true;
                    txtFromTime.Clear();
                    txtEndTime.Visible = true;
                    txtEndTime.Clear();
                    txtSlCount.Visible = true;
                    txtSlCount.Clear();
                    txtSlCount.ReadOnly = false;
                    txtFromTime.Focus();
                }
                else
                {
                    MessageBox.Show("Please Enter Exact Keys");
                }
                cmbKeys = new string[8];
                i = 0;
                
            }
          
        }

        private void AssignRakeNumber()
        {
            string tempTare = GetRakeNumberFromTempTare();

            

            if (string.IsNullOrEmpty(tempTare))
            {
                txtRakeNo.Text = DateTime.Now.Year + "000" + "001";
            }
            else
            {
                
                int year = Convert.ToInt16(tempTare.Substring(0, 4));
                string rakeValue = tempTare.Replace(year.ToString(), "");

                if (year != DateTime.Now.Year)
                    txtRakeNo.Text =(int.Parse(DateTime.Now.Year + rakeValue.ToString()) +1).ToString();
                else
                    txtRakeNo.Text = (int.Parse(tempTare) + 1).ToString();
                
            }
        }

        private void AssignDefaultColumns()
        {
            dttemp = new DataTable();
            dttemp.Columns.Add("Sno");
            dttemp.Columns.Add("Weight");
            dttemp.Columns.Add("Speed");
            dttemp.Columns.Add("Remarks");
            dttemp.Columns.Add("Ctime");
            dataGridView1.DataSource = dttemp;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "00.00";
            //dataGridView1.Columns[4].Visible = false;


            //double weigh = 4.4;
            //double speed = 44;
            //for (int i = 0; i < 2; i++)
            //{
            //    DataRow dr = dttemp.NewRow();
            //    dr[0] = (i + 1);
            //    dr[1] = weigh.ToString("00.00");
            //    dr[2] = speed.ToString("00.00");
            //    dr[3] = "X";
            //    dttemp.Rows.Add(dr);

            //}

            //DateTime dt = DateTime.Now;
            //txtRakeArrivalDate.Text = dt.ToString("MM/dd/yyyy");
            //txtRakeArrivalTime.Text = dt.ToString("hh:mm:ss");

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //string[] cha = new string[20];
        //int inpu = 0;

        double commulativeWeigh = 0.0;

        public static Func<string, string> KeyReader = key => System.Configuration.ConfigurationManager.AppSettings[key];



        private string ConfirmCommunication()
        {
            char[] hj = new char[4];
            hj[0] = Convert.ToChar(2);
            hj[1] = 's';
            hj[2] = 'c';
            hj[3] = Convert.ToChar(3);

            string s = new String(hj);
            return s;
        }
        private void com2_OnComm(object sender, EventArgs e)
        {
            Thread.Sleep(50);
            try
            {
                input = com2.Input.ToString();
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }

            if (input.Contains("M"))
            {
                txtstatus.Text = "Axle Forward";
            }
            else if (input.Contains("M"))
            {
                txtstatus.Text = "Axle Reverse";
            }

            if (input.Contains("SC"))
            {
                string waitingTime= KeyReader("PortWeightTime");
                txtstatus.Text = "Communication OK";
                com2.Output = ConfirmCommunication();
                isCommunicationError = false;
 
            }
            else if (input.Contains("M@"))
            {
                string waitingTime = KeyReader("PortWeightTime");
                Thread.Sleep(int.Parse(waitingTime));
                txtstatus.Text = "Ready To Weigh";

                isCommunicationError = false;

            }
            if (isCommunicationError)
            {
                Thread.Sleep(5000);
                txtstatus.Text = "Communication Error...!!!";
            }
                       

            switch (input.Length)
            {
               
                case 18:
                case 19:
                    string unknownV = input.Substring(2, 1);
                    try
                    {
                        if (input.Contains(""))
                        {
                            txtstatus.Text = "UnKnown Vehicle";
                        }

                    }
                    catch (Exception ex)
                    {
                        Shared_Functions.Logged_ErrorMessage(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case 4:
                    string halfE = input.Substring(2, 1);
                    try
                    {
                        if (input.Contains(""))
                        {
                            txtstatus.Text = "Half Engine Passed";
                        }
                        else if (input.Contains(""))
                        {
                            txtstatus.Text = "IN Direction detected";
                            DateTime dt = DateTime.Now;
                            txtRakeArrivalDate.Text = dt.ToString(Shared_Variables.DateFormat);
                            txtRakeArrivalTime.Text = dt.ToString(Shared_Variables.TimeFormatWithSeconds);
                            txtDirection.Text = "IN";

                        }
                        else if (input.Contains("")) // 
                        {
                            DateTime dt = DateTime.Now;
                            txtRakeArrivalDate.Text = dt.ToString(Shared_Variables.DateFormat);
                            txtRakeArrivalTime.Text = dt.ToString(Shared_Variables.TimeFormatWithSeconds);
                            txtstatus.Text = "OUT Direction detected";
                            txtDirection.Text = "OUT";
                        }

                    }
                    catch (Exception ex)
                    {
                        Shared_Functions.Logged_ErrorMessage(ex.Message);
                        MessageBox.Show(ex.Message);
                    }

                    break;

                case 5:
                    try
                    {
                        if (input.Contains("MB"))
                        {
                            txtstatus.Text = "Full Engine Passed";
                        }


                    }
                    catch (Exception ex)
                    {
                        Shared_Functions.Logged_ErrorMessage(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case 10:
                    if (input.Contains("M") && input.Contains("\r"))
                    {
                        speed = Convert.ToDouble(input.Substring(3, 6).Trim());
                        speed1 = speed;
                        txtstatus.Text = "Bogie Weighed";
                    }
                    else if (input.Contains("M") && input.Contains("\n"))
                    {
                        weight = Convert.ToDouble(input.Substring(3, weightlength).Trim());
                        weight1 = weight;
                        txtstatus.Text = "Wagon Weighed";
                    }
                    else
                    {
                        weight = Convert.ToDouble(input.Substring(3, weightlength).Trim());
                        weight1 = weight;
                        txtstatus.Text = "Wagon Weighed";
                    }
                    
                    break;
                case 14:
                    if (input.Contains("M") && input.Contains("\n"))
                    {
                        weight = Convert.ToDouble(input.Substring(7, weightlength).Trim());
                        weight1 = weight;
                        txtstatus.Text = "Wagon Weighed";
                    }
                    else
                    {
                        weight = Convert.ToDouble(input.Substring(7, weightlength).Trim());
                        weight1 = weight;
                        txtstatus.Text = "Wagon Weighed";
                    }
                    break;
                default:
                   //txtstatus.Text = input.ToString()+"-Length"+input.ToString().Length;
                   break;           
            }   
           
            if (weight1 != 0.0 && speed1 != 0.0)
            {
                DataRow dr;
                dr = dttemp.NewRow();
                
                //weight1 = weight1 * 1000;

                //weight1 = (Convert.ToInt32(weight1 / res)) * res;
                weight1 = weight1 / 1000;


                int serialNumber = ++lastsno;

                dr[1] = weight1.ToString("00.00");
                dr[0] = serialNumber;
                               
                dr[2] = (speed1 / axcount).ToString("00.00");
                
                if ((speed1 / axcount) <= 9)
                    dr[3] = "X"; //"NS"; 
                if ((speed1 / axcount) > 9 && (speed1 / axcount) <= 15)
                    dr[3] = "MS";
                if ((speed1 / axcount) > 15)
                    dr[3] = "OS";

                if ((speed1 / axcount) > 21)
                    dr[1] = 0;

                commulativeWeigh += weight1;

              //  txtweight.Text = serialNumber.ToString() + ":   " + weight1.ToString("00.00") + ":  " + commulativeWeigh.ToString("00.00");
                labSlno.Text = serialNumber.ToString(); lblWeight.Text = weight1.ToString("00.00"); lblCommulativeWeight.Text = commulativeWeigh.ToString("00.00");
                dr[4] = DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds);
                dttemp.Rows.Add(dr);
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                
                weight1 = 0;
                speed1 = 0;

                
            }   

        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {           

            DateTime dt = DateTime.Now;
            txtDateTime.Text = dt.ToString(Shared_Variables.DateFormat) +" "+ dt.ToString(Shared_Variables.TimeFormatWithSeconds);
            

        }

        private DataTable GetDefaultPccData()
        {
            var db = new DBEngine();
            var dtVendor = new DataSet();
            db.ExecuteQuery(string.Format("Select[Wpcclimit],[Wtol] from [Wagon] where [Wtype]='{0}'",txtWagonType.Text.Trim()), null, dtVendor, "Default");
            return dtVendor.Tables[0];
        }

        private void clear()
        {
            txtRakeArrivalDate.Clear();
            txtRakeArrivalTime.Clear();
            txtstatus.Clear();            
            txtweight.Clear();
            txtDirection.Clear();
            txtRakeNo.Clear();
            txtDateTime.Clear();
            txtSlCount.Clear();
            lblCommulativeWeight.Text = string.Empty;
            lblWeight.Text = string.Empty;
            labSlno.Text = string.Empty;
            btnPrint.Enabled = false;
            txtDirection.ReadOnly = true;
            isManualEntry = false;
            dttemp.Rows.Clear();
        }

        private void btnEndofWeighment_Click(object sender, EventArgs e)
        {        
            
            if (dttemp.Rows.Count <= 0)
                return;

            if (MessageBox.Show("Do you want to Save All The Records", "Save Dialog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string totime = string.Empty;

            //if (toTime == TimeSpan.MinValue)

            toTime = TimeSpan.Parse(DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds));
            totime = toTime.ToString();
            //totime = toTime.ToString(Shared_Variables.TimeFormatWithSeconds);
            //else
            //totime = toTime.ToString();



            btnEndofWeighment.Enabled = false;

              var db = new DBEngine();
            
              var dt=new System.Data.DataTable();
              System.Data.OleDb.OleDbConnection cn=new System.Data.OleDb.OleDbConnection(db.ConnectionString);
           //   //System.Data.OleDb.

             string insertQuery = string.Empty;
             
             DataTable limit = GetDefaultPccData();
             string pccLimit = limit.Rows[0][0].ToString();
             string Wtol = limit.Rows[0][1].ToString();
             string query = string.Format("Select ID,Slno,RakeNo,WagonNo,WagonType,Tare,datein,timein,Gross,dateout,timeout,spd,PCC,[From],[Product],[To],spdRemarks,direction,rdate,WagonTime from Temp where RakeNo={0}", txtRakeNo.Text);

             

             System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(query, cn);
             System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(cmd);
             System.Data.OleDb.OleDbCommandBuilder ocb = new System.Data.OleDb.OleDbCommandBuilder(da);
           
             da.Fill(dt);
             ocb.QuoteSuffix = "]";
             ocb.QuotePrefix = "[";

             string time = string.Empty;

             for (int i = 0; i < dttemp.Rows.Count; i++)
             {
                 DataRow dr = dt.NewRow();
                 dr["Slno"] = dttemp.Rows[i][0];
                 dr["RakeNo"] = txtRakeNo.Text;
                 dr["WagonType"] = txtWagonType.Text;
                 dr["WagonNo"] = "X";

                 string inTime = txtRakeArrivalTime.Text != string.Empty ? txtRakeArrivalTime.Text : DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds);


                 if(txtDirection.Text == "IN")
                 {
                     dr["datein"] = txtDateTime.Text.Split(' ')[0];

                    if (i == 0)
                        time = inTime;
                    else if (i == dttemp.Rows.Count - 1)
                        time = totime;
                    else
                        time = dttemp.Rows[i][4].ToString();

                     dr["timein"] = time;
                 }
                 else
                 {                     
                     dr["dateout"] = txtDateTime.Text.Split(' ')[0];

                     if (i == 0)                                              
                         time = inTime;
                     else if (i == dttemp.Rows.Count - 1)
                        time = totime; 
                     else
                         time = dttemp.Rows[i][4].ToString();

                    dr["timeout"] = time;
                 }

                string rDate = string.Empty;
                if (isManualEntry)
                {
                    if (i == 0)
                    {
                        dr["timein"] = dr["timeout"] = txtFromTime.Text;
                    }
                    else if (i == dttemp.Rows.Count - 1)
                    {
                        var endTime = TimeSpan.Parse(txtEndTime.Text);
                        var wagonEndTime =TimeSpan.Parse(dttemp.Rows[i][4].ToString());
                        if (endTime < wagonEndTime)
                            txtEndTime.Text = endTime.Add(TimeSpan.FromSeconds(10)).ToString();

                        dr["timein"] = dr["timeout"] = txtEndTime.Text;
                    }
                    else
                    {
                        dr["timein"] = dr["timeout"] = dttemp.Rows[i][4];
                    }
                    
                }

                 dr["Gross"] = dttemp.Rows[i][1];
                 dr["Tare"] = "0";
                 dr["Product"] = Product;
                 dr["spd"] = dttemp.Rows[i][2];
                 dr["Pcc"] = pccLimit;
                 dr["From"]=From;
                 dr["To"]=To;
                 dr["spdRemarks"] = dttemp.Rows[i][3];
                 dr["direction"] = txtDirection.Text;
                 dr["WagonTime"] = dttemp.Rows[i][4];
                 //dr["rdate"] = txtDateTime.Text;
                 rDate = txtDateTime.Text.Split(' ')[0] + " " + time;                                 
                 dr["rdate"] = rDate;
                 dt.Rows.Add(dr);
             }
             da.Update(dt);           
            MessageBox.Show("Data Saved Successfully");
            btnPrint.Enabled = true;
            txtDirection.ReadOnly = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (com2.PortOpen) com2.PortOpen = false;
            this.Hide();
            new frmDefaultImage().Show();
        }
        private void Print()
        {

            //PrinterSettings printerSettings = new PrinterSettings();
            //IQueryable<PaperSize> paperSizes = printerSettings.PaperSizes.Cast<PaperSize>().AsQueryable();
            //PaperSize a4rotated = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A3).First();

            //var psettings = new PageSettings();
            //psettings.PrinterSettings = printerSettings;

            PrintDocument pd = new PrintDocument();
            
           // pd.DefaultPageSettings = psettings;
            pd.PrintPage += new PrintPageEventHandler(PrintImage);

            if (!Convert.ToBoolean(Shared_Variables.printPreviewEnable))
            {
                PaperSize ps = new PaperSize("sd", pd.DefaultPageSettings.PaperSize.Width, (141 + (dataGridView1.Rows.Count * 15) + 200));
                pd.DefaultPageSettings.PaperSize = ps;                
                pd.Print();
            }
            else
            {

                PaperSize ps = new PaperSize("sd", pd.DefaultPageSettings.PaperSize.Width, (141 + (dataGridView1.Rows.Count * 15) + 200));

                pd.DefaultPageSettings.PaperSize = ps;
                PrintPreviewDialog pview = new PrintPreviewDialog();

                pview.Document = pd;
                pview.ShowDialog();
            }

        }
        int printRowCount = 0; bool onceHeader = false; int j = 1; double WSpeed = 0.0;
        int FooterDotLine1 = 0; int FooterDotLine2 = 0; int FooterTotal = 0; int FooterSign = 0; int currentpage = 0;
        int totalheight = 0;
       
        void PrintImage(object o, PrintPageEventArgs e)
        {
            try
            {
                
                int width = 200;
                int height = 15;
                int origin = 100;
                
                int GRDH = 25;
                int y = 0;
                


                int Corigin = 60;

                int GNRDW = 100;
                Pen dashed_pen = new Pen(Color.Black, 1.5f);

                StringFormat str = GetStringFormatValue();
                if (!onceHeader)
                    height = PrintHeader(e, width, height, origin, GRDH, Corigin, GNRDW, Shared_Variables.dotLine, str);
                else
                    height = 0;
               
                int totalCount = dataGridView1.Rows.Count;

                for(int i=printRowCount; i<totalCount; i++)
                {
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    height += Shared_Variables.SpaceValue;
                    e.Graphics.DrawString(dgvr.Cells[0].Value.ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr.Cells[4].Value.ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 80, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr.Cells[1].Value.ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 270, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr.Cells[2].Value.ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 420, height, GNRDW, GRDH), str);
                    e.Graphics.DrawString(dgvr.Cells[3].Value.ToString(), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);
                    
                    WSpeed += double.Parse(dgvr.Cells[1].Value.ToString());

                    printRowCount++; //RowCount++;

                    totalheight += height;
                    if (CheckHeight(height, currentpage))
                    {
                        e.HasMorePages = true;
                        onceHeader = true;
                        currentpage++;
                        j++;
                        return;
                    }                   
                }

                if (FooterDotLine1 == 0)
                {

                    height += 18;
                    totalheight += height;
                }


                if (!CheckHeight(height, currentpage))
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
                    onceHeader = true;
                    currentpage++;
                    return;

                }
               
                if(FooterTotal == 0)
                    height += 18;

                string leavingTime = string.Empty;               

                if(isManualEntry)
                {
                    leavingTime = txtEndTime.Text;

                    var endTime = TimeSpan.Parse(txtEndTime.Text);
                    var wagonEndTime = TimeSpan.Parse(dttemp.Rows[totalCount-1][4].ToString());
                    if (endTime < wagonEndTime)
                        leavingTime = endTime.Add(TimeSpan.FromSeconds(10)).ToString();
                }
                else
                {
                    if (toTime == TimeSpan.MinValue)
                        leavingTime = DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds);
                    else
                        leavingTime = toTime.ToString(Shared_Variables.TimeSpanFormatWithSeconds);
                    //leavingTime = TimeSpan.Parse(dataGridView1.Rows[totalCount - 1].Cells[4].Value.ToString()).ToString(Shared_Variables.TimeSpanFormat);


                }

                if (!CheckHeight(height, currentpage))
                {
                    if (FooterTotal == 0)
                    { 
                       
                        e.Graphics.DrawString("TOTAL (MT):", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin+80, height, GNRDW + 60, GRDH), str);
                        e.Graphics.DrawString(WSpeed.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 230, height, GNRDW, GRDH), str);

                        e.Graphics.DrawString("Train Leaving Time:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
                        e.Graphics.DrawString(leavingTime, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

                        FooterTotal = 1;
                    }

                }
                else
                {
                    onceHeader = true;
                    e.HasMorePages = true;
                    currentpage++;
                    return;
                }
               
                if(FooterDotLine2 ==0)
                    height += 18;
               
                if (!CheckHeight(height, currentpage))
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
                    onceHeader = true;
                    currentpage++;
                    return;
                }               
                
                
                if(FooterSign ==0)
                   height += 50;

                if (!CheckHeight(height, currentpage))                
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
                    onceHeader = true;
                    currentpage++;
                    return;
                }  

                //height = PrintFooter(e, width, height, origin, GRDH, Corigin, GNRDW, dotLine, str, WSpeed);
                printRowCount = 0; 
                onceHeader = false;
                WSpeed = 0.0;
                FooterDotLine1 = 0; FooterDotLine2 = 0; FooterTotal = 0; FooterSign = 0; currentpage = 0;
                j = 1;

             
                    
            }
            catch(Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckHeight(int height, int currentPage)
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

        //private int PrintFooter(PrintPageEventArgs e, int width, int height, int origin, int GRDH, int Corigin, int GNRDW, string dotLine, StringFormat str, double WSpeed)
        //{
        //    //1041
        //    height += 18;

          
        //        e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
          
        //    height += 18;

        //    e.Graphics.DrawString("TOTAL (MT):", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
        //    e.Graphics.DrawString(WSpeed.ToString("00.00"), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 100, height, GNRDW, GRDH), str);


        //    e.Graphics.DrawString("Train Leaving Time:", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
        //    e.Graphics.DrawString(toTime.ToString(Shared_Variables.TimeFormat), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

        //    height += 18;
        //    e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
        //    height += 50;
        //    e.Graphics.DrawString("All Weights are in Tonnes", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, width + 100, GRDH), str);
        //    e.Graphics.DrawString("Authorised Signature", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW + 100, GRDH), str);
        //    return height;
        //}

        private int PrintHeader(PrintPageEventArgs e, int width, int height, int origin, int GRDH, int Corigin, int GNRDW, string dotLine, StringFormat str)
        {
            var dt = new DataSet();
            new DBEngine().ExecuteQuery("select Report1 from ScreenCommon", null, dt, "ReportHeader");
            string[] headers = dt.Tables[0].Rows[0][0].ToString().Split(',');



            if (headers.Length > 1)
            {
                e.Graphics.DrawString(headers[0], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 400, GRDH), str);
                height += 18;
                e.Graphics.DrawString(headers[1], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(350 - headers[1].Length, height, width + 300, GRDH), str);
            }
            else
                e.Graphics.DrawString(headers[0], Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(300 - headers[0].Length, height, width + 300, GRDH), str);

            height += 18;
            //e.Graphics.DrawString(dotLine, new Font(FontFamily.GenericSerif, 20, FontStyle.Regular), Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
            e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

            height += 18; 

            
            e.Graphics.DrawString("RAKE No", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 90, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(txtRakeNo.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 100, height, GNRDW, GRDH), str);


            e.Graphics.DrawString("DIRECTION", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 560, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 640, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(txtDirection.Text, Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

            height += 18; 

            e.Graphics.DrawString("WEIGHMENT ON", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 90, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(txtRakeArrivalDate.Text!=string.Empty ? txtRakeArrivalDate.Text : DateTime.Now.ToString(Shared_Variables.DateFormat), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 100, height, GNRDW, GRDH), str);

            e.Graphics.DrawString("PRINTING TIME", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin+250, height, GNRDW + 60, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 350, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 370, height, GNRDW, GRDH), str);


            string rakeArrivalTime = string.Empty;

            if(isManualEntry)
            {
                rakeArrivalTime = txtFromTime.Text;
            }
            else
            {
                if (txtRakeArrivalTime.Text != string.Empty && txtRakeArrivalTime.Text.Length > 6)
                    rakeArrivalTime = txtRakeArrivalTime.Text; 
                //rakeArrivalTime = txtRakeArrivalTime.Text.Substring(0, txtRakeArrivalTime.Text.Length - 3);
                else
                    rakeArrivalTime = TimeSpan.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString()).ToString(Shared_Variables.TimeSpanFormatWithSeconds);
            }
            

            e.Graphics.DrawString("TIME IN", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 560, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(":", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 640, height, GNRDW, GRDH), str);
            e.Graphics.DrawString(rakeArrivalTime != string.Empty ? rakeArrivalTime : DateTime.Now.ToString(Shared_Variables.TimeFormatWithSeconds), Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 650, height, GNRDW, GRDH), str);

            height += 18; 
            e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);

            height += 18;

            e.Graphics.DrawString("SLNO", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("TIME", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 80, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("WEIGHT", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 270, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("SPEED", Shared_Variables.GetFont, Brushes.Black, new RectangleF(Corigin + 420, height, GNRDW, GRDH), str);
            e.Graphics.DrawString("REMARKS", Shared_Variables.GetFont, Brushes.Black, new RectangleF(origin + 500, height, GNRDW, GRDH), str);

            height += 18;
            e.Graphics.DrawString(dotLine, Shared_Variables.GetFontForHeader, Brushes.Black, new RectangleF(Corigin, height, width + 610, GRDH), str);
            return height;
        }

        private static StringFormat GetStringFormatValue()
        {
            StringFormat str = new StringFormat();
            str.Alignment = StringAlignment.Near;
            str.LineAlignment = StringAlignment.Center;
            str.Trimming = StringTrimming.EllipsisCharacter;
            return str;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dttemp.Rows.Count <= 0)
                return;

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


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            AssignDefaultColumns();           
            btnEndofWeighment.Enabled = true;
            AssignRakeNumber();
            disableControl();
            AssignRestCommandToComPort();
            
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (com2.PortOpen) com2.PortOpen = false;
        }

        
        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbKeys[0] != null && cmbKeys[0] != "ctrl")
            {
                cmbKeys = new string[8];
                i = 0;
                return;
            }

            if (cmbKeys[1] != null && cmbKeys[1] != "F12")
            {
                cmbKeys = new string[8];
                i = 0;
                return;
            }

            if(e.KeyChar == (char)Keys.D2)
            {
                cmbKeys[i] = "2";
                i++;
                //is2KeyEnable = false;
            }
            if (e.KeyChar == (char)Keys.D6)
            {
                cmbKeys[i] = "6";
                i++;
            }
            //if (e.KeyChar == (char)Keys.D2)
            //{
            //    cmbKeys[i] = "2";
            //    i++;
            //}
            if (e.KeyChar == (char)Keys.D7)
            {
                cmbKeys[i] = "7";
                i++;
            }
            if(e.KeyChar == (char)Keys.Enter)
            {
                string values = string.Join("", cmbKeys);               

                if (values.Length != cmbKeysvalue.Length)
                {
                    i = 0;
                    cmbKeys = new string[8];
                    return;
                }
                if (values == cmbKeysvalue)
                {
                    
                    txtDirection.Text = "OUT";
                    txtDirection.ReadOnly = false;
                    isManualEntry = true;

                    DateTime dateTime = DateTime.Now;
                    txtRakeArrivalDate.Text = dateTime.ToString(Shared_Variables.DateFormat);                    

                    pnlManualGross.Visible = true;
                    txtFromTime.Visible = true;
                    txtFromTime.Clear();
                    txtEndTime.Visible = true;
                    txtEndTime.Clear();
                    txtSlCount.Visible = true;
                    txtSlCount.Clear();
                    txtSlCount.ReadOnly = false;
                    txtFromTime.Focus();
                }
                else
                {
                    MessageBox.Show("Please Enter Exact Keys");
                }
                cmbKeys = new string[8];
                i = 0;
            }
        }

        private  void disableControl()
        {
            pnlManualGross.Visible = false;
            txtFromTime.Visible = false;
            txtEndTime.Visible = false;
            txtSlCount.Visible = false;
            
            
        }

        private void enableControl()
        {
            pnlManualGross.Visible = true;
            txtFromTime.Visible = true;
            txtEndTime.Visible = true;
            txtSlCount.Visible = true;
            txtSlCount.Enabled = true;
        }

        private void txtSlCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                 //txtDirection.Text = "IN";

                
                 if(!string.IsNullOrEmpty(txtFromTime.Text))
                 {
                     TimeSpan.TryParse(txtFromTime.Text, out fromTime);
                     if (fromTime == TimeSpan.MinValue)
                         return;

                 }
                 if (!string.IsNullOrEmpty(txtEndTime.Text))
                 {
                     TimeSpan.TryParse(txtEndTime.Text, out toTime);
                     if (toTime == TimeSpan.MinValue)
                         return;

                 }
                 if (string.IsNullOrEmpty(txtSlCount.Text))
                     return;

                 TimeSpan diff = toTime - fromTime;
                 int slno = 0;
                int seconds = 0;
                 if (txtSlCount.Text != string.Empty)
                 {
                     int.TryParse(txtSlCount.Text, out slno);
                     if (slno == 0)
                         return;
                    seconds = Convert.ToInt16(diff.TotalSeconds / slno);

                 }

                dttemp.Rows.Clear();

                if (isManualEntry)
                    txtRakeArrivalTime.Text = txtFromTime.Text;                

                double totalWeight = 0.0;
                for(int i=0; i<slno; i++)
                {
                    DataRow dr = dttemp.NewRow();

                    double weight =Math.Round(GetRandomNumber(80.0, 95.0),2);
                    double speed =Math.Round(GetRandomNumber(5.00, 8.00),2);

                    
                    totalWeight += weight;

                    dr[0] = (i+1);
                    dr[1]= weight.ToString("00.00");
                    dr[2] = speed.ToString("00.00");
                    dr[3] = "X";
                    fromTime = fromTime.Add(TimeSpan.FromSeconds(seconds));
                    var leavingTime = TimeSpan.Parse(txtEndTime.Text);
                    if (leavingTime > fromTime)
                        dr[4] = fromTime.ToString(Shared_Variables.TimeSpanFormatWithSeconds);
                    else
                        dr[4] = txtEndTime.Text;

                    labSlno.Text = (i + 1).ToString();
                    lblWeight.Text = weight.ToString("00.00");
                    lblCommulativeWeight.Text = totalWeight.ToString("00.00");

                    dttemp.Rows.Add(dr);
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;

                    txtFromTime.Visible = false;
                    txtEndTime.Visible = false;
                }
                txtSlno.Focus();
                txtSlCount.ReadOnly = true;
            }
        }

        private void CalculateWeight()
        {
            double weight = 0.0;
            double totalWeight = 0.0;

            for(int i=0; i<dataGridView1.Rows.Count; i++)
            {
                weight = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);            
                totalWeight += weight;
            }
            lblCommulativeWeight.Text = totalWeight.ToString("00.00");
        }

        private double GetRandomNumber(double min, double max)
        {
            Random ra = new Random();
            return ra.NextDouble() * (max - min) + min;

        }

        private void txtSpd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSlno.Text))
                    return;

                if (string.IsNullOrEmpty(txtGrsWt.Text))
                    return;

                if (string.IsNullOrEmpty(txtSpd.Text))
                    return;

                int slnumber = Convert.ToInt16(txtSlno.Text);

                dataGridView1.Rows[slnumber-1].Cells[1].Value = txtGrsWt.Text;
                dataGridView1.Rows[slnumber-1].Cells[2].Value = txtSpd.Text;
               // dataGridView1.Refresh();

                txtSlno.Clear(); txtGrsWt.Clear(); txtSpd.Clear(); txtSlno.Focus();
                CalculateWeight();
                //dgRakeView.Rows[rownumb].Cells["wagonno"].Value = txtWagonNo.Text;
            }
        }

        private void txtFromTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtFromTime.Text) && txtFromTime.Text.Length > 1)
                {
                    txtEndTime.Focus();
                }
            }
        }

        private void txtEndTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtEndTime.Text) && txtEndTime.Text.Length > 1)
                {
                    txtSlCount.Focus();
                }
            }
        }

        private void txtSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSlno.Text) && txtSlno.Text.Length > 0)
                {
                    txtGrsWt.Focus();
                }
            }
        }

        private void txtGrsWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtGrsWt.Text) && txtGrsWt.Text.Length > 0)
                {
                    txtSpd.Focus();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

        }
        

    }
}
 
    
