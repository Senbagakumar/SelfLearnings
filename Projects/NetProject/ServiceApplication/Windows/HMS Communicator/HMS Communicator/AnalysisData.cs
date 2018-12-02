using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HMS_Communicator.AnalysisData;

namespace HMS_Communicator
{

    public class Analysis
    {
        public string InputText { get; private set; }
        public string System_no { get; set; }
        public string Sample_type { get; set; }
        public string Sample_no { get; set; }
        public string Sample_id { get; set; }

        public string dummy4 = "    ";

        public string DataClassificationNo;
        public string Psex { get; set; }
        public string Pageyear { get; set; }
        public string Pagemonth { get; set; }
        public string Pinformation { get; set; }
        public List<AnalysisDataFormat> OnlineTestResult { get; set; }
        public List<String[]> OnlineTestInfo { get; set; }
    }
    public class ConstantData
    {
        //Constants
        public const bool USE_SYSTEM_NUMBER = false;
        public const bool USE_SAMPLE_TYPE = true;
        public const byte SAMPLE_NO_SIZE = 4;
        public const byte SAMPLE_ID_SIZE = 26;
        public const byte ONLINE_TEST_NO_SIZE = 3;
        public const byte ANALYSIS_DATA_SIZE = 6;
        public const byte ANALYSIS_DATA_FLAG_SIZE = 2;
        public const string Dummy4 = "    ";

        public const string RESPONSE_START_CODE = "S ";
        public const string RESPONSE_END_CODE = "SE";

        public const int InProcess = 2;
        public const int Processed = 3;
        public const int Failed = 7;

        public static string STARTTEXT = ((char)2).ToString(); //Start Message constant
        public static string ENDTEXT = ((char)3).ToString();   //End Message constant

        //DB Constants.
        //Sample Statuses
        public const byte DB_SAMPLE_STATUS_INITIAL = 1; //The default status. The sample has just been added.
        public const byte DB_SAMPLE_STATUS_IN_PROCESS = 2; //The sample is being processed manually or in a Lab Machine.
        public const byte DB_SAMPLE_STATUS_PROCESSED = 3; //Sample has been successfully processed.
        public const byte DB_SAMPLE_STATUS_UNUSABLE = 4; //The sample is unusable.
        public const byte DB_SAMPLE_STATUS_FAILED = 5; //Sample processing was unsuccessful.
        //Test Result Statuses
        public const byte DB_SAMPLE_TR_INITIAL = 1; //No action has been taken yet.
        public const byte DB_SAMPLE_TR_SAMPLE_ACQUIRED = 2; //The sample was received from the patient.
        public const byte DB_SAMPLE_TR_COMPLETED = 3; //The Test was completed successfully. Must set the COMPLETED_DATE field as well.
        public const byte DB_SAMPLE_TR_ABORTED = 4; //The test was aborted for some reason.
        public const byte DB_SAMPLE_TR_SAMPLE_PROCESSED = 5; //Samples have been processed

