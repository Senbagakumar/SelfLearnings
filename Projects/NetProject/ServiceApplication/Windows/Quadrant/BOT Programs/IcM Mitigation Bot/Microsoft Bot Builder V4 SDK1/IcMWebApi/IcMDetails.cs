using IcMWebApi.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace IcMWebApi
{
    public class IcMDetails
    {
        private readonly string IcMId;
        private readonly string IcMTsgId;
        private readonly string botConnectionString;
        public IcMDetails(string icmId, string icMTsgId)
        {
            IcMId = icmId;
            IcMTsgId = icMTsgId;
            botConnectionString = "Data Source=healthproperties.database.windows.net,1433;Initial Catalog=HealthProperties;Persist Security Info=True;User ID=reader;Password=SQL@zure";
        }

        public IcM GetClusterShortName()
        {
            string query = string.Format($@"select top 1 Title,Cluster,Details,IsExpired,MonitorId from incidents where ICMIncidentId={IcMId}");
            var icmDeatails = new IcM();
            string details = string.Empty;
            using (var sqlConnection = new SqlConnection(this.botConnectionString))
            { 
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = query;
                    sqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        icmDeatails.ClusterName = reader["Cluster"].ToString();
                        icmDeatails.IcMTitle = reader["Title"].ToString();
                        details = reader["Details"].ToString();
                        icmDeatails.IsExpired = Convert.ToBoolean(reader["IsExpired"].ToString());
                        icmDeatails.RunnerName = reader["MonitorId"].ToString();
                    }
                    sqlConnection.Close();
                }
            }

            if (!string.IsNullOrWhiteSpace(icmDeatails.IcMTitle) && icmDeatails.IcMTitle.Contains(IcMTsgId))
                icmDeatails.IsValidIcM = true;
            else
                icmDeatails.IsValidIcM = false;

            if (icmDeatails.IcMTitle.Trim().ToLower().Contains("ppbot-049"))
                return icmDeatails;

            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(details);
            string expandoAppName = expando.ApplicationName;
            icmDeatails.ApplicationName= expandoAppName.Split('/')[2];
            icmDeatails.RingName = expando.TenantRingName;
            return icmDeatails;
        }
    }
}