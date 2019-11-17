using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Services.Client;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlAzure.ServiceManagement.Authentication;
using ClientCluster = Microsoft.SqlServer.Management.Service.Client.Cluster;

namespace RunnerCommon
{
    /// <summary>
    /// The purpose of querying Cms
    /// </summary>
    public enum CmsQueryPurpose
    {
        /// <summary>
        /// No purpose specified
        /// </summary>
        Uninitialized = 0,

        /// <summary>
        /// To confirm creating an incident
        /// </summary>
        IncidentConfirmation,

        /// <summary>
        /// Other purposes
        /// </summary>
        Other
    }
    public interface ISterlingCasContext
    {
        DataSet QueryCms(string query);

        //DataSet QueryNode(string tenantClusterAddress, string nodeName, string sqlInstanceName, string query);

        //bool ConfirmIncident(string query, IHealthProperty healthProperty, int severity, Func<DataTable, bool> actionToConfirm = null);
    }

    /// <summary>
    /// Query sterling nodes over TDS. Uses CAS to pipe the query to CMS in
    /// control ring or (through NodeAgent) to the DB nodes on tenant rings).
    /// This class will be replaced/refactored by nuget package later.
    /// </summary>
    public class SterlingCasContext : ISterlingCasContext
    {
        /// <summary>
        /// Cluster client certificate for CAS
        /// </summary>
        public ClientCluster.Cluster Client { get; private set; }

        /// <summary>
        /// Gets the fabric cluster
        /// </summary>
        public ClientCluster.FabricCluster ControlFabricCluster
        {
            get
            {
                // Execute the request to get back a dataset
                // note: we might have many CR rings, one primary, others are secondary. we just choose the first one for now
                // until we have a property in cluster member to indicate which one is primary
                ClientCluster.FabricCluster cluster = null;

                // DevNote: Running this part in multiple threads cause problem with ToList() because of multiple iterations
                lock (this.Client.FabricClusters)
                {
                    cluster = this.Client.FabricClusters.Where(fc => fc.IsControlRing).ToList().First();
                }

                return cluster;
            }
        }

        /// <summary>
        /// Event store for logging CMS querying-related activities
        /// </summary>
        //public CmsQueryEventStore CmsQueryEvents { get; private set; }

        /// <summary>
        /// Event store for logging incident confirmation activities
        /// </summary>
        //public IncidentConfirmationEventStore IncidentConfirmEvents { get; private set; }

        /// <summary>
        /// CMS query throttling manager
        /// </summary>
        //public IThrottlingManager ThrottleManager { get; private set; }

        /// <summary>
        /// Runner name for this CAS context
        /// </summary>
        public string RunnerName { get; private set; }

        public SterlingCasContext(string runnerName, string clusterAddress, string thumbprint, bool useCertificate = true)
        {
            this.RunnerName = runnerName;
            X509Certificate2 certificate = null;
            if (useCertificate)
            {
                //certificate = EncryptionHelper.GetCertificate(thumbprint);
            }

            this.Client = CreateClient(clusterAddress, certificate);

            //this.CmsQueryEvents = new CmsQueryEventStore();
            //this.IncidentConfirmEvents = new IncidentConfirmationEventStore();
            //this.ThrottleManager = new ThrottlingManager();
        }