        public const string SampleSelectQuery = "SELECT vsstc.sample_id, vsstc.test_code, vsstc.sample_type_name, vsstc.sample_diluent_type, vsstc.test_request_id, vsstc.patient_id, vsstc.first_name, vsstc.last_name, vsstc.mrd_number, vsstc.gender, vsstc.calculated_age FROM v_sample_sample_test_codes vsstc WHERE vsstc.sample_id = @sample_id AND vsstc.deleted = 0 AND vsstc.enabled = 1 ORDER BY vsstc.display_order, vsstc.sample_test_code_id;";
        public const string SampleUpdateQuery = "UPDATE samples SET samples.status = {0} WHERE sample_id = {1}";
        public const string SampleGetQuery = "SELECT vsstc.sample_id, vsstc.test_code, vsstc.test_request_id, tf.test_id, vsstc.patient_id, tf.test_field_id, tft.name AS test_field_type_name FROM v_sample_sample_test_codes vsstc INNER JOIN test_fields tf ON vsstc.test_id = tf.test_id AND vsstc.sample_test_code_id = tf.sample_test_code_id INNER JOIN test_field_types tft ON tft.test_field_type_id = tf.test_field_type_id WHERE vsstc.sample_id = @sample_id ORDER BY tf.display_order, tf.test_field_id ";
        //public const string InsertQuery = "INSERT INTO test_result_values (test_request_id, test_id, patient_id, test_field_id, value, created_by, created_date) values ( {0}, {1} , {2} , {3} , {4} , {5} , {6} ) ON DUPLICATE KEY UPDATE value = VALUES(value);";
        public const string SampleUpdateQuery1 = "UPDATE samples SET samples.status = {0} ,processed_date = CURRENT_TIMESTAMP Where Sample_id={1};";
        public const string SampleUpdateTestQuery = "UPDATE test_requests SET test_requests.status = {0} Where test_request_id = {1};";
    }
    public class AnalysisData
    {
        public Analysis Parse(string receivedText, bool useSystemNo, bool useSampleType)
        {
            Analysis analysis = new Analysis();

            string InputText = receivedText;
            int strIndex = 0;
            strIndex++; //Ignore first char, i.e., STX.
                        //Check if this is the correct input.
            if (!InputText.Substring(strIndex, 2).Equals("D "))
            {
                return null;
            }

            strIndex += 2;
            if (useSystemNo)
            {
                analysis.System_no = InputText.Substring(strIndex, 2);
                analysis.System_no = analysis.System_no.Trim();
                strIndex += 2;
            }

            if (useSampleType)
            {
                analysis.Sample_type = InputText.Substring(strIndex, 1);
                strIndex += 1;
            }

            analysis.Sample_no = InputText.Substring(strIndex, ConstantData.SAMPLE_NO_SIZE);
            analysis.Sample_no = analysis.Sample_no.Trim();
            strIndex += ConstantData.SAMPLE_NO_SIZE;

            analysis.Sample_id = InputText.Substring(strIndex, ConstantData.SAMPLE_ID_SIZE);
            analysis.Sample_id = analysis.Sample_id.Trim();
            strIndex += ConstantData.SAMPLE_ID_SIZE;

            //Skip the dummy space.
            strIndex += 4;

            //TODO::Based on the below field, combine multiple records sent for the same sample.
            analysis.DataClassificationNo = InputText.Substring(strIndex, 1);
            strIndex++;

            //Extract the Patient Information.
            analysis.Psex = InputText.Substring(strIndex, 1);
            strIndex++;
            analysis.Pageyear = InputText.Substring(strIndex, 3);
            strIndex += 3;
            analysis.Pagemonth = InputText.Substring(strIndex, 2);
            strIndex += 2;
            analysis.Pinformation = InputText.Substring(strIndex, 20);
            strIndex += 20;

            //Analysis data begins here and ends with ETX character.
            int idxMsgEnd = InputText.IndexOf(ConstantData.ENDTEXT);
            if (idxMsgEnd < 0 || idxMsgEnd <= strIndex)
            {
                //TODO:: Throw an exception and exit.
            }
            int AnalysisRecordSize = ConstantData.ONLINE_TEST_NO_SIZE + ConstantData.ANALYSIS_DATA_SIZE + ConstantData.ANALYSIS_DATA_FLAG_SIZE;
            String AnalysisRecordText = "";
            AnalysisDataFormat adf;
            analysis.OnlineTestResult = new List<AnalysisDataFormat>();
            while (strIndex < idxMsgEnd)
            {
                AnalysisRecordText = InputText.Substring(strIndex, AnalysisRecordSize);
                adf = new AnalysisDataFormat(AnalysisRecordText);
                analysis.OnlineTestResult.Add(adf);
                strIndex += AnalysisRecordSize;
            }
            return analysis;
        }
        /// <summary>
        /// Saves the Analysis Data to the database.
        /// </summary>
        /// <param name="ad"></param>
        public void SaveSampleTestData(Analysis ad, string connectionString, NLog.Logger logger)
        {
          
            //Connect to the DB, query test_request_id, test_field_value_id.
            try
            {

                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.ConnectionString = connectionString;                    
                    //For this sample's test request, get the test fields that have a test code associated with it.
                    MySqlCommand mySqlCommand = new MySqlCommand(ConstantData.SampleGetQuery, conn);
                    // Add the parameters.
                    mySqlCommand.Parameters.Add(new MySqlParameter("sample_id", ad.Sample_id));
                    StringBuilder sb = new StringBuilder();
                    Boolean hasTestvalues = false;
                    string test_request_id = "", test_id, patient_id;
                    conn.Open();
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        //Now, either insert or update the values fetched from the lab machine.
                        string createdBy = "1";
                        string createdDate = "CURRENT_TIMESTAMP";
                        bool isFirstRow = true;
                        sb.Append("INSERT INTO test_result_values (test_request_id, test_id, patient_id, test_field_id, value, created_by, created_date) ");
                        sb.Append(" VALUES ");

                        while (reader.Read())
                        {
                            if (!isFirstRow)
                            {
                                sb.Append(",");
                            }
                            isFirstRow = false;
                            sb.Append("(");
                            int readerFieldCount = reader.FieldCount;
                            int adTestResultIdx = -1;
                            string TestDataValue = "";
                            //Check if the current test field's test code matches the one from the machine.
                            for (int resIdx = 0; resIdx < ad.OnlineTestResult.Count; resIdx++)
                            {
                                //Loop through the received test codes and find the matching test code 
                                if (ad.OnlineTestResult[resIdx].OnlineTestNo.Equals(reader["test_code"].ToString()))
                                {
                                    adTestResultIdx = resIdx;
                                    hasTestvalues = true;
                                    break;
                                }
                            }
                            //If no test field has a matching test code, then skip this test code.
                            if (adTestResultIdx == -1)
                            {
                                continue;
                            }

                            //Store the Test Request ID to use in subsequent SQL queries.
                            test_request_id = reader["test_request_id"].ToString();
                            test_id = reader["test_id"].ToString();
                            patient_id = reader["patient_id"].ToString();
                            //Get the test data value and apply transformations to it, if required.
                            TestDataValue = ad.OnlineTestResult[adTestResultIdx].TestData;
                            TestDataValue = EncodeTestResultValue(reader["test_field_type_name"].ToString(), TestDataValue);
                            //Otherwise, create the insert/update query.

                            sb.Append(test_request_id);
                            sb.Append("," + test_id);
                            sb.Append("," + patient_id);
                            sb.Append("," + Convert.ToString(reader["test_field_id"].ToString()));
                            //Now, add the value, created_by and created_date fields.
                            sb.Append("," + FormatDBString(TestDataValue));
                            sb.Append("," + createdBy);
                            sb.Append("," + createdDate);

                            sb.Append(")");
                        }
                        sb.Append(" ON DUPLICATE KEY UPDATE value = VALUES(value); ");
                    }
                    conn.Close();

                    if (hasTestvalues)
                    {
                        int updCnt, insCnt;

                        //Insert or update the test result value records.
                        logger.Debug(sb.ToString());
                        insCnt = QueryExecutor.ExecuteNonQuery(sb.ToString(), connectionString);                        
                        logger.Info("Successfully inserted/updated " + insCnt + " test_result_values records.");

                        //Now update the samples' statuses.
                       
                        string updateQuery = string.Format(ConstantData.SampleUpdateQuery1, ConstantData.DB_SAMPLE_STATUS_PROCESSED,ad.Sample_id);
                        logger.Debug(updateQuery);
                        updCnt = QueryExecutor.ExecuteNonQuery(updateQuery, connectionString);
                        
                        logger.Info("Successfully updated " + updCnt + " sample record status.");

                       
                        updateQuery = string.Format(ConstantData.SampleUpdateTestQuery, ConstantData.DB_SAMPLE_TR_SAMPLE_PROCESSED, test_request_id);
                        logger.Debug(updateQuery);
                        updCnt = QueryExecutor.ExecuteNonQuery(updateQuery, connectionString);
                       
                        logger.Info("Successfully updated " + updCnt + " test_request record status.");
                    }
                    else
                    {
                        logger.Warn("No Online Test Codes matched.");
                    }                    
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }

