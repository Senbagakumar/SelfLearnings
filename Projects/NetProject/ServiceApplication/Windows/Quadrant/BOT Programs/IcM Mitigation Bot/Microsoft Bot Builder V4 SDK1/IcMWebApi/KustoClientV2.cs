//using Kusto.Cloud.Platform.Data;
//using Kusto.Data;
//using Kusto.Data.Common;
//using Kusto.Data.Exceptions;
//using Kusto.Data.Net.Client;
using Kusto.Cloud.Platform.Data;
using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Exceptions;
using Kusto.Data.Net.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace Microsoft.SqlServer.Tools.SterlingKustoClientV2
{
    public class KustoConnectionStringInfo
    {
        public string DataSource { get; set; }

        public string InitialCatalog { get; set; }
    }
    public interface IKustoUtilProvider
    {
        DataTable QueryTable(string connectionString, string query, bool adminRequired);

        DataSet QueryTableAndReturnResultWithStatus(string connectionString, string query, bool adminRequired);

        DataSet QueryTableAndReturnResultWithStatus(string connectionString, string query, bool adminRequired, int retryIntervalInSeconds, int numberOfRetries, int retryBackoffMultiplier);

        string RewriteConnectionString(string connectionString, string userName, string password, string applicationClientId, string applicationKey, string dataAccessTracer, string applicationCertificateThumbprint);

        string GetKustoConnectionString(string dataSource, string initialCatalog);

        KustoConnectionStringInfo GetKustoConnectionStringInfo(string connectionString);
    }
    public interface IKustoQueryLogic
    {
        string StripQueryNamespace(string query);

        string CreateFilteredQuery(string queryString, string baseTable, string[] clusterIds, AzureSQLClusterProfile clusterInfo);

        string GetSterlingKustoTable(string baseTable);

        string ModifyTableName(string tableName);

        string ModifyQueryDateTimeFilter(string tableName, string queryString, KustoRequest request);

        void ModifyDataTableValuesForDateTimeColumn(DataTable dataTable);
    }
    public class KustoQueryLogic : IKustoQueryLogic
    {
        protected static readonly string[] KustoNamespaceTags = new string[3]
        {
      "{KustoNamespace}",
      "%KustoNamespace%",
      "WASD2Prod"
        };
        protected static readonly string[] KustoMonikerTags = new string[2]
        {
      "{KustoMoniker}",
      "%KustoMoniker%"
        };
        private static readonly Lazy<KustoQueryLogic> _default = new Lazy<KustoQueryLogic>((Func<KustoQueryLogic>)(() => new KustoQueryLogic()));

        public static KustoQueryLogic Default
        {
            get
            {
                return KustoQueryLogic._default.Value;
            }
        }

        public string StripQueryNamespace(string query)
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            Regex regex1 = new Regex("(WASD2(Prod|Mcpr|Stage|Bfpr|Ffpr|Test|Orcasql))", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex("{KustoNamespace}|%KustoNamespace%", RegexOptions.IgnoreCase);
            string b = (string)null;
            foreach (char ch in query)
            {
                switch (ch)
                {
                    case '"':
                    case '\'':
                        if (b == null)
                        {
                            string input1 = stringBuilder1.ToString();
                            stringBuilder1.Clear();
                            string input2 = regex1.Replace(input1, string.Empty);
                            string str = regex2.Replace(input2, string.Empty);
                            stringBuilder2.Append(str);
                            b = ch.ToString();
                            break;
                        }
                        if (string.Equals(ch.ToString(), b))
                        {
                            stringBuilder2.Append(ch.ToString() + stringBuilder1.ToString() + ch.ToString());
                            stringBuilder1.Clear();
                            b = (string)null;
                            break;
                        }
                        stringBuilder1.Append(ch);
                        break;
                    default:
                        stringBuilder1.Append(ch);
                        break;
                }
            }
            string input3 = stringBuilder1.ToString();
            string input4 = regex1.Replace(input3, string.Empty);
            string str1 = regex2.Replace(input4, string.Empty);
            stringBuilder2.Append(str1);
            return stringBuilder2.ToString();
        }

        public string CreateFilteredQuery(string queryString, string baseTable, string[] clusterIds, AzureSQLClusterProfile clusterInfo)
        {
            string input = queryString;
            foreach (string kustoNamespaceTag in KustoQueryLogic.KustoNamespaceTags)
                input = Regex.Replace(input, kustoNamespaceTag, clusterInfo.MdsNamespace, RegexOptions.IgnoreCase);
            string replacement = "";
            foreach (string kustoMonikerTag in KustoQueryLogic.KustoMonikerTags)
                input = Regex.Replace(input, kustoMonikerTag, replacement, RegexOptions.IgnoreCase);
            string str;
            if (clusterIds == null || clusterIds.Length == 0 || string.IsNullOrEmpty(baseTable))
            {
                str = input;
            }
            else
            {
                string empty = string.Empty;
                foreach (string clusterId in clusterIds)
                {
                    AzureSQLFabricCluster fabricCluster = this.GetFabricCluster(clusterId, clusterInfo);
                    if (fabricCluster == null)
                        throw new ArgumentException("unable to find cluster address of cluster id " + clusterId, nameof(clusterIds));
                    empty += string.Format("  ClusterName ==\"{0}\" or", (object)fabricCluster.ClusterAddress);
                }
                str = string.Format(" where {0} | {1}", (object)empty.Substring(0, empty.Length - 2), (object)input).Replace("\\\\", "\\").Replace("\\", "\\\\");
            }
            return str;
        }

        public string GetSterlingKustoTable(string baseTable)
        {
            if (!string.IsNullOrEmpty(baseTable))
                return baseTable;
            return string.Empty;
        }

        public string ModifyTableName(string tableName)
        {
            string str = tableName.Trim();
            if (str.Any<char>(new Func<char, bool>(char.IsWhiteSpace)) || str.Contains("|") || tableName.Length == 0)
                return tableName;
            string pattern1 = "{KustoNamespace}|%KustoNamespace%";
            string pattern2 = "WASD2(Prod|Mcpr|Stage|Bfpr|Ffpr|Test|Orcasql)";
            if (!Regex.IsMatch(str, pattern1, RegexOptions.IgnoreCase) && !Regex.IsMatch(str, pattern2, RegexOptions.IgnoreCase))
                return tableName;
            return this.StripQueryNamespace(tableName);
        }

        public string ModifyQueryDateTimeFilter(string tableName, string queryString, KustoRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(tableName))
            {
                stringBuilder.AppendFormat("{0} | ", (object)tableName);
                if (!string.IsNullOrEmpty(request.DateTimeColumn))
                {
                    if (request.StartTime.HasValue)
                        stringBuilder.AppendFormat("where {1} > datetime({0}) |", (object)request.StartTime.Value.ToString("O"), (object)request.DateTimeColumn);
                    if (request.EndTime.HasValue)
                        stringBuilder.AppendFormat("where {1}  <= datetime({0}) |", (object)request.EndTime.Value.ToString("O"), (object)request.DateTimeColumn);
                }
            }
            stringBuilder.Append(queryString);
            return stringBuilder.ToString();
        }

        public void ModifyDataTableValuesForDateTimeColumn(DataTable dataTable)
        {
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
            {
                if (column.DataType == Type.GetType("System.DateTime") && (column.DateTimeMode == DataSetDateTime.UnspecifiedLocal || column.DateTimeMode == DataSetDateTime.Local))
                    column.DateTimeMode = DataSetDateTime.Unspecified;
            }
        }

        private AzureSQLFabricCluster GetFabricCluster(string clusterId, AzureSQLClusterProfile clusterInfo)
        {
            foreach (AzureSQLFabricCluster fabricCluster in (IEnumerable<AzureSQLFabricCluster>)clusterInfo.FabricClusters)
            {
                if (fabricCluster.Id.Equals(clusterId, StringComparison.InvariantCultureIgnoreCase) || fabricCluster.ClusterAddress.Equals(clusterId, StringComparison.InvariantCultureIgnoreCase))
                    return fabricCluster;
            }
            return (AzureSQLFabricCluster)null;
        }
    }
    public class KustoRequest
    {
        private int _numberOfRetries = 3;
        private int _retryBackoffMultiplier = 2;
        private int _retryIntervalInSeconds = 2;
        private int _requestTimeout = 120000;
        private readonly IList<string> _kustoTableList = (IList<string>)new List<string>();
        private readonly IList<string> _queries;

        public IList<string> Queries
        {
            get
            {
                return this._queries;
            }
        }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int NumberOfRetries
        {
            get
            {
                return this._numberOfRetries;
            }
            set
            {
                this._numberOfRetries = value;
            }
        }

        public TimeSpan RequestTimeout
        {
            get
            {
                return TimeSpan.FromMilliseconds((double)this._requestTimeout);
            }
            set
            {
                this._requestTimeout = (int)value.TotalMilliseconds;
            }
        }

        public int RetryBackoffMultiplier
        {
            get
            {
                return this._retryBackoffMultiplier;
            }
            set
            {
                this._retryBackoffMultiplier = value;
            }
        }

        public int RetryIntervalInSeconds
        {
            get
            {
                return this._retryIntervalInSeconds;
            }
            set
            {
                this._retryIntervalInSeconds = value;
            }
        }

        public IList<string> KustoTableList
        {
            get
            {
                return this._kustoTableList;
            }
            private set
            {
                this._kustoTableList.Clear();
                foreach (string str in (IEnumerable<string>)value)
                    this._kustoTableList.Add(str);
            }
        }

        public string DateTimeColumn { get; private set; }

        public bool AdminRequired { get; private set; }

        public KustoRequest(IEnumerable<string> queries, IEnumerable<string> kustoTableList, string dateTimeColumn = null, DateTime? startTime = null, DateTime? endTime = null, bool adminRequired = false)
        {
            if (queries == null || !queries.Any<string>())
                throw new ArgumentNullException(nameof(queries));
            if (endTime.HasValue && endTime.Value.Kind != DateTimeKind.Utc)
                throw new ArgumentException("endTime must be UTC", nameof(endTime));
            if (startTime.HasValue && startTime.Value.Kind != DateTimeKind.Utc)
                throw new ArgumentException("startTime must be UTC", nameof(startTime));
            if (startTime.HasValue && (this.EndTime.HasValue && startTime.Value > endTime.Value))
                throw new ArgumentException("startTime might smaller or equals than endTime", nameof(startTime));
            this._queries = (IList<string>)new List<string>(queries);
            this.KustoTableList = kustoTableList == null ? (IList<string>)new List<string>() : (IList<string>)new List<string>(kustoTableList);
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.DateTimeColumn = dateTimeColumn;
            this.AdminRequired = adminRequired;
        }
    }
    public class AzureSQLFabricCluster
    {
        public AzureSQLFabricClusterType Type { get; set; }

        public string Id { get; set; }

        public string ClusterAddress { get; set; }
    }
    public enum AzureSQLFabricClusterType
    {
        Control,
        Tenant,
        Utility,
    }
    public class AzureSQLClusterProfile
    {
        public string ClusterName { get; set; }

        public string MdsNamespace { get; set; }

        public IList<AzureSQLFabricCluster> FabricClusters { get; set; }
    }

    public interface IKustoConnectionFactory
    {
        KustoConnection Create(string clusterName, string clusterShortName, string dataSource, string initialCatalog, string mdsNamespace);
    }
    public abstract class KustoConnectionConfigurationContextBase
    {
        protected const string DefaultConnectionString = "Data Source=dw-srvr-01.database.windows.net;Initial Catalog=Test3DB_1;User ID=Streamsreader;Password=$tream$READER";
        protected const string ResourcesFile = "Resources";
        protected const string ConfigurationFile = "KustoConnectionConfiguration.xml";

        public string SqlResourcesReadPath { get; private set; }

        public string SqlResourcesWritePath { get; private set; }

        public string ConfigFileReadPath { get; private set; }

        public string ConfigFileWritePath { get; private set; }

        public string SqlConnectionString { get; private set; }

        protected IKustoConnectionFactory KustoConnectionFactory { get; set; }

        protected KustoConnectionConfigurationContextBase(IKustoConnectionFactory kustoConnectionFactory, string sqlConnectionString, string configFile, string resourcesFile, string readDirectory, string writeDirectory = null)
        {
            string path1 = writeDirectory ?? readDirectory;
            this.KustoConnectionFactory = kustoConnectionFactory;
            this.SqlConnectionString = sqlConnectionString;
            this.ConfigFileReadPath = Path.Combine(readDirectory, configFile);
            this.ConfigFileWritePath = Path.Combine(path1, configFile);
            this.SqlResourcesReadPath = Path.Combine(readDirectory, resourcesFile);
            this.SqlResourcesWritePath = Path.Combine(path1, resourcesFile);
        }

        public virtual SqlConnection TrySqlConnect(int numTries, TimeSpan backoff)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlConnectionString);
            for (int index = 1; index <= numTries; ++index)
            {
                try
                {
                    sqlConnection.Open();
                    if (sqlConnection.State == ConnectionState.Open)
                        return sqlConnection;
                    return (SqlConnection)null;
                }
                catch (SqlException ex)
                {
                    Thread.Sleep(TimeSpan.FromTicks(backoff.Ticks * (long)index));
                }
            }
            return (SqlConnection)null;
        }

        public virtual List<KustoConnection> ReadConnectionsFromSql(SqlConnection connection)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            StringBuilder stringBuilder = new StringBuilder(string.Empty);
            try
            {
                str1 = Process.GetCurrentProcess().ProcessName;
                str2 = Environment.MachineName;
                foreach (IPAddress hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
                    stringBuilder.Append(',').Append(hostAddress.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("There was an error while getting client information: {0]", (object)ex);
            }
            Trace.TraceInformation("Process:{0} Machine:{1} Ip:{2}", (object)str1, (object)str2, (object)stringBuilder.ToString());
            SqlCommand sqlCommand = new SqlCommand("[dbo].[sp_get_kusto_cluster]", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ip_list", (object)stringBuilder.ToString());
            sqlCommand.Parameters.AddWithValue("@process_name", (object)str1);
            sqlCommand.Parameters.AddWithValue("@machine_name", (object)str2);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<KustoConnection> kustoConnectionList = new List<KustoConnection>();
            while (sqlDataReader.Read())
            {
                string clusterName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ClusterName"));
                string clusterShortName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ClusterShortName"));
                string dataSource = sqlDataReader.GetString(sqlDataReader.GetOrdinal("DataSource"));
                string initialCatalog = sqlDataReader.GetString(sqlDataReader.GetOrdinal("InitialCatalog"));
                string mdsNamespace = sqlDataReader.GetString(sqlDataReader.GetOrdinal("MdsNamespace"));
                kustoConnectionList.Add(this.KustoConnectionFactory.Create(clusterName, clusterShortName, dataSource, initialCatalog, mdsNamespace));
            }
            return kustoConnectionList;
        }

        public virtual string ReadXmlFromConfig()
        {
            return SafeFile.ReadFile(this.ConfigFileWritePath, 30, SterlingKustoConstants.ReadFileInterval);
        }

        public virtual List<KustoConnection> ReadConnectionsFromXml(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            List<KustoConnection> kustoConnectionList = new List<KustoConnection>();
            foreach (XmlNode selectNode in xmlDocument.SelectNodes("//Connection"))
            {
                string innerText1 = selectNode.SelectSingleNode("ClusterName").InnerText;
                string innerText2 = selectNode.SelectSingleNode("ClusterShortName").InnerText;
                string innerText3 = selectNode.SelectSingleNode("DataSource").InnerText;
                string innerText4 = selectNode.SelectSingleNode("InitialCatalog").InnerText;
                string innerText5 = selectNode.SelectSingleNode("MdsNamespace").InnerText;
                kustoConnectionList.Add(this.KustoConnectionFactory.Create(innerText1, innerText2, innerText3, innerText4, innerText5));
            }
            return kustoConnectionList;
        }

        public string ConvertConnectionListToXml(List<KustoConnection> connections)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ConnectionList>\n");
            foreach (KustoConnection connection in connections)
            {
                stringBuilder.Append("\t<Connection>\n");
                stringBuilder.AppendFormat("\t\t<ClusterName>{0}</ClusterName>\n", (object)connection.ClusterName);
                stringBuilder.AppendFormat("\t\t<ClusterShortName>{0}</ClusterShortName>\n", (object)connection.ClusterShortName);
                stringBuilder.AppendFormat("\t\t<DataSource>{0}</DataSource>\n", (object)connection.DataSource);
                stringBuilder.AppendFormat("\t\t<InitialCatalog>{0}</InitialCatalog>\n", (object)connection.InitialCatalog);
                stringBuilder.AppendFormat("\t\t<MdsNamespace>{0}</MdsNamespace>\n", (object)connection.MdsNamespace);
                stringBuilder.Append("\t</Connection>\n");
            }
            stringBuilder.Append("</ConnectionList>\n");
            return stringBuilder.ToString();
        }

        public virtual bool TryPersistConfig(List<KustoConnection> connections)
        {
            if (connections.Count > 0)
                return SafeFile.TryWriteFile(this.ConvertConnectionListToXml(connections), this.ConfigFileWritePath, 15, SterlingKustoConstants.WriteFileRetryInterval);
            Trace.TraceWarning("A zero length connection list was attemtped to be persisted");
            return false;
        }

        public virtual bool ConfigFileIsRecent()
        {
            return !SafeFile.FileOlderThan(SterlingKustoConstants.RecentTimespan, this.ConfigFileWritePath);
        }
    }
    public abstract class KustoConnectionLogicBase : IKustoConnectionLogic
    {
        protected List<KustoConnection> Cache = new List<KustoConnection>();
        private readonly IKustoUtilProvider _kustoUtilProvider;
        private System.Timers.Timer KustoRefreshTimer;
        private TimeSpan? _kustoRefreshTimer;

        public List<KustoConnection> KustoConnections
        {
            get
            {
                if (this.Cache == null)
                    return (List<KustoConnection>)null;
                return new List<KustoConnection>((IEnumerable<KustoConnection>)this.Cache);
            }
        }

        public TimeSpan? KustoCacheRefreshTimespan
        {
            get
            {
                return this._kustoRefreshTimer;
            }
            set
            {
                this._kustoRefreshTimer = value;
                if (this.KustoRefreshTimer == null)
                    return;
                this.KustoRefreshTimer.Stop();
                this.KustoRefreshTimer.Interval = this._kustoRefreshTimer.Value.TotalMilliseconds;
                this.KustoRefreshTimer.Start();
            }
        }

        protected KustoConnectionConfigurationContextBase Context
        {
            get
            {
                return this.ContextInstance;
            }
        }

        protected KustoConnectionConfigurationContextBase ContextInstance { get; set; }

        protected KustoConnectionLogicBase(KustoConnectionConfigurationContextBase context, IKustoUtilProvider kustoUtilProvider, TimeSpan? refreshTimeSpan = null)
        {
            this.ContextInstance = context;
            this._kustoUtilProvider = kustoUtilProvider;
            this.KustoCacheRefreshTimespan = refreshTimeSpan;
            List<Exception> exceptions = new List<Exception>();
            if (!this.TryCreateWriteFile(ref exceptions, this.Context.SqlResourcesWritePath, this.Context.SqlResourcesReadPath))
                throw new AggregateException((IEnumerable<Exception>)exceptions);
            if (!this.TryCreateWriteFile(ref exceptions, this.Context.ConfigFileWritePath, this.Context.ConfigFileReadPath))
                throw new AggregateException((IEnumerable<Exception>)exceptions);
            if (!this.UpdateLocalConfigFileAndCache(ref exceptions))
                throw new AggregateException((IEnumerable<Exception>)exceptions);
            this.KustoRefreshTimer = new System.Timers.Timer((refreshTimeSpan ?? SterlingKustoConstants.RefreshTimerInterval).TotalMilliseconds);
            this.KustoRefreshTimer.Elapsed += (ElapsedEventHandler)((sender, args) => this.RefreshCache());
            this.KustoRefreshTimer.Start();
        }

        public abstract AzureSQLClusterProfile GetClusterInfo(string clusterName);

        public KustoConnection GetConnectionByClusterName(string clusterName)
        {
            try
            {
                return this.Cache.Single<KustoConnection>((Func<KustoConnection, bool>)(connection =>
                {
                    if (!string.Equals(connection.ClusterName, clusterName, StringComparison.OrdinalIgnoreCase))
                        return string.Equals(connection.ClusterShortName, clusterName, StringComparison.OrdinalIgnoreCase);
                    return true;
                }));
            }
            catch (InvalidOperationException ex)
            {
                throw new NotSupportedException(string.Format("Cluster {0} is not supported by Kusto yet", (object)clusterName));
            }
        }

        public KustoConnection GetConnectionByConnectionString(string connectionString)
        {
            KustoConnectionStringInfo info = this._kustoUtilProvider.GetKustoConnectionStringInfo(connectionString);
            return this.Cache.FirstOrDefault<KustoConnection>((Func<KustoConnection, bool>)(conn =>
            {
                if (string.Equals(conn.DataSource, info.DataSource, StringComparison.OrdinalIgnoreCase))
                    return string.Equals(conn.InitialCatalog, info.InitialCatalog, StringComparison.OrdinalIgnoreCase);
                return false;
            }));
        }

        public List<string> GetAllConnectionStrings()
        {
            return this.Cache.Select<KustoConnection, string>((Func<KustoConnection, string>)(connection => connection.ConnectionString)).Distinct<string>().ToList<string>();
        }

        private void LoadConfigFromFile()
        {
            string xml = this.Context.ReadXmlFromConfig();
            if (xml == null)
                throw new XmlException("Reading XML timed out");
            List<KustoConnection> kustoConnectionList = this.Context.ReadConnectionsFromXml(xml);
            if (kustoConnectionList == null || kustoConnectionList.Count <= 0)
                return;
            lock (this.Cache)
                this.Cache = kustoConnectionList;
        }

        private void LoadConfigFromSql()
        {
            SqlConnection connection = this.TrySqlConnect();
            List<KustoConnection> kustoConnectionList;
            using (connection)
                kustoConnectionList = this.Context.ReadConnectionsFromSql(connection);
            if (kustoConnectionList == null || kustoConnectionList.Count <= 0)
                return;
            lock (this.Cache)
                this.Cache = kustoConnectionList;
        }

        private bool SafeLoadConfigFromFile(ref List<Exception> exceptions)
        {
            try
            {
                this.LoadConfigFromFile();
                return true;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                Trace.TraceWarning("Calling SafeLoadConfigFromFile to local local config failed with exception {0}", (object)ex);
                return false;
            }
        }

        private bool SafeLoadConfigFromSql(ref List<Exception> exceptions)
        {
            try
            {
                this.LoadConfigFromSql();
                return true;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                Trace.TraceWarning("Calling SafeLoadConfigFromSql to update local config failed with exception {0}", (object)ex);
                return false;
            }
        }

        private bool UpdateLocalConfigFileAndCache(ref List<Exception> exceptions)
        {
            FileStream fileStream = SafeFile.TryLockFile(this.Context.SqlResourcesWritePath);
            bool flag;
            if (fileStream == null)
            {
                flag = this.SafeLoadConfigFromFile(ref exceptions);
                if (!flag)
                    flag = this.SafeLoadConfigFromSql(ref exceptions);
            }
            else
            {
                using (fileStream)
                    flag = this.SafeLoadConfigFromSql(ref exceptions);
                if (!flag)
                    flag = this.SafeLoadConfigFromFile(ref exceptions);
            }
            return flag;
        }

        private void RefreshCache()
        {
            List<Exception> exceptions = new List<Exception>();
            FileStream fileStream = (FileStream)null;
            if (!this.Context.ConfigFileIsRecent())
            {
                Trace.TraceInformation("File older than threshold, refreshing Cache.");
                fileStream = SafeFile.TryLockFile(this.Context.SqlResourcesWritePath);
            }
            if (fileStream == null)
            {
                this.SafeLoadConfigFromFile(ref exceptions);
                Trace.TraceInformation("Config loaded from XML");
            }
            else
            {
                using (fileStream)
                {
                    if (!this.SafeLoadConfigFromSql(ref exceptions))
                        return;
                    Trace.TraceInformation("Configuration loaded from SQL.");
                    this.Context.TryPersistConfig(this.Cache);
                }
            }
        }

        private bool TryCreateWriteFile(ref List<Exception> exceptions, string writeFile, string readFile)
        {
            bool flag = true;
            for (int index = 1; index <= 3; ++index)
            {
                if (!File.Exists(writeFile))
                {
                    try
                    {
                        File.Copy(readFile, writeFile);
                        break;
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                        if (index == 3)
                        {
                            Trace.TraceInformation("Process failed to copy Source:{0} Destination:{1}", (object)readFile, (object)writeFile);
                            flag = false;
                        }
                    }
                }
                else
                    break;
            }
            return flag;
        }

        protected SqlConnection TrySqlConnect()
        {
            SqlConnection sqlConnection = this.Context.TrySqlConnect(3, SterlingKustoConstants.SqlConnectionBackoff);
            if (sqlConnection == null)
                throw new IOException("Could not connect to the SQL instance.");
            return sqlConnection;
        }
    }
    public class SterlingKustoConstants
    {
        public static readonly TimeSpan RefreshTimerInterval = TimeSpan.FromMinutes(30.0);
        public static readonly TimeSpan SqlConnectionBackoff = TimeSpan.FromSeconds(5.0);
        public static readonly TimeSpan ReadFileInterval = TimeSpan.FromSeconds(2.0);
        public static readonly TimeSpan WriteFileRetryInterval = TimeSpan.FromSeconds(1.0);
        public static readonly TimeSpan RecentTimespan = TimeSpan.FromMinutes(15.0);
        public const int SqlConnectionRetries = 3;
        public const int ReadFileRetryCount = 30;
        public const int WriteFileRetryCount = 15;
        public const int RetryCount = 3;
        public const int RetryIntervalInSeconds = 2;
        public const int RetryBackoffMultiplier = 2;
    }
    public class KustoConnection
    {
        private readonly IKustoUtilProvider _kustoUtilProvider;

        public string ClusterName { get; private set; }

        public string ClusterShortName { get; private set; }

        public string DataSource { get; private set; }

        public string InitialCatalog { get; private set; }

        public string MdsNamespace { get; private set; }

        public string ConnectionString
        {
            get
            {
                return this._kustoUtilProvider.GetKustoConnectionString(this.DataSource, this.InitialCatalog);
            }
        }

        public KustoConnection(IKustoUtilProvider kustoUtilProvider, string clusterName, string clusterShortName, string dataSource, string initialCatalog, string mdsNamespace)
        {
            this._kustoUtilProvider = kustoUtilProvider;
            this.ClusterName = clusterName;
            this.ClusterShortName = clusterShortName;
            this.DataSource = dataSource;
            this.InitialCatalog = initialCatalog;
            this.MdsNamespace = mdsNamespace;
        }
    }
    public interface IKustoConnectionLogic
    {
        AzureSQLClusterProfile GetClusterInfo(string clusterName);

        KustoConnection GetConnectionByConnectionString(string connectionString);

        KustoConnection GetConnectionByClusterName(string clusterName);

        List<string> GetAllConnectionStrings();
    }

    internal class RetryHelper
    {
        public static void RetryVoid(Action action, int numRetries, TimeSpan retryInterval)
        {
            RetryHelper.Retry<object>((Func<object>)(() =>
            {
                action();
                return (object)null;
            }), numRetries, retryInterval);
        }

        public static T Retry<T>(Func<T> action, int numRetries, TimeSpan retryInterval)
        {
            if (action == null)
                throw new ArgumentException("\"action\" cannot be null.");
            if (numRetries < 0)
                throw new InvalidOperationException(string.Format("{0} retries specified, must be >= 0", (object)numRetries));
            List<Exception> exceptionList = new List<Exception>();
            for (int index = 0; index < numRetries; ++index)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    exceptionList.Add(ex);
                }
                Thread.Sleep(retryInterval);
            }
            throw new AggregateException((IEnumerable<Exception>)exceptionList);
        }
    }
    public static class SafeFile
    {
        public static bool TryWriteFile(string content, string filePath, int numRetries, TimeSpan retryInterval)
        {
            try
            {
                RetryHelper.RetryVoid((Action)(() => File.WriteAllText(filePath, content)), numRetries, retryInterval);
                SafeFile.SafeTrace("Updating Cache File.");
                return true;
            }
            catch (Exception ex)
            {
                SafeFile.SafeTrace("Hit exception while writing file.");
                SafeFile.SafeTrace("Exception: {0}.", (object)ex);
                return false;
            }
        }

        public static FileStream TryLockFile(string filePath)
        {
            try
            {
                return File.OpenWrite(filePath);
            }
            catch (IOException ex)
            {
                return (FileStream)null;
            }
        }

        public static string ReadFile(string filePath, int numRetries, TimeSpan retryInterval)
        {
            try
            {
                return RetryHelper.Retry<string>((Func<string>)(() => File.ReadAllText(filePath)), numRetries, retryInterval);
            }
            catch (Exception ex)
            {
                SafeFile.SafeTrace("Could not read the file {0}", (object)filePath);
                return (string)null;
            }
        }

        public static bool FileOlderThan(TimeSpan range, string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException(string.Format("File {0} does not exist!", (object)filePath));
            DateTime dateTime = DateTime.Now - range;
            SafeFile.SafeTrace("File is {0} minutes old", (object)(DateTime.Now - File.GetLastWriteTime(filePath)).TotalMinutes);
            return File.GetLastWriteTime(filePath) < dateTime;
        }

        private static void SafeTrace(string format, params object[] args)
        {
            try
            {
                Trace.TraceInformation(string.Format("[{0}] {1}", (object)string.Format("ProcessId:{0} Thread:{1} ProcessName:{2}", (object)Process.GetCurrentProcess().Id.ToString(), (object)Thread.CurrentThread.ManagedThreadId, (object)Process.GetCurrentProcess().ProcessName), (object)format), args);
            }
            catch (Exception ex)
            {
            }
        }
    }
    public class SterlingKustoUtilProvider : IKustoUtilProvider
    {
        private static readonly Lazy<SterlingKustoUtilProvider> _default = new Lazy<SterlingKustoUtilProvider>((Func<SterlingKustoUtilProvider>)(() => new SterlingKustoUtilProvider()));

        public static SterlingKustoUtilProvider Default
        {
            get
            {
                return SterlingKustoUtilProvider._default.Value;
            }
        }

        public virtual DataTable QueryTable(string connectionString, string query, bool adminRequired)
        {
            return this.QueryTableAndReturnResultWithStatus(connectionString, query, adminRequired).Tables[0];
        }

        public virtual DataSet QueryTableAndReturnResultWithStatus(string connectionString, string query, bool adminRequired)
        {
            return this.QueryTableAndReturnResultWithStatus(connectionString, query, adminRequired, 2, 3, 2);
        }

        public virtual DataSet QueryTableAndReturnResultWithStatus(string connectionString, string query, bool adminRequired, int retryIntervalInSeconds, int numberOfRetries, int retryBackoffMultiplier)
        {
            if (retryIntervalInSeconds < 0)
                throw new ArgumentException("retryIntervalInSeconds cannot be negative");
            if (numberOfRetries <= 0)
                throw new ArgumentException("numberOfRetries must be positive");
            if (retryBackoffMultiplier < 0)
                throw new ArgumentException("retryBackoffMultiplier must be positive");
            DataSet dataSet = (DataSet)null;
            int millisecondsTimeout = retryIntervalInSeconds * 1000;
            string queryToLog = query.Substring(0, Math.Min(query.Length, 100));
            for (int index = 1; index <= numberOfRetries; ++index)
            {
                try
                {
                    dataSet = this.QueryTableAndReturnResultWithStatusProtected(connectionString, query, adminRequired);
                    break;
                }
                catch (KustoRequestThrottledException ex)
                {
                    this.LogNonRetryFailure((Exception)ex, queryToLog);
                    throw;
                }
                catch (KustoRequestException ex)
                {
                    this.LogNonRetryFailure((Exception)ex, queryToLog);
                    throw;
                }
                catch (KustoServicePartialQueryFailureLimitsExceededException ex)
                {
                    this.LogNonRetryFailure((Exception)ex, queryToLog);
                    throw;
                }
                catch (KustoException ex)
                {
                    Trace.TraceInformation(ex.ToString());
                    Trace.TraceInformation(string.Format("Kusto query failed with exception {0}. Retry attempt {1} of {2}. Query truncated to 100 characters: {3}", (object)ex.GetType().FullName, (object)index, (object)numberOfRetries, (object)queryToLog));
                    if (index == numberOfRetries)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(millisecondsTimeout);
                        millisecondsTimeout *= retryBackoffMultiplier;
                    }
                }
                catch (Exception ex)
                {
                    this.LogNonRetryFailure(ex, queryToLog);
                    throw;
                }
            }
            return dataSet;
        }

        public virtual string RewriteConnectionString(string connectionString, string userName = null, string password = null, string applicationClientId = null, string applicationKey = null, string dataAccessTracer = null, string applicationCertificateThumbprint = null)
        {
            KustoConnectionStringBuilder connectionStringBuilder = new KustoConnectionStringBuilder(connectionString);
            if (!string.IsNullOrWhiteSpace(applicationClientId) && !string.IsNullOrWhiteSpace(applicationKey))
            {
                connectionStringBuilder.DstsFederatedSecurity = false;
                connectionStringBuilder.FederatedSecurity = true;
                connectionStringBuilder.ApplicationClientId = applicationClientId;
                connectionStringBuilder.ApplicationKey = applicationKey;
            }
            else if (!string.IsNullOrEmpty(applicationCertificateThumbprint))
            {
                connectionStringBuilder.ApplicationCertificateThumbprint = applicationCertificateThumbprint;
                connectionStringBuilder.DstsFederatedSecurity = true;
                connectionStringBuilder.FederatedSecurity = false;
            }
            else if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                connectionStringBuilder.UserID = userName;
                connectionStringBuilder.Password = password;
                connectionStringBuilder.DstsFederatedSecurity = false;
                connectionStringBuilder.FederatedSecurity = false;
            }
            connectionStringBuilder.ApplicationNameForTracing = dataAccessTracer ?? string.Empty;
            return connectionStringBuilder.ConnectionString;
        }

        public virtual string GetKustoConnectionString(string dataSource, string initialCatalog)
        {
            KustoConnectionStringBuilder connectionStringBuilder = new KustoConnectionStringBuilder(dataSource, initialCatalog);
            connectionStringBuilder.DstsFederatedSecurity = dataSource.EndsWith(".kusto.chinacloudapi.cn", StringComparison.InvariantCultureIgnoreCase) || dataSource.EndsWith(".kusto.cloudapi.de", StringComparison.InvariantCultureIgnoreCase) || dataSource.EndsWith(".kusto.usgovcloudapi.net", StringComparison.InvariantCultureIgnoreCase);
            connectionStringBuilder.FederatedSecurity = !connectionStringBuilder.DstsFederatedSecurity;
            return connectionStringBuilder.ToString();
        }

        public virtual KustoConnectionStringInfo GetKustoConnectionStringInfo(string connectionString)
        {
            KustoConnectionStringBuilder connectionStringBuilder = new KustoConnectionStringBuilder(connectionString);
            return new KustoConnectionStringInfo()
            {
                DataSource = connectionStringBuilder.DataSource,
                InitialCatalog = connectionStringBuilder.InitialCatalog
            };
        }

        protected virtual DataSet QueryTableAndReturnResultWithStatusProtected(string connectionString, string query, bool adminRequired)
        {
            IDataReader reader;
            if (adminRequired)
            {
                using (ICslAdminProvider cslAdminProvider = KustoClientFactory.CreateCslAdminProvider(connectionString))
                    reader = cslAdminProvider.ExecuteControlCommand(query, (ClientRequestProperties)null);
            }
            else
            {
                using (ICslQueryProvider cslQueryProvider = KustoClientFactory.CreateCslQueryProvider(connectionString))
                    reader = cslQueryProvider.ExecuteQuery(query);
            }
            DataSet dataSet = reader.ToDataSet();
            reader.Close();
            return dataSet;
        }

        private void LogNonRetryFailure(Exception e, string queryToLog)
        {
            Trace.TraceInformation(e.ToString());
            Trace.TraceInformation(string.Format("Kusto query failed with exception {0}. Retry aborted due to exception type. Query truncated to 100 characters: {1}", (object)e.GetType().FullName, (object)queryToLog));
        }
    }
    public class KustoConnectionFactoryV2 : IKustoConnectionFactory
    {
        public KustoConnection Create(string clusterName, string clusterShortName, string dataSource, string initialCatalog, string mdsNamespace)
        {
            return new KustoConnection((IKustoUtilProvider)SterlingKustoUtilProvider.Default, clusterName, clusterShortName, dataSource, initialCatalog, mdsNamespace);
        }
    }
    public class KustoConnectionConfigurationContextV2 : KustoConnectionConfigurationContextBase
    {
        public static KustoConnectionConfigurationContextV2 DefaultContext = new KustoConnectionConfigurationContextV2("Data Source=dw-srvr-01.database.windows.net;Initial Catalog=Test3DB_1;User ID=Streamsreader;Password=$tream$READER", "KustoConnectionConfiguration.xml");

        protected KustoConnectionConfigurationContextV2(string sqlConnectionString, string configFileName)
          : base((IKustoConnectionFactory)new KustoConnectionFactoryV2(), sqlConnectionString, configFileName, "Resources", $@"{AppDomain.CurrentDomain.BaseDirectory}\log", $@"{AppDomain.CurrentDomain.BaseDirectory}\log\Write")
        {
        }

        public string GetMdsNamespace(SqlConnection connection, string clusterName)
        {
            SqlCommand sqlCommand = new SqlCommand(string.Format("dbo.proc_kusto_cluster"), connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ClusterName", clusterName);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            string str = sqlDataReader.GetString(sqlDataReader.GetOrdinal("MdsNamespace"));
            sqlDataReader.Close();
            return str;
        }

        public IList<AzureSQLFabricCluster> GetFabricClusters(SqlConnection connection, string clusterName)
        {
            SqlCommand sqlCommand = new SqlCommand(string.Format("dbo.proc_fabric_cluster"), connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ClusterName", clusterName);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            IList<AzureSQLFabricCluster> sqlFabricClusterList = (IList<AzureSQLFabricCluster>)new List<AzureSQLFabricCluster>();
            while (sqlDataReader.Read())
            {
                AzureSQLFabricCluster sqlFabricCluster = new AzureSQLFabricCluster();
                sqlFabricCluster.ClusterAddress = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ring_address"));
                sqlFabricCluster.Id = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ring_short_name"));
                AzureSQLFabricClusterType? nullable = AzureSQLFabricClusterTypeConverter.Convert(sqlDataReader.GetString(sqlDataReader.GetOrdinal("ring_type")));
                if (nullable.HasValue)
                    sqlFabricCluster.Type = nullable.Value;
                sqlFabricClusterList.Add(sqlFabricCluster);
            }
            sqlDataReader.Close();
            return sqlFabricClusterList;
        }
    }
    public static class AzureSQLFabricClusterTypeConverter
    {
        public static AzureSQLFabricClusterType? Convert(string input)
        {
            return input == "Control" ? new AzureSQLFabricClusterType?(AzureSQLFabricClusterType.Control) : (input == "Tenant" ? new AzureSQLFabricClusterType?(AzureSQLFabricClusterType.Tenant) : (input == "Utility" ? new AzureSQLFabricClusterType?(AzureSQLFabricClusterType.Utility) : new AzureSQLFabricClusterType?()));
        }
    }
    public class KustoConnectionLogicV2 : KustoConnectionLogicBase
    {
        private static readonly Lazy<KustoConnectionLogicV2> _default = new Lazy<KustoConnectionLogicV2>((Func<KustoConnectionLogicV2>)(() => new KustoConnectionLogicV2(KustoConnectionConfigurationContextV2.DefaultContext)));

        protected KustoConnectionConfigurationContextV2 Context
        {
            get
            {
                return (KustoConnectionConfigurationContextV2)this.ContextInstance;
            }
        }

        public static KustoConnectionLogicV2 Default
        {
            get
            {
                return KustoConnectionLogicV2._default.Value;
            }
        }

        public KustoConnectionLogicV2(KustoConnectionConfigurationContextV2 context)
          : base((KustoConnectionConfigurationContextBase)context, (IKustoUtilProvider)SterlingKustoUtilProvider.Default, new TimeSpan?())
        {
        }

        public override AzureSQLClusterProfile GetClusterInfo(string clusterName)
        {
            SqlConnection connection = this.TrySqlConnect();
            string mdsNamespace;
            IList<AzureSQLFabricCluster> fabricClusters;
            using (connection)
            {
                mdsNamespace = this.Context.GetMdsNamespace(connection, clusterName);
                fabricClusters = this.Context.GetFabricClusters(connection, clusterName);
            }
            return new AzureSQLClusterProfile()
            {
                ClusterName = clusterName,
                MdsNamespace = mdsNamespace,
                FabricClusters = fabricClusters
            };
        }
    }

    public class KustoResponse
    {
        public IList<string> Queries { get; private set; }

        public DataTable ResultTable { get; private set; }

        public DataTable QueryStatusTable { get; private set; }

        public AggregateException AggregateException { get; private set; }

        public string ExceptionMessage
        {
            get
            {
                if (this.AggregateException == null)
                    return string.Empty;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Exception innerException in this.AggregateException.InnerExceptions)
                    stringBuilder.AppendLine(innerException.Message);
                return stringBuilder.ToString();
            }
        }

        public KustoResponse(DataTable resultTable, IEnumerable<Exception> exceptionList, IEnumerable<string> finalQueries = null)
        {
            this.ResultTable = resultTable;
            this.AggregateException = !exceptionList.Any<Exception>() ? (AggregateException)null : new AggregateException(exceptionList);
            if (finalQueries != null)
            {
                this.Queries = (IList<string>)new List<string>(finalQueries);
            }
            else
            {
                this.Queries = (IList<string>)new List<string>();
                this.Queries.Add("Query was not specified");
            }
        }

        public KustoResponse(DataTable resultTable, DataTable queryStatusTable, IEnumerable<Exception> exceptionList, IEnumerable<string> finalQueries = null)
          : this(resultTable, exceptionList, finalQueries)
        {
            this.QueryStatusTable = queryStatusTable;
        }
    }
    public abstract class KustoClientBase
    {
        private static readonly TraceSource _traceSource = new TraceSource("SterlingKustoClient");
        private readonly Dictionary<string, KustoConnection> _sterlingKustoConnections;
        private readonly IKustoQueryLogic _kustoQueryLogic;
        private readonly IKustoUtilProvider _kustoProvider;
        private readonly IKustoConnectionLogic _kustoConnectionLogic;

        public IList<string> ConnectionStrings { get; private set; }

        public TimeSpan RequestTimeout { get; private set; }

        public ReadOnlyCollection<string> GetKustoTables(string connectionString)
        {
            KustoClientBase._traceSource.TraceInformation("Getting Kusto tables");
            List<string> stringList = new List<string>();
            try
            {
                foreach (DataRow row in (InternalDataCollectionBase)this._kustoProvider.QueryTable(connectionString, ".show tables", false).Rows)
                    stringList.Add(row["TableName"].ToString());
            }
            catch (Exception ex)
            {
                KustoClientBase._traceSource.TraceInformation("Failed to get Kusto tables with Exception: {0}", (object)ex);
                throw;
            }
            return stringList.AsReadOnly();
        }

        protected internal KustoClientBase(string connectionString, IKustoQueryLogic kustoQueryLogic, IKustoConnectionLogic kustoConnectionLogic, IKustoUtilProvider kustoProvider, TimeSpan? requestTimeout = null)
          : this((IEnumerable<string>)new string[1]
          {
        connectionString
          }, kustoQueryLogic, kustoConnectionLogic, kustoProvider, requestTimeout)
        {
        }

        protected internal KustoClientBase(IEnumerable<string> connectionStrings, IKustoQueryLogic kustoQueryLogic, IKustoConnectionLogic kustoConnectionLogic, IKustoUtilProvider kustoProvider, TimeSpan? requestTimeout = null)
        {
            this._kustoQueryLogic = kustoQueryLogic;
            this._kustoConnectionLogic = kustoConnectionLogic;
            this._kustoProvider = kustoProvider;
            this.ConnectionStrings = (IList<string>)new List<string>(connectionStrings);
            this.RequestTimeout = requestTimeout ?? TimeSpan.Zero;
            this._sterlingKustoConnections = new Dictionary<string, KustoConnection>();
            foreach (string connectionString1 in (IEnumerable<string>)this.ConnectionStrings)
            {
                KustoConnection connectionString2 = this._kustoConnectionLogic.GetConnectionByConnectionString(connectionString1);
                this._sterlingKustoConnections[connectionString1] = connectionString2;
            }
            KustoClientBase._traceSource.Switch.Level |= SourceLevels.Information;
        }

        public static void SetTraceLevel(SourceLevels level)
        {
            KustoClientBase._traceSource.Switch.Level = level;
        }

        public static void AddTraceListener(TraceListener listener)
        {
            KustoClientBase._traceSource.Listeners.Add(listener);
        }

        public KustoResponse QueryTables(KustoRequest request)
        {
            return this.QueryTablesAsync(request).Result;
        }

        public async Task<KustoResponse> QueryTablesAsync(KustoRequest request)
        {
            return await this.QueryTablesAsync((IEnumerable<KustoRequest>)new KustoRequest[1]
            {
        request
            });
        }

        public async Task<KustoResponse> QueryTablesAsync(IEnumerable<KustoRequest> requests)
        {
            List<Exception> exceptionList = new List<Exception>();
            List<string> queryLists = new List<string>();
            Tuple<DataTable, DataTable> tuple = await this.QueryTables(requests, (ICollection<Exception>)exceptionList, (ICollection<string>)queryLists);
            this._kustoQueryLogic.ModifyDataTableValuesForDateTimeColumn(tuple.Item1);
            this._kustoQueryLogic.ModifyDataTableValuesForDateTimeColumn(tuple.Item2);
            return new KustoResponse(tuple.Item1, tuple.Item2, (IEnumerable<Exception>)exceptionList, (IEnumerable<string>)queryLists);
        }

        private string GetCleanKustoTableName(string tableNamePattern, string connectionString)
        {
            tableNamePattern = this._kustoQueryLogic.StripQueryNamespace(tableNamePattern);
            KustoClientBase._traceSource.TraceInformation("query table names for pattern {0} for {1}", (object)tableNamePattern, (object)connectionString);
            tableNamePattern = tableNamePattern.Trim();
            return tableNamePattern;
        }

        private IList<string> GetKustoTablesFromRequest(KustoRequest request, string connectionString)
        {
            return (IList<string>)request.KustoTableList.Select<string, string>((Func<string, string>)(tName => this._kustoQueryLogic.StripQueryNamespace(tName))).ToList<string>().Select<string, string>((Func<string, string>)(table => this.GetCleanKustoTableName(table, connectionString))).ToList<string>();
        }

        private async Task<DataSet> QuerySingleTableReturnResultWithStatusAsync(string connectionString, KustoRequest request, string queryString, string tableName)
        {
            return await Task.Run<DataSet>((Func<DataSet>)(() =>
            {
                string query = this._kustoQueryLogic.StripQueryNamespace(this._kustoQueryLogic.ModifyQueryDateTimeFilter(tableName, queryString, request));
                DateTime utcNow = DateTime.UtcNow;
                DataSet dataSet = this._kustoProvider.QueryTableAndReturnResultWithStatus(connectionString, query, request.AdminRequired, request.RetryIntervalInSeconds, request.NumberOfRetries, request.RetryBackoffMultiplier);
                KustoClientBase._traceSource.TraceEvent(TraceEventType.Information, 0, "Finished Kusto query data in {0} seconds with {1} rows: {2}", (object)(DateTime.UtcNow - utcNow).TotalSeconds, (object)dataSet.Tables[0].Rows.Count, (object)query.Substring(0, Math.Min(query.Length, 100)));
                return dataSet;
            }));
        }

        private DataSet QuerySingleTableReturnResultWithStatus(string connectionString, KustoRequest request, string queryString, string tableName)
        {

                string query = this._kustoQueryLogic.StripQueryNamespace(this._kustoQueryLogic.ModifyQueryDateTimeFilter(tableName, queryString, request));
                DateTime utcNow = DateTime.UtcNow;
                DataSet dataSet = this._kustoProvider.QueryTableAndReturnResultWithStatus(connectionString, query, request.AdminRequired, request.RetryIntervalInSeconds, request.NumberOfRetries, request.RetryBackoffMultiplier);
                KustoClientBase._traceSource.TraceEvent(TraceEventType.Information, 0, "Finished Kusto query data in {0} seconds with {1} rows: {2}", (object)(DateTime.UtcNow - utcNow).TotalSeconds, (object)dataSet.Tables[0].Rows.Count, (object)query.Substring(0, Math.Min(query.Length, 100)));
                return dataSet;
           
        }

        private async Task<Tuple<DataTable, DataTable>> QueryTables(IEnumerable<KustoRequest> requests, ICollection<Exception> exceptionList, ICollection<string> queryList)
        {
            DataTable resultTable = new DataTable();
            DataTable queryStatusTable = new DataTable();
            HashSet<Task<DataSet>> tasks = new HashSet<Task<DataSet>>();
            foreach (string connectionString in (IEnumerable<string>)this.ConnectionStrings)
            {
                foreach (KustoRequest request in requests)
                {
                    IList<string> tablesFromRequest = this.GetKustoTablesFromRequest(request, connectionString);
                    if (!tablesFromRequest.Any<string>())
                        exceptionList.Add((Exception)new ArgumentException(string.Format("Not able to find Kusto table with pattern {0}", (object)string.Join(",", (IEnumerable<string>)request.KustoTableList))));
                    foreach (string tableName in (IEnumerable<string>)tablesFromRequest)
                    {
                        foreach (string query in (IEnumerable<string>)request.Queries)
                        {
                            var dataSet=this.QuerySingleTableReturnResultWithStatus(connectionString, request, query, tableName);

                            resultTable.Merge(dataSet.Tables[0], true, MissingSchemaAction.Add);
                            queryStatusTable.Merge(dataSet.Tables[2], true, MissingSchemaAction.Add);

                            // tasks.Add(this.QuerySingleTableReturnResultWithStatusAsync(connectionString, request, query, tableName));
                            //queryList.Add(this._kustoQueryLogic.ModifyQueryDateTimeFilter(tableName, query, request));
                        }
                    }
                }
            }
            while (tasks.Any<Task<DataSet>>())
            {
               
                try
                {
                    Task<DataSet> task = await Task.WhenAny<DataSet>((IEnumerable<Task<DataSet>>)tasks);
                    tasks.Remove(task);

                    DataSet dataSet = await task;
                    resultTable.Merge(dataSet.Tables[0], true, MissingSchemaAction.Add);
                    queryStatusTable.Merge(dataSet.Tables[2], true, MissingSchemaAction.Add);
                }
                catch (Exception ex)
                {
                    exceptionList.Add(ex);
                }
            }
            resultTable.EndLoadData();
            queryStatusTable.EndLoadData();
            resultTable.AcceptChanges();
            queryStatusTable.AcceptChanges();
            return new Tuple<DataTable, DataTable>(resultTable, queryStatusTable);
        }
    }
    public class KustoClientV2 : KustoClientBase
    {
        public KustoClientV2(string connectionString, IKustoUtilProvider kustoUtilProvider, TimeSpan? requestTimeout = null)
      : this((IEnumerable<string>)new string[1]
      {
        connectionString
      }, kustoUtilProvider, requestTimeout)
        {
        }

        public KustoClientV2(IEnumerable<string> connectionStrings, IKustoUtilProvider kustoUtilProvider, TimeSpan? requestTimeout = null)
          : base(connectionStrings, (IKustoQueryLogic)KustoQueryLogic.Default, (IKustoConnectionLogic)KustoConnectionLogicV2.Default, kustoUtilProvider, requestTimeout)
        {
        }

        public static string GetConnecionString(string shortClusterName)
        {
            return KustoConnectionLogicV2.Default.GetConnectionByClusterName(shortClusterName).ConnectionString;
        }
    }
}
    

