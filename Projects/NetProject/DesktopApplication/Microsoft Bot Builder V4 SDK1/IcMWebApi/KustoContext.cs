using Microsoft.SqlServer.Tools.SterlingKustoClientV2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace KustoExecutor
{
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
