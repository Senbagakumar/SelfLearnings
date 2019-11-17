using Microsoft.SqlServer.Tools.SterlingKustoClientV2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
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
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string>();
            try
            {
                var kustoContext = new KustoContext();
                //string kustoQuery = "MonSqlVm | where ClusterName == 'cr1.uksouth1-a.control.database.windows.net' | top 10 by AppName asc";
                string kustoQuery = string.Format(@"Notifications | where PrimaryTargetType == 'TEAMID' | where PrimaryTarget in (38526, 38525, 37536, 38523, 38527, 38524, 36369, 32529, 10979, 22315, 10613, 11230, 36392, 36391)
                                         | where AcknowledgeDate >= datetime('2019-02-25')  and AcknowledgeDate <= datetime('2019-02-26')
                                         | summarize by IncidentId
                                         | top 10 by IncidentId");
                var dt = kustoContext.ExecuteQuery(kustoQuery);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(row[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