        /// <summary>
        /// Test Field Value can be a number, text or any of the pre-defined strings.
        /// CURRENTLY NOT USED. All values are stored as received.
        /// </summary>
        /// <param name="TestFieldType"></param>
        /// <param name="InputValue"></param>
        /// <returns></returns>
        private string EncodeTestResultValue(string TestFieldType, string InputValue)
        {
            Dictionary<string, string> dictListItem = new Dictionary<string, string>();
            Dictionary<string, string> dictRating = new Dictionary<string, string>();

            //TODO:: Check how to map inputvalue to, say, "+++".
            //Does the reported value correspond one-to-one with the dictionary value?
            dictListItem.Add("1", "Positive");
            dictListItem.Add("2", "Negative");
            dictListItem.Add("3", "Trace");
            dictListItem.Add("4", "Nil");
            dictListItem.Add("5", "Normal");

            dictRating.Add("0", "Nil");
            dictRating.Add("1", "+");
            dictRating.Add("2", "++");
            dictRating.Add("3", "+++");
            dictRating.Add("4", "++++");
            dictRating.Add("5", "+++++");

            string cleanInputValue;
            if (TestFieldType.Equals("SelectText"))
            {
                //Remove any decimal points within the string.
                cleanInputValue = Convert.ToString(Convert.ToInt64(Convert.ToDouble(InputValue)));
                return dictListItem[cleanInputValue];
            }
            else if (TestFieldType.Equals("Rating"))
            {
                cleanInputValue = Convert.ToString(Convert.ToInt64(Convert.ToDouble(InputValue)));
                return dictRating[cleanInputValue];
            }
            return InputValue;
        }

