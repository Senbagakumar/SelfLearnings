using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeldingDataCollector
{
    public partial class Form1 : Form
    {
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;

        string _Server = string.Empty;
        int _Port = 0;

        Func<string, string> reader = key => ConfigurationManager.AppSettings[key];
        string strFileName = "";
        string strFolderPath = "";
        string strFilePath = "";
        string baseDirectory = "";
        int interval = 0;
        public Form1()
        {
            InitializeComponent();

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _Server = reader("Server");
            _Port = int.Parse(reader("Port"));
            baseDirectory = reader("DirectoryPath");
            interval = int.Parse(reader("Interval"));

            strFileName = DateTime.Now.ToString("HH-mm-ss");
            strFolderPath = DateTime.Now.ToString("dd-MMM-yyyy");

            timer1.Interval = interval;           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;             
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;            
        }

        private void OnExecutor()
        {
            //AssignStreamValue();
            using (TcpClient client = new TcpClient(_Server, _Port))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = new Byte[256];
                    string requestData = string.Empty;

                    try
                    {
                        int bytes = stream.Read(data, 0, data.Length);
                        requestData = Encoding.ASCII.GetString(data, 0, bytes);

                        if (!string.IsNullOrEmpty(requestData))
                            AssignStreamValue(requestData);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.OnExecutor();
        }

        private void AssignStreamValue(string streamValue="")
        {
            string loc = streamValue ;//"[58-570-40-60-55]";
            loc=loc.Replace("[", "");
            loc=loc.Replace("]", "");
            string[] stream = loc.Split('-');
            if (stream.Length > 0)
            {
                txtVoltage.Text = stream[0];
                txtCurrent.Text = stream[1];
                txtIdleTime.Text = stream[2];
                txtWeldingTime.Text = stream[3];
                txtWeldingCount.Text = stream[4];

                WriteRecordInCSV(stream);
            }
            Thread.Sleep(1000);
        }

        private void WriteRecordInCSV(string[] stream)
        {
            strFilePath = baseDirectory + @"\" + strFolderPath + @"\" + strFileName + ".csv";
            bool firstCheck = false;
            if (!Directory.Exists(baseDirectory + @"\" + strFolderPath))
            {
                Directory.CreateDirectory(baseDirectory + @"\" + strFolderPath);
                firstCheck = true;
            }
            using (FileStream fs = new FileStream(strFilePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                if (firstCheck)
                    sw.WriteLine("Voltage,Current,IdleTime,WeldingTime,WeldingCount,");

                if (!firstCheck)
                    sw.Write(sw.NewLine);

                foreach (string s in stream)
                {
                    sw.Write(s + ",");
                }
                sw.Write(sw.NewLine);
                sw.Flush();
                sw.Close();
            }
            
        }        
            
    }
}
