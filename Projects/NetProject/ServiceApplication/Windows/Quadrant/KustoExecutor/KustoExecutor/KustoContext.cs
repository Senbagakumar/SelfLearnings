using Kusto.Data;
using Microsoft.SqlServer.Tools.SterlingKustoClientV2;
using Microsoft.SqlServer.Tools.SterlingKustoCommon;
using Microsoft.SqlServer.Tools.SterlingKustoCommon.KustoClient;
using Microsoft.SqlServer.Tools.SterlingKustoCommon.KustoLibUtil;
using Microsoft.SqlServer.Tools.SterlingKustoLibUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace KustoExecutor
{
    public class KustoContextSettings
    {
        private int _numberOfRetries = SterlingKustoConstants.RetryCount;

        private int _retryBackoffMultiplier = SterlingKustoConstants.RetryBackoffMultiplier;

        private int _retryIntervalInSeconds = SterlingKustoConstants.RetryIntervalInSeconds;

        /// <summary>
        /// Short name of the cluster to query.
        /// Examples: "ProdCus1a", "McprCnea1a", "ProdEus1a", "BfprDeCe1a"
        /// </summary>
        public string ClusterShortName { get; set; }

        /// <summary>
        /// Name of the tenant or control ring name to filter by.
        /// Examples: "tr1.chinanorth1-a.worker.database.chinacloudapi.cn", "cr2.eastasia1-a.control.database.windows.net"
        /// </summary>
        public string ClusterNameFilter { get; set; }

        /// <summary>
        /// Any extra query "where" filter to apply. Do not incldue a prepending "|".
        /// Examples: "where NodeName == 'DB.46'", "where package != 'xdbhost'"
        /// </summary>
        public string ExtraFilter { get; set; }

        /// <summary>
        /// Number of times to retry before throwing. Defaults to 3.
        /// </summary>
        public int NumberOfRetries
        {
            get { return _numberOfRetries; }
            set { _numberOfRetries = value; }
        }

        /// <summary>
        /// In order to prevent overloading the cluster with a lot of retries 
        /// </summary>
        public int MaxNumberOfRetries
        {
            get { return 10; }
        }

        /// <summary>
        /// Multiplier used to increase the time between retries. Defaults to 2.
        /// </summary>
        public int RetryBackoffMultiplier
        {
            get { return _retryBackoffMultiplier; }
            set { _retryBackoffMultiplier = value; }
        }

        /// <summary>
        /// Initial time interval to wait between retries. Defaults to 2.
        /// </summary>
        public int RetryIntervalInSeconds
        {
            get { return _retryIntervalInSeconds; }
            set { _retryIntervalInSeconds = value; }
        }

        /// <summary>
        /// If true, federated auth (user credentials) are used. If false, an app key
        /// or certificate is used. Defaults to false.
        /// </summary>
        public bool UseFederatedAuth { get; set; }

        /// <summary>
        /// If true, compression is used for all ingestion commands. Defaults to false.
        /// </summary>
        public bool UseCompressionToIngestData { get; set; }
    }
    public class KustoContext
    {
        private KustoClientV2 kustoClient;
        private readonly string IcmClusterConnectionString;

        public KustoContext()
        {
            IcmClusterConnectionString = "Data Source=https://icmcluster.kusto.windows.net;Initial Catalog=IcmDataWarehouse;AAD Federated Security=True;dSTS Federated Security=False";
            //IcmClusterConnectionString = "Data Source=https://sqlazureuk2.kustomfa.windows.net:443;Initial Catalog=sqlazure1;AAD Federated Security=True;dSTS Federated Security=False";
            SetKustoClient();
        }

        private void SetKustoClient()
        {
            bool bSuccess = false;
            int iTryCount = 0;
            while (!bSuccess)
            {
                try
                {
                    kustoClient = new KustoClientV2(IcmClusterConnectionString, SterlingKustoUtilProvider.Default);
                    //kustoClusterEndPoint = "Data Source=https://sqlazureuk2.kustomfa.windows.net:443";
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    if (++iTryCount > 3)
                    {
                        Trace.TraceInformation("Create kusto client failed! Exception: " + e);

                        throw e;
                    }
                    else
                    {
                        Trace.TraceInformation("Try again to create the kusto client! Exception: " + e);
                        Thread.Sleep(15 * 1000);
                    }
                }
            }            
        }

        public DataTable ExecuteQuery(string query)
        {
            KustoRequest request = new KustoRequest(new[] { query }, new List<string>() { "" }, "TIMESTAMP", null, null);
            request.NumberOfRetries = 3;
            request.RetryBackoffMultiplier = SterlingKustoConstants.RetryBackoffMultiplier;
            request.RetryIntervalInSeconds = SterlingKustoConstants.RetryIntervalInSeconds;

            request.RequestTimeout = TimeSpan.FromMinutes(4);
            var response = kustoClient.QueryTables(request);
            return response.ResultTable;
        }
      
    }
}