        /// <summary>
        /// Incident confirmation against CMS
        /// </summary>
        /// <param name="query">Query for incident confirmation:
        ///                     The query should be able to determine if an alert still occurs.
        ///                     The query should return an empty row for indicating that the alert does not occur any more.
        /// </param>
        /// <param name="healthProperty">Alerted health property object</param>
        /// <param name="severity">Severity of incident to be created/rejected</param>
        /// <param name="actionToConfirm">An action that can be used to confirm/reject incident creation, or null
        /// if the logic that checks the existince of a row to confirm incident creation is used.</param>
        /// <returns>True: Incident Confirmed, False: Alert does not occur any more.</returns>
//        public bool ConfirmIncident(string query, IHealthProperty healthProperty, int severity,
//            Func<DataTable, bool> actionToConfirm = null)

//{
//            bool isConfirmed = true;

//            this.IncidentConfirmEvents.AtIncidentConfirmationDecisionStarting(new IncidentConfirmationEventArgs
//            {
//                EventTimeStamp = DateTime.UtcNow,
//                TriggeredEvent = IncidentConfirmationEventName.IncidentConfirmationStarting,
//                RunnerName = this.RunnerName,
//                HealthPropertyInfo = healthProperty,
//                Severity = severity
//            });

//            try
//            {
//                string reason = "";
//                if (!ThrottleManager.ShouldThrottle(ref reason))
//                {
//                    DataSet result = ExecuteCmsQuery(query, CmsQueryPurpose.IncidentConfirmation);
//                    if (result == null)
//                    {
//                        throw new ApplicationException(string.Format("Runner: {0}, An empty dataset from Query execution {1}",
//                                                                      this.RunnerName, query));
//                    }

//                    var dataTable = result.Tables[0];

//                    actionToConfirm = actionToConfirm ?? (dt => dt.Rows.Count != 0);

//                    // Call the user-defined callback function for incident confirmation.
//                    //
//                    isConfirmed = actionToConfirm(dataTable);
//                }
//                else
//                {
//                    // Emit a CMS query throttled event
//                    CmsQueryEvents.AtCmsQueryThrottled(new CmsQueryEventArgs
//                    {
//                        EventTimeStamp = DateTime.UtcNow,
//                        TriggeredEvent = CmsQueryEventName.CmsQueryThrottled,
//                        CmsQueryStatement = query,
//                        CmsQueryPurpose = CmsQueryPurpose.IncidentConfirmation,
//                        CmsQueryDetails = reason
//                    });
//                }
//            }
//            finally
//            {
//                this.IncidentConfirmEvents.AtIncidentConfirmationDecisionEnded(new IncidentConfirmationEventArgs
//                {
//                    EventTimeStamp = DateTime.UtcNow,
//                    TriggeredEvent = IncidentConfirmationEventName.IncidentConfirmationEnded,
//                    RunnerName = this.RunnerName,
//                    HealthPropertyInfo = healthProperty,
//                    CmsQueryStatement = query,
//                    Severity = severity,
//                    ConfirmationResult = isConfirmed ? ConfirmationDecision.Confirmed : ConfirmationDecision.Rejected
//                });
//            }

//            return isConfirmed;
//        }

        /// <summary>
        /// Queries Cms with or without throttling based on the purpose
        /// </summary>
        /// <param name="query">The query statement</param>
        /// <returns>The dataset result of query.</returns>
        public DataSet QueryCms(string query)
        {
            DataSet result = new DataSet();
            result = ExecuteCmsQuery(query, CmsQueryPurpose.Other);

            return result;
        }


