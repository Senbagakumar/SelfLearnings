using KustoExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IcMWebApi.Controllers
{
    public class OutPut
    {
        //IncidentId, AcknowledgeContactAlias, AcknowledgeDate
        public string IncidentId { get; set; }
        public string Title { get; set; }
    }
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get(int severity, string owningTeamName, string status)
        {
            var list = new List<OutPut>();
            try
            {
                 var kustoContext = new KustoContext();
                //string kustoQuery = "MonSqlVm | where ClusterName == 'cr1.uksouth1-a.control.database.windows.net' | top 10 by AppName asc";
                //string kustoQuery = string.Format(@"Notifications | where PrimaryTargetType == 'TEAMID' | where PrimaryTarget in (38526, 38525, 37536, 38523, 38527, 38524, 36369, 32529, 10979, 22315, 10613, 11230, 36392, 36391)
                //                         | where AcknowledgeDate >= datetime('2019-02-25')  and AcknowledgeDate <= datetime('2019-02-26')
                //                         | project IncidentId, AcknowledgeContactAlias, AcknowledgeDate  Status=='ACTIVE' MITIGATED
                //                         | top 10 by IncidentId");   or OwningTeamName contains '{2}' or OwningTeamName contains '{3}'  "GeoDR", "Backup/Restore" SQLDBperfv-queue

                string kustoQuery =string.Format(@"Incidents | where OwningTenantName == '{0}' 
                                                | where CreateDate > datetime('{1}') | where Severity == {2}
                                                | where OwningTeamName contains '{3}' | where Status == '{4}'
                                                | project IncidentId, Title", "Azure SQL DB", DateTime.Now.AddHours(-1), severity, owningTeamName, status);
                var dt = kustoContext.ExecuteQuery(kustoQuery);

                if (dt != null && dt.Rows.Count > 0)
                {                    
                    foreach (DataRow row in dt.Rows)
                    {
                        var oput = new OutPut();
                        oput.IncidentId = row[0].ToString();
                        oput.Title = row[1].ToString();
                        list.Add(oput);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            ///
            return Ok(list);
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
