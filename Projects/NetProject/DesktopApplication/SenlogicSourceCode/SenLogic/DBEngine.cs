using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data.OleDb;
namespace SenLogic
{

    public class DBEngine : IDisposable
    {
        private Hashtable htAdapters = new Hashtable();
        private Hashtable htDataSets = new Hashtable();

        
        bool blnTransactionRequired = true;
        string connecStr;

        public string ConnectionString
        {
            get 
            {
                return Shared_Variables.GetConnectionString = System.Configuration.ConfigurationSettings.AppSettings.Get("ConnectionString");      
           
            }
           
        }

        public DBEngine()
        {
           // this.isolationLevel = IsolationLevels.ReadUnCommitted;
        }

      

        

        //******************************************************************************
        // Name: CommandType
        // Description: This enumeration provides various command types available in SQL
        //
        // Parameters:
        //
        // Returns:
        //
        //******************************************************************************
        public enum CommandType
        {
            DeleteCommand,
            InsertCommand,
            SelectCommand,
            UpdateCommand
        }

        public IDbDataAdapter ExecuteQuery(string sqlQuery, Hashtable htParameters, DataSet objDS, string tablename)
        {

            IDbDataAdapter dbAdapter = null;
            IDbCommand dbCommand = null;

            //create command
            dbCommand = CreateCommand(sqlQuery);
            //create adapter
            dbAdapter = CreateAdapter(dbCommand, CommandType.SelectCommand);

            //setup parameters
           // GenerateParameters(dbCommand, htParameters);

            try
            {
                //run query
                ((OleDbDataAdapter)dbAdapter).Fill(objDS, tablename);

                return dbAdapter;
            }
            catch (Exception genEx)
            {
                Shared_Functions.Logged_ErrorMessage(genEx.Message);
                throw genEx;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
        }

        public string ExecuteQuery(string sqlQuery)
        {
            string value = string.Empty;
            IDbCommand dbCommand = null;

            //create command
            try
            {
                dbCommand = CreateCommand(sqlQuery);
                OleDbDataReader odr = (OleDbDataReader)dbCommand.ExecuteReader();
                while (odr.Read())
                {
                    value = odr[0].ToString();
                }
                return value;
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                //dbCommand.Connection.Close();
                //dbCommand.Connection.Dispose();
            }
            finally
            {
                //close con
                dbCommand.Connection.Close();
                dbCommand.Connection.Dispose();
            }
            return string.Empty;

        }
        public IDbConnection CreateConnection()
        {
           // SqlConnection dbConn = new SqlConnection();
            var dbConn = new OleDbConnection();

            
            dbConn.ConnectionString = ConnectionString;

            try
            {
                dbConn.Open();              
                return dbConn;
            }
            catch (Exception ex)
            {
                Shared_Functions.Logged_ErrorMessage(ex.Message);
                dbConn.Close();
                dbConn.Dispose();                
                throw ex;
            }
        }

        //******************************************************************************
        // Name: CreateCommand
        // Description: Create a command object based on parameters
        //
        // Parameters:
        //
        // Returns:
        //
        //******************************************************************************

        public IDbCommand CreateCommand(string sqlQuery)
        {
            //SqlCommand dbCommand = new SqlCommand();
            var dbCommand = new OleDbCommand();

           // dbCommand.Connection = (SqlConnection)CreateConnection();
            dbCommand.Connection = (OleDbConnection)CreateConnection();
            if (!string.IsNullOrEmpty(sqlQuery))
            {
                dbCommand.CommandText = sqlQuery;
            }
            return dbCommand;
        }

        private IDbCommand CreateCommand(string sqlQuery, IDbConnection dbconn)
        {
            SqlCommand dbCommand = new SqlCommand();
            dbCommand.Connection = (SqlConnection)dbconn;

            if (!string.IsNullOrEmpty(sqlQuery))
            {
                dbCommand.CommandText = sqlQuery;
            }

            return dbCommand;
        }

        //******************************************************************************
        // Name: CreateAdapter
        // Description: Create an adapter object based on parameters
        //
        // Parameters:
        //
        // Returns:
        //
        //******************************************************************************

        private OleDbDataAdapter CreateAdapter(IDbCommand dbCommand, CommandType cmdType)
        {
            var dbAdapter = new OleDbDataAdapter();

            switch (cmdType)
            {
                case CommandType.DeleteCommand:
                    dbAdapter.DeleteCommand = (OleDbCommand)dbCommand;
                    break;
                case CommandType.InsertCommand:
                    dbAdapter.InsertCommand = (OleDbCommand)dbCommand;
                    break;
                case CommandType.SelectCommand:
                    dbAdapter.SelectCommand = (OleDbCommand)dbCommand;
                    break;
                case CommandType.UpdateCommand:
                    dbAdapter.UpdateCommand = (OleDbCommand)dbCommand;
                    break;
            }

            return dbAdapter;
        }

        //******************************************************************************
        // Name: CreateAdapter
        // Description: Create an adapter object based on parameters
        //
        // Parameters:
        //
        // Returns:
        //
        //******************************************************************************
        private IDbDataAdapter CreateAdapter(string sqlQuery)
        {
            IDbCommand dbCommand = default(IDbCommand);
            dbCommand = CreateCommand(sqlQuery);
            return CreateAdapter(dbCommand, CommandType.SelectCommand);
        }

        private IDbDataAdapter CreateAdapter(string sqlQuery, CommandType cmdType)
        {
            IDbCommand dbCommand = default(IDbCommand);
            dbCommand = CreateCommand(sqlQuery);
            return CreateAdapter(dbCommand, cmdType);
        }


        private void GenerateParameters(IDbCommand dbCommand, Hashtable htParameters)
        {
            SqlParameter parameter = default(SqlParameter);
            Regex re = default(Regex);
            Match regMatch = default(Match);
            string sqlText = null;
            string expText = null;
            string attribute = null;
            string ioType = null;

            bool isUpdateStmt = false;
            int indexOfWhere = 0;

            dbCommand.CommandTimeout = 3600;

            //
            //check parameters
            // Validator.AssertNotNull(Me, dbCommand)

            try
            {

                //regular exressions are used to parse the sql string and get the tokens in each parameter
                //following regular expression will extract all values between { and } into attrib
                //
                expText = "{(?<attrib>[^{]*)}";
                sqlText = dbCommand.CommandText;

                //if the sql command is UPDATE, DELETE , all parameters after the WHERE clause should
                // have sourceversion property as original. In the case of an INSERT statement
                // all parameters will have sourceversion property as original.
                if (sqlText.IndexOf("UPDATE") > -1 | sqlText.IndexOf("DELETE") > -1)
                {
                    isUpdateStmt = true;
                    indexOfWhere = sqlText.IndexOf("WHERE");
                }
                else if (sqlText.IndexOf("INSERT") > -1)
                {
                    isUpdateStmt = true;
                    indexOfWhere = 0;
                }
                else
                {
                    isUpdateStmt = false;
                }

                //for each parameter value in the hashtable, create a parameter object for associated
                //tokens in the sql string
                //
                if ((htParameters != null))
                {
                    foreach (string item in htParameters.Keys)
                    {

                        //check if parameter is in the format of '@paramname.columnname
                        //set attributes of the sqlparameter object appropriately
                        //
                        parameter = new SqlParameter();
                        re = new Regex("(?<paramname>\\w+)\\.?(?<columnname>\\w+)?");
                        regMatch = re.Match(item);

                        if (regMatch.Success)
                        {
                            parameter.ParameterName =  regMatch.Result("${paramname}");
                            if (string.IsNullOrEmpty(regMatch.Result("${columnname}")))
                            {
                                parameter.SourceColumn = regMatch.Result("${paramname}");
                            }
                            else
                            {
                                parameter.SourceColumn = regMatch.Result("${columnname}");
                            }
                        }
                        //set the value
                        if ((htParameters[item] != null))
                        {
                            parameter.Value = htParameters[item];
                        }

                        //find the attribute { } associated witht he parameter token
                        re = new Regex(item + expText, RegexOptions.IgnoreCase);
                        regMatch = re.Match(sqlText);

                        if (regMatch.Success)
                        {
                            //if found after WHERE, update sourceversion property
                            if (isUpdateStmt & regMatch.Index > indexOfWhere)
                            {
                                parameter.SourceVersion = DataRowVersion.Original;
                            }

                            //get attribute within { and }
                            attribute = regMatch.Result("${attrib}");
                            //extract i/o/io/t
                            re = new Regex("(\\bi(o)?\\b)|(\\bo\\b)|(\\br\\b)|(\\bt\\b)");
                            regMatch = re.Match(attribute);

                            if (regMatch.Success)
                            {
                                ioType = regMatch.Value;
                                if (ioType == "i")
                                {
                                    parameter.Direction = ParameterDirection.Input;
                                }
                                else if (ioType == "o")
                                {
                                    parameter.Direction = ParameterDirection.Output;
                                }
                                else if (ioType == "io")
                                {
                                    parameter.Direction = ParameterDirection.InputOutput;
                                }
                                else if (ioType == "r")
                                {
                                    parameter.Direction = ParameterDirection.ReturnValue;
                                }
                                else if (ioType == "t")
                                {
                                    parameter.Value = null;
                                }
                            }

                            //get size attribute for parameter
                            re = new Regex("\\d+\\b");
                            regMatch = re.Match(attribute);
                            if (regMatch.Success)
                            {
                                parameter.Size = int.Parse(regMatch.Value);
                            }
                        }
                        dbCommand.Parameters.Add(parameter);
                    }

                    //change the sql string to appropriate format by removing all custom tokens.
                    expText = "{[^{]*}";
                    re = new Regex(expText, RegexOptions.IgnoreCase);
                    sqlText = re.Replace(sqlText, "");

                    if (dbCommand.CommandType == System.Data.CommandType.StoredProcedure)
                    {
                        re = new Regex("(?<commandName>[^\\s]*)");
                        dbCommand.CommandText = re.Match(dbCommand.CommandText).Result("${commandName}");
                    }
                    else
                    {
                        re = new Regex("(?<paramname>\\w+)\\.\\w+");
                        dbCommand.CommandText = re.Replace(sqlText, "$1");
                    }
                }
            }
            catch (Exception genEx)
            {
                Shared_Functions.Logged_ErrorMessage(genEx.Message); 
            }

        }

        public object ExecuteScalar(string sqlQuery, Hashtable htParameters)
        {
            IDbCommand dbCommand = default(IDbCommand);
            object result = null;

            //create command
            dbCommand = CreateCommand(sqlQuery);
            dbCommand.Connection = CreateConnection();          
            

            try
            {
                //run
                result = dbCommand.ExecuteScalar();
                return result;
            }
            catch (Exception genEx)
            {
                Shared_Functions.Logged_ErrorMessage(genEx.Message);
                //dbCommand.Connection.Close();
                // throw (genEx);
            }
            finally
            {
                //close con
                dbCommand.Connection.Close();
                dbCommand.Connection.Dispose();
            }
            return result;
        }

        public object ExecuteNonQuery(string sqlQuery, Hashtable htParameters)
        {
            IDbCommand dbCommand = default(IDbCommand);
            object result = null;

            //create command
            dbCommand = CreateCommand(sqlQuery);

            //create parameters for the query
           // GenerateParameters(dbCommand, htParameters);

            try
            {
                //run
                result = dbCommand.ExecuteNonQuery();
                return result;
            }
            catch (Exception genEx)
            {
                Shared_Functions.Logged_ErrorMessage(genEx.Message);
                //  throw (genEx);
            }
            finally
            {
                //close con
           
                //close con
                dbCommand.Connection.Close();
                dbCommand.Connection.Dispose();
            
            }
            return result;
        }


        public void ExecuteNonQuery(string sqlQuery)
        {
            IDbCommand dbCommand = default(IDbCommand);                      
            dbCommand = CreateCommand(sqlQuery);           
            try
            {               
                dbCommand.ExecuteNonQuery();                
            }
            catch (Exception genEx)
            {
                Shared_Functions.Logged_ErrorMessage(genEx.Message);
                throw (genEx);
            }
            finally
            {
                //close con
                dbCommand.Connection.Close();
                dbCommand.Connection.Dispose();
            }
        }



        public void Dispose()
        {
            
        }
    }



}