        /// <summary>
        /// Query CMS database
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="purpose">The reason of query, if incidentConfirmation throttling will apply</param>
        /// <returns>The dataset result of query.</returns>
        private DataSet ExecuteCmsQuery(string query, CmsQueryPurpose purpose)
        {
            Trace.TraceInformation(string.Format("Query Cms: {0}", query));
            DataSet dataSet = null;

            // Emit the event at the start of a CMS query
            //CmsQueryEvents.AtCmsQueryStarting(new CmsQueryEventArgs
            //{
            //    EventTimeStamp = DateTime.UtcNow,
            //    TriggeredEvent = CmsQueryEventName.CmsQueryStarting,
            //    CmsQueryStatement = query,
            //    CmsQueryPurpose = purpose
            //});

            int iTryCount = 0;
            bool bSuccess = false;
            while (!bSuccess)
            {
                try
                {
                    dataSet = this.ControlFabricCluster.ExecuteCMSQuery(query, this.Client);

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    if (++iTryCount > 3)
                    {
                        // Emit a CMS query failure event
                        //CmsQueryEvents.AtCmsQueryFailed(new CmsQueryEventArgs
                        //{
                        //    EventTimeStamp = DateTime.UtcNow,
                        //    TriggeredEvent = CmsQueryEventName.CmsQueryFailed,
                        //    CmsQueryStatement = query,
                        //    CmsQueryPurpose = purpose
                        //});

                        throw;
                    }
                    else
                    {
                        Trace.TraceInformation("Try again to QueryCms! Got Exception: " + e);

                        // Emit a CMS query in-progress event
                        //CmsQueryEvents.AtCmsQueryInProgress(new CmsQueryEventArgs
                        //{
                        //    EventTimeStamp = DateTime.UtcNow,
                        //    TriggeredEvent = CmsQueryEventName.CmsQueryInProgress,
                        //    CmsQueryStatement = query,
                        //    CmsQueryPurpose = purpose
                        //});

                        Thread.Sleep(15 * 1000);
                    }
                }
            }

            // If query fails, the CAS call doesn't throw! Instead it returns a DataSet of a single DataTable. This table has a single row and a single
            // column named INTERNAL_SERVER_ERROR. The cell has a string that contains the exception message.
            //
            // See DS_Main TFS change 782246 by REDMOND\jreed on 08/06/2015 19:22:44 - "Exposes exceptions to XTS when CMS/Node queries fail"
            if (dataSet.Tables.Count == 1 &&
                dataSet.Tables[0].Columns.Count == 1 && dataSet.Tables[0].Columns[0].ColumnName == "INTERNAL_SERVER_ERROR" &&
                dataSet.Tables[0].Rows.Count == 1)
            {
                // Emit a CMS query failure event
                //CmsQueryEvents.AtCmsQueryFailed(new CmsQueryEventArgs
                //{
                //    EventTimeStamp = DateTime.UtcNow,
                //    TriggeredEvent = CmsQueryEventName.CmsQueryFailed,
                //    CmsQueryStatement = query,
                //    CmsQueryPurpose = purpose
                //});

                string message = Convert.ToString(dataSet.Tables[0].Rows[0]["INTERNAL_SERVER_ERROR"]);
                throw new Exception(message);
            }

            SterlingCasContext.UpdateDataSetValuesForDateTimeColumn(dataSet);

            // Emit a CMS query completion event
            //CmsQueryEvents.AtCmsQueryCompleted(new CmsQueryEventArgs
            //{
            //    EventTimeStamp = DateTime.UtcNow,
            //    TriggeredEvent = CmsQueryEventName.CmsQueryCompleted,
            //    CmsQueryStatement = query,
            //    CmsQueryPurpose = purpose
            //});

            return dataSet;
        }

        /// <summary>
        /// Query Node
        /// </summary>
        /// <param name="tenantClusterAddress">tenant cluster full address</param>
        /// <param name="nodeName">node name</param>
        /// <param name="sqlInstanceName">sql instance name, i.e., appname</param>
        /// <param name="query">the query</param>
        /// <param name="useCertificate">if set to <c>true</c> [use certificate].</param>
        /// <returns>
        /// the query result
        /// </returns>
        //public DataSet QueryNode(string tenantClusterAddress, string nodeName, string sqlInstanceName, string query)
        //{
        //    Trace.TraceInformation(string.Format("QueryNode, tenantClusterAddress: {0},  nodeName: {1}, sqlInstanceName: {2}", tenantClusterAddress, nodeName, sqlInstanceName));

        //    DataSet dataSet = null;

        //    int iTryCount = 0;
        //    bool bSuccess = false;
        //    while(!bSuccess)
        //    {
        //        try
        //        {
        //            var node = new ClientCluster.FabricNode { FabricCluster = tenantClusterAddress, Name = nodeName };
        //            dataSet = node.ExecuteQuery(this.Client, sqlInstanceName, query);
        //            bSuccess = true;
        //        }
        //        catch (Exception e)
        //        {
        //            if(++iTryCount > 3)
        //            {
        //                throw;
        //            }
        //            else
        //            {
        //                Trace.TraceInformation("Try again to QueryNode! Got Exception: " + e);
        //                Thread.Sleep(15 * 1000);
        //            }
        //        }
        //    }

