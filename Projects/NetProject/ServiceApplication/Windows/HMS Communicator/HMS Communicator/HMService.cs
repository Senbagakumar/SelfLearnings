using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HMS_Communicator
{
    public partial class HMService : ServiceBase
    {
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;        

        string _Source = "HMService";
        string _Log = "Application";
       
        string _Server = string.Empty;
        int _Port = 0;
        string _MySqlConnection = string.Empty;       
              
        //Init the logger
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        Func<string, string> reader = key => ConfigurationManager.AppSettings[key];
        public HMService()
        {
            InitializeComponent();

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            if (!EventLog.SourceExists(_Source))
                EventLog.CreateEventSource(_Source, _Log);
           
            _Server = reader("Server");
            _Port = int.Parse(reader("Port"));
            _MySqlConnection = reader("CodifyhmsMySqlServer");

        }
        protected override void OnStart(string[] args)
        {
            try
            {
                while (!this._cancellationToken.IsCancellationRequested)
                {
                    Task.Factory.StartNew(() =>
                    {
                        this.OnExecutor();
                    },
                    _cancellationToken).ContinueWith(t =>
                    {
                        var aggException = t.Exception.Flatten();
                        LogException(aggException);
                    }, TaskContinuationOptions.OnlyOnFaulted);
                    
                }

               
            }
            catch (Exception ex)
            {               
                LogException(ex);                
            }
        }
        protected override void OnStop()
        {
            LogEvent("Service Stoped");
            logger.Info("Service Stoped");
            this._cancellationTokenSource.Cancel();            
            this.Stop();           
        }
        public void OnExecutor()
        {
            LogEvent("Service Started");
            logger.Info("Service Started");
           
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

                        LogEvent($"Received: {requestData}");
                        logger.Info($"Received: {requestData}");

                        string msgDistinctionCode = requestData.Substring(1, 2);

                        if (msgDistinctionCode.Equals("R "))
                        {
                            var sampleInformationRequest = GetRequest(requestData, false, true);//system_no, sample_type, sample_no, sample_id                         
                            var sampleId = sampleInformationRequest.Item4;
                            var sampleNo = sampleInformationRequest.Item3;

                            if (!string.IsNullOrEmpty(sampleId.Trim()))
                            {
                                Analysis analysisInfo = SampleInformationResponse(sampleId, sampleNo, " ");
                                var pbytes = GetRequestBytes(analysisInfo, true);
                                stream.Write(pbytes, 0, pbytes.Length);
                                stream.Flush();

                                LogEvent($"Sent Sample Information Response for Sample ID: {sampleId};  Data:{Encoding.ASCII.GetString(pbytes)}");
                                logger.Info($"Sent Sample Information Response for Sample ID: {sampleId};  Data:{Encoding.ASCII.GetString(pbytes)}");
                            }
                            else
                            {
                                LogEvent("Sample Id was not set. Skipping this request.");
                                logger.Warn("Sample Id was not set. Skipping this request.");
                            }                                      

                        }
                        else if (msgDistinctionCode.Equals("DB"))
                        {
                            LogEvent("Received DB");
                            logger.Info("Received DB");
                        }
                        else if (msgDistinctionCode.Equals("D "))
                        {
                            //Analysis Data/Test Results.
                            //TODO:: Store these results in the appropriate tables.

                            //Analysis Data/Test Results.
                            logger.Info("Received Analysis Data.");
                            LogEvent("Received Analysis Data.");

                            var ad = new AnalysisData();
                            Analysis analysis = ad.Parse(requestData, false, true);
                            //Store these results in the appropriate tables.
                            ad.SaveSampleTestData(analysis, _MySqlConnection,logger);

                        }
                        else if (msgDistinctionCode.Equals("RE"))
                        {
                            //Request End.
                            //Machine status changed from Running to 'StandBy'. Send a Response End message.
                            byte[] sampleEnd = GetSampleEndMsg();
                            stream.Write(sampleEnd, 0, sampleEnd.Length);
                            stream.Flush();

                            LogEvent($"Sent SE message:{Encoding.ASCII.GetString(sampleEnd)}");
                            logger.Info($"Sent SE message:{Encoding.ASCII.GetString(sampleEnd)}");
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                        LogException(ex);
                    }
                    finally
                    {
                        // Close everything.
                        stream.Flush();
                        stream.Close();
                        client.Close();
                    }
                }
            }
            logger.Warn("Program was stopped.");
            Thread.Sleep(1000);
        }
        public Byte[] GetRequestBytes(Analysis analysis, bool useSampleType)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(ConstantData.STARTTEXT);
                sb.Append(ConstantData.RESPONSE_START_CODE);
                if (useSampleType)
                {
                    sb.Append(analysis.Sample_type);
                }
                sb.Append(analysis.Sample_no.PadLeft(ConstantData.SAMPLE_NO_SIZE, '0'));
                sb.Append(analysis.Sample_id.PadLeft(ConstantData.SAMPLE_ID_SIZE, ' '));
                sb.Append(ConstantData.Dummy4);
                sb.Append("E"); //TODO::Recalculate based on number of messages being sent for this sample. Refer Manual.
                sb.Append(analysis.Psex);
                sb.Append(analysis.Pageyear.PadLeft(3, '0'));
                sb.Append(analysis.Pagemonth.PadLeft(2, '0'));
                sb.Append(analysis.Pinformation.PadLeft(20, ' '));
                //Append the test codes information.
                for (int ix = 0; ix < analysis.OnlineTestInfo.Count; ++ix)
                {
                    sb.Append(analysis.OnlineTestInfo[ix][0].PadLeft(ConstantData.ONLINE_TEST_NO_SIZE, '0')); //Online Test Code default size is 3 chars.
                    sb.Append(analysis.OnlineTestInfo[ix][1]); //Diluent Information.
                }
                sb.Append(ConstantData.ENDTEXT);
            }
            catch (Exception e)
            {
                logger.Error(e);
                LogException(e);
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }
        public byte[] GetSampleEndMsg()
        {
            StringBuilder sb = new StringBuilder()
                .Append(ConstantData.STARTTEXT)
                .Append(ConstantData.RESPONSE_END_CODE)
                .Append(ConstantData.ENDTEXT);

            return Encoding.ASCII.GetBytes(sb.ToString());
        }        
        private Tuple<string, string,string,string> GetRequest(string inputText,bool useSystemNumber,bool useSampleType)
        {
            int strIndex = 0;
            strIndex++;
            string system_no = string.Empty;
            string sample_id = string.Empty;
            string sample_no = string.Empty;
            string sample_type = string.Empty;

            if (!inputText.Substring(strIndex, 2).Equals("R "))
            {
                return new Tuple<string, string, string, string>(system_no, sample_type, sample_no, sample_id);
            }

            strIndex += 2;
            if (useSystemNumber)
            {
                system_no = inputText.Substring(strIndex, 2);
                system_no = system_no.Trim();
                strIndex += 2;
            }

            if (useSampleType)
            {
                sample_type = inputText.Substring(strIndex, 1);
                strIndex += 1;
            }

            sample_no = inputText.Substring(strIndex, ConstantData.SAMPLE_NO_SIZE);
            sample_no = sample_no.Trim();
            strIndex += ConstantData.SAMPLE_NO_SIZE;
            sample_id = inputText.Substring(strIndex, ConstantData.SAMPLE_ID_SIZE);
            sample_id = sample_id.Trim();
            strIndex += ConstantData.SAMPLE_ID_SIZE;
            return new Tuple<string, string, string, string>(system_no, sample_type, sample_no, sample_id);

        }
        private Analysis SampleInformationResponse(string sampleId, string sampleNo, string sampleType)
        {
             //The sample is being processed manually or in a Lab Machine.
            var analysis = new Analysis();
           
            //Query data for sample_id and populate these fields.
            try
            {
                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.ConnectionString = _MySqlConnection;
                    
                    //For this sample id, get all the test codes associated with it.
                    MySqlCommand cmdSelect = new MySqlCommand(ConstantData.SampleSelectQuery, conn);
                    // Add the parameters.
                    cmdSelect.Parameters.Add(new MySqlParameter("sample_id", sampleId));
                    conn.Open();
                    using (MySqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        String test_code, diluent_type;
                        analysis.OnlineTestInfo = new List<string[]>();
                        while (reader.Read())
                        {
                            analysis.Psex = reader["gender"].ToString();
                            analysis.Pageyear = reader["calculated_age"].ToString();
                            analysis.Pagemonth = "0";
                            analysis.Pinformation = reader["mrd_number"].ToString() + " - " + reader["first_name"].ToString() + " " + reader["last_name"].ToString();

                            if (reader["sample_type_name"].ToString().Equals("Blood"))
                            {
                               analysis.Sample_type = " ";
                            }
                            else if (reader["sample_type_name"].ToString().Equals("Urine"))
                            {
                                analysis.Sample_type = "U";
                            }
                            test_code = reader["test_code"].ToString();
                            diluent_type = "0";
                            if (reader["sample_diluent_type"].ToString().Equals("Diluent"))
                            {
                                diluent_type = "1";
                            }
                            else if (reader["sample_diluent_type"].ToString().Equals("Condense"))
                            {
                                diluent_type = "2";
                            }
                            analysis.OnlineTestInfo.Add(new string[] { test_code, diluent_type });
                        }
                        conn.Close();
                    }

                    //Now update the samples' statuses.                  
                    
                    string query = string.Format(ConstantData.SampleUpdateQuery, ConstantData.DB_SAMPLE_STATUS_IN_PROCESS, sampleId);
                    int updCnt = QueryExecutor.ExecuteNonQuery(query, _MySqlConnection);

                    LogEvent("Successfully updated " + updCnt + " sample record status.");
                    logger.Info("Successfully updated " + updCnt + " sample record status.");
                }
            }
            catch (Exception e)
            {
                LogException(e);
            }
            finally
            {

            }

            return analysis;
        }       
        private void LogEvent(string message)
        {
            EventLog.Source = _Source;
            EventLog.Log = _Log;
            EventLog.WriteEntry($"{DateTime.Now}{message}");
        }
        private void LogException(Exception ex)
        {
            EventLog.Source = _Source;
            EventLog.Log = _Log;
            EventLog.WriteEntry($"{DateTime.Now}{ex.ToString()}");
        }
       
    }
}
