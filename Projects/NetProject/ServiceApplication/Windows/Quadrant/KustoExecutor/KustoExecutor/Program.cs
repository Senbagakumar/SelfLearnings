using System;
using System.Collections.Generic;
using System.Text;

namespace KustoExecutor
{
    class Program
    {
        static void Main(string[] args)
        {

            var kustoContext = new KustoContext();
            //string kustoQuery = "MonSqlVm | where ClusterName == 'cr1.uksouth1-a.control.database.windows.net' | top 10 by AppName asc";
            string kustoQuery= string.Format(@"Notifications | where PrimaryTargetType == 'TEAMID' | where PrimaryTarget in (38526, 38525, 37536, 38523, 38527, 38524, 36369, 32529, 10979, 22315, 10613, 11230, 36392, 36391)
                                             | where AcknowledgeDate >= datetime('2019-02-25')  and AcknowledgeDate <= datetime('2019-02-26')
                                             | summarize by IncidentId
                                             | top 10 by IncidentId");
            var dt=kustoContext.ExecuteQuery(kustoQuery);
            Console.WriteLine($"Count Of Records={dt.Rows.Count}");
            Console.Read();
        }
    }
}