        //    // If query fails, the CAS call doesn't throw! Instead it returns a DataSet of a single DataTable. This table has a single row and a single
        //    // column named INTERNAL_SERVER_ERROR. The cell has a string that contains the exception message.
        //    //
        //    // See DS_Main TFS change 782246 by REDMOND\jreed on 08/06/2015 19:22:44 - "Exposes exceptions to XTS when CMS/Node queries fail"
        //    if (
        //        dataSet.Tables.Count == 1 &&
        //        dataSet.Tables[0].Columns.Count == 1 && dataSet.Tables[0].Columns[0].ColumnName == "INTERNAL_SERVER_ERROR" &&
        //        dataSet.Tables[0].Rows.Count == 1
        //    )
        //    {
        //        string message = Convert.ToString(dataSet.Tables[0].Rows[0]["INTERNAL_SERVER_ERROR"]);
        //        throw new Exception(message);
        //    }

        //    SterlingCasContext.UpdateDataSetValuesForDateTimeColumn(dataSet);
        //    return dataSet;
        //}

        /// <summary>
        /// Query fabric repair tasks for each fabric cluster. Filters to only tasks which have executed since to the time specified.
        /// </summary>
        /// <param name="upgradeActiveTimeUtc">Time filter to return only jobs that have executed since this time.</param>
        /// <param name="fabricClusterNames">List of fabric rings to query.</param>
        /// <returns>A DataTable containing the FabricRepairTasks for each ring.</returns>
        //public DataTable QueryFabricRepairTasks(DateTime upgradeActiveTimeUtc, IList<string> fabricClusterNames)
        //{
        //    // Initialize the dataset object
        //    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(ClientCluster.FabricRepairTask));
        //    DataTable table = new DataTable();

        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    }

        //    // Query each ring for the fabric repair tasks
        //    table.Columns.Add("Name", typeof (string));
        //    foreach (var targetCluster in fabricClusterNames)
        //    {
        //        var uri = new Uri(string.Format(CultureInfo.InvariantCulture,
        //            "{0}('{1}')/FabricRepairTasks?$filter=(CompletedTimestamp%20eq%20null)%20or%20(CompletedTimestamp%20gt%20DateTime'{2}')",
        //            this.Client.FabricClusters.RequestUri,
        //            targetCluster,
        //            upgradeActiveTimeUtc.ToString("yyyy-MM-ddTHH:mm:ssZ")));

        //        // Return each task as a generic DataTable row object
        //        foreach (var repairTask in this.Client.Execute<ClientCluster.FabricRepairTask>(uri, "GET", true))
        //        {
        //            if (!repairTask.ClaimedTimestamp.HasValue)
        //            {
        //                continue;
        //            }

        //            object[] values = new object[props.Count + 1];
        //            for (int i = 0; i < values.Length - 1; i++)
        //            {
        //                values[i] = props[i].GetValue(repairTask);
        //            }

        //            values[values.Length - 1] = targetCluster;
        //            table.Rows.Add(values);
        //        }
        //    }

        //    return table;
        //}

        /// <summary>
        /// Gets the cas version from a given cluster
        /// </summary>
        /// <param name="host">Cluster Address</param>
        /// <returns>The version of cas running on the given cluster</returns>
        public static Version GetCasVersion(string host)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(string.Format("https://{0}:9333/v2/ClusterAdministration.svc/Cluster/$metadata", host));

