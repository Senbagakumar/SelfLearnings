using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KustoExecutor;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace TestFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;
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

            if (name == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                name = data?.name;
            }

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
    }
}