        /// <summary>
        /// Formats a String for use in DB Insert queries.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string FormatDBString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "NULL";
            }
            else
            {
                return "'" + value + "'";
            }
        }
        public class AnalysisDataFormat
        {
            public string InputText { get; private set; }
            private string _OnlineTestNo;
            public string OnlineTestNo
            {
                get
                {
                    //Trim leading zeroes from online test no.
                    return _OnlineTestNo.TrimStart(new char[] { '0', ' ' });
                }
                set => _OnlineTestNo = value;
            }
            private string _TestData;
            public string TestData
            {
                get
                {
                    //Remove zero suppression and space padding.
                    //Possible valid values are: "- 123.2", "-0123.2"
                    //TODO::For NIH test and custom tests, we may need to parse the TestData into multiple values.
                    return _TestData.Replace(" ", "");
                }
                set => _TestData = value;
            }
            public string DataFlag { get; set; }
            public AnalysisDataFormat(string receivedText)
            {
                this.Parse(receivedText);
            }

            public void Parse(string receivedText)
            {
                InputText = receivedText;
                int strIndex = 0;

                OnlineTestNo = InputText.Substring(strIndex, ConstantData.ONLINE_TEST_NO_SIZE);
                strIndex += ConstantData.ONLINE_TEST_NO_SIZE;

                TestData = InputText.Substring(strIndex, ConstantData.ANALYSIS_DATA_SIZE);
                strIndex += ConstantData.ANALYSIS_DATA_SIZE;

                DataFlag = InputText.Substring(strIndex, ConstantData.ANALYSIS_DATA_FLAG_SIZE);
                strIndex += ConstantData.ANALYSIS_DATA_FLAG_SIZE;
            }
        }
    }

    public class QueryExecutor
    {
        public static int ExecuteNonQuery(string query, string mySqlConnection)
        {
            MySqlConnection sqlConnection = new MySqlConnection(mySqlConnection);
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, sqlConnection);
            sqlConnection.Open();
            //Execute command
            int updateCount=cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return updateCount;
        }
    }

}
        
   