            try
            {
                var resp = request.GetResponse();
                return ExtractVersionFromHeader(resp);
            }
            catch (WebException ex)
            {
                var resp = ex.Response;
                return ExtractVersionFromHeader(resp);
            }
        }

        private static ClientCluster.Cluster CreateClient(string clusterAddress, X509Certificate2 certificate)
        {
            string host = String.Format("management.{0}", clusterAddress);
            ClientCluster.Cluster client;
            if (certificate != null)
            {

                Uri uri = new Uri("https://" + host + ":9333/v2/TrustedClusterAdministration.svc/Cluster/");
                client = ClientCluster.Cluster.CreateCertificateClient(uri, false, certificate);
                Trace.TraceInformation("Use Name:{0} with thumbprint: {1} to authentiate CMS", certificate.SubjectName.Name, certificate.Thumbprint);
            }
            else
            {
                client = ClientCluster.Cluster.CreateTokenClient(host, GetAuthenticationToken);
            }

            return client;
        }

        /// <summary>
        /// Extracts the version header from a webresponse
        /// </summary>
        /// <param name="resp">The response to extract from</param>
        /// <returns>The cas-version header</returns>
        private static Version ExtractVersionFromHeader(WebResponse resp)
        {
            if (resp != null)
            {
                if (resp.Headers != null)
                {
                    if (resp.Headers.AllKeys.Contains("cas-version"))
                    {
                        return new Version(resp.Headers["cas-version"]);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get SAML token from Corp STS. The configuration is discovered from the host service.
        /// </summary>
        /// <returns>SAML token</returns>
        private static string GetAuthenticationToken(HttpWebRequest request)
        {
            var token = string.Empty;
            ADFSAuthenticationClient client = null;

            var multiconfig = new MultistepAuthenticationConfig();
            if (multiconfig.ReadSettings(request.RequestUri.ToString()))
            {
                // new format
                if (multiconfig.AuthenticationEnabled)
                {
                    client = new ADFSAuthenticationClient(multiconfig);
                }
            }

            else
            {
                // Old format
                // Will remove once CAS server is updated
                var config = new SterlingAuthorizationConfig();
                config.ReadSettings(request.RequestUri.ToString());
                if (config.EnabledADFSAuth)
                {
                    client = new ADFSAuthenticationClient(config.RelyingPartyUri, config.StsEndpoint, config.CacheTokenSeconds);
                }
            }

            if (client != null)
            {
                token = client.GetSamlToken();
            }

            return token;
        }

        /// <summary>
        /// Updates the date set values for date time column to Utc.
        /// The enum DataSetDateTime  controls serialization format for datetime at http://msdn.microsoft.com/en-us/library/ms135436.aspx, in the future, we can fix in derialzation code
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        private static void UpdateDataSetValuesForDateTimeColumn(DataSet dataSet)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                UpdateDataTableValuesForDateTimeColumn(dataTable);
            }

            dataSet.AcceptChanges();
        }

        /// <summary>
        /// Updates the date set values for date time column.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        private static void UpdateDataTableValuesForDateTimeColumn(DataTable dataTable)
        {
            List<DataColumn> dateTimeColumns = new List<DataColumn>();
            // get a list of columns which DateTimeMode is local or unspecfiedlocal
            //
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == System.Type.GetType("System.DateTime"))
                {
                    if (column.DateTimeMode == DataSetDateTime.UnspecifiedLocal || column.DateTimeMode == DataSetDateTime.Local)
                    {
                        dateTimeColumns.Add(column);
                    }
                }
            }
            if (dateTimeColumns.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn column in dateTimeColumns)
                    {
                        if (row[column] != DBNull.Value)
                        {
                            row[column] = ((DateTime)row[column]).ToUniversalTime();
                        }
                    }
                }
            }
        }

        public static string GetHtmlTable(DataSet dataSet, DateTime timestamp)
        {
            string messageBody = String.Format("<font>The following records have been taken on {0}: </font><br><br>", timestamp);

            if (dataSet.Tables[0].Rows.Count == 0)
                return messageBody;

            foreach (DataTable table in dataSet.Tables)
            {
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                foreach (DataColumn column in table.Columns)
                {
                    messageBody += htmlTdStart + column.ColumnName + htmlTdEnd;
                }

                messageBody += htmlHeaderRowEnd;

                foreach (DataRow row in table.Rows)
                {
                    messageBody = messageBody + htmlTrStart;
                    foreach (DataColumn column in table.Columns)
                    {
                        object item = row[column];
                        messageBody = messageBody + htmlTdStart + item.ToString() + htmlTdEnd;
                    }
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
            }

            return messageBody;
        }

        public void ExecuteCasAction(string casActionName, List<OperationParameter> parameters)
        {
            Uri uri = new Uri(string.Format("{0}/FabricClusters('{1}')/{2}", Client.BaseUri, ControlFabricCluster.Name, casActionName));

            Client.Execute(uri, "POST", parameters.ToArray());
        }
    }
}

