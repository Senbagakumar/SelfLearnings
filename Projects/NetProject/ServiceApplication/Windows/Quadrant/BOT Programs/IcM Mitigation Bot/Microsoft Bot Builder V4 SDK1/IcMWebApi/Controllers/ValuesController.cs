using IcMWebApi.Utilities;
using KustoExecutor;
using RunnerCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace IcMWebApi.Controllers
{
    public class OutPut
    {
        //IncidentId, AcknowledgeContactAlias, AcknowledgeDate
        public string IncidentId { get; set; }
        public string Title { get; set; }
    }

    public class IcM
    {
        public bool IsExpired { get; set; }
        public string ClusterName { get; set; }
        public string RegionName { get; set; }
        public string ApplicationName { get; set; }
        public bool IsValidIcM { get; set; }
        public string IcMTitle { get; set; }
        public string RingName { get; set; }
        public string RunnerName { get; set; }
    }
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get(string IcMId, string IcMTsgId)
        {
            string command = string.Empty;
            var list = new List<OutPut>();
            try
            {

                DataTable dt = null;
                IcM icmdetails = new IcMDetails(IcMId, IcMTsgId).GetClusterShortName();

                if (icmdetails.IsValidIcM)
                {
                    if (icmdetails.RunnerName.Contains("AppBelowTargetReplicaCount"))
                        command = ExecuteCMSQuery(icmdetails);
                    else if (icmdetails.RunnerName.Contains("PerfHighUsageNode"))
                    {
                        dt = ExecuteKustoQuery(icmdetails.ClusterName);
                        if (dt != null && dt.Rows.Count > 0)
                            command="Execute the below Command in DA Console";
                        else
                            command = "It was transient, so mark it as mitigated.";
                    }
                }
                else
                {
                    command = "Invalid TSG ID aginst the IcM";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            ///
            return Ok(command);
        }

        private DataTable ExecuteKustoQuery(string shortName)
        {
            var kustoContext = new KustoContext(shortName);

            string kustoQuery = string.Format(@"MonRgLoad
                                                | where originalEventTimestampTo > ago(30m)
                                                | where event == ""cpu_total_load_cap_per_core"" and NodeName startswith ""DB"" and (node_sku contains ""G3"" or node_sku contains ""G4"" or node_sku contains ""GZ"" or node_sku contains ""G5"")
                                                | project TIMESTAMP, node_sku, ClusterName , NodeName, node_user_cpu_usage, node_user_cpu_allocation
                                                | extend node_cpu_cap = iff(node_sku contains ""G3"", 1400, iff(node_sku contains ""G4"", 2200, iff(node_sku contains ""G5"", 7200, 2800)))
                                                | extend is_violated = iff(node_user_cpu_usage * 1.0 / node_cpu_cap > 0.8, 1, 0)
                                                | summarize arg_max(TIMESTAMP, node_user_cpu_allocation), node_cpu_cap = any(node_cpu_cap), peak_cpu_usage = max(node_user_cpu_usage), node_sku = any(node_sku), cnt = count(), violation_cnt = sum(is_violated) by ClusterName, NodeName
                                                | extend overbook_ratio = node_user_cpu_allocation * 1.0 / node_cpu_cap
                                                | where violation_cnt * 1.0 / cnt > 0.9 
                                                | join kind = leftouter (
	                                                MonRgLoad
	                                                | where TIMESTAMP > ago(20m)
	                                                | where event == ""application_load""
	                                                | summarize max(app_cpu_load) by ClusterName, NodeName
                                                ) on ClusterName, NodeName
                                                | where overbook_ratio > 1.05 or (overbook_ratio <= 1.05 and max_app_cpu_load * 1.0 / node_cpu_cap < 0.3)
                                                | project ClusterName, NodeName, node_sku, overbook_ratio, peak_cpu_usage, node_cpu_cap");

            var dt = kustoContext.ExecuteQuery(kustoQuery);
            return dt;
        }

        private string ExecuteCMSQuery(IcM icm)
        {
            //tr1.eastus1-a.worker.database.windows.net  ad4356228452
          
            var appName =icm.ApplicationName;
            var kustoContext = new KustoContext(icm.ClusterName);
            string query = Queries.AppBelowQuery2;
            //query = string.Format(query, "tr1.eastus1-a.worker.database.windows.net", "ad4356228452");
            string result= "There is no active instance";

            var kutoQuery = query.Replace("Dynamic_AppName", icm.ApplicationName); //string.Format(query, icm.RingName, appName);
            kutoQuery = kutoQuery.Replace("Dynamic_Source_Ring_Name", icm.RingName);
            var dt1 = kustoContext.ExecuteQuery(kutoQuery);
            if (dt1.Rows.Count <= 0)
            {
                if (icm.ApplicationName.Contains("v-"))
                    return $"{icm.ApplicationName} most of the v- appNames created by Backup/Restore team, Please check with them and route it to same";
                else
                    return $"{icm.ApplicationName} is not able to move, please verify this app Name";
            }

            var command = dt1.Rows[0]["command"].ToString(); //Dynamic_Target_Ring_Name
            string slotype = dt1.Rows[0]["slo"].ToString();


            Regex rgx = new Regex(@"^cr\d+\.");
            var runnerName = "AppBelowTargetReplicaCount";
            var shortRunnerName = runnerName.Substring(runnerName.IndexOf('.', runnerName.IndexOf('.') + 1) + 1);
            var ControlRingAddress = GetControlRingAddress(icm.ClusterName);//"cr3.westus2-a.control.database.windows.net";
            var sterlingCasContext = new SterlingCasContext(shortRunnerName, rgx.Replace(ControlRingAddress, string.Empty), "", false);

            if (!string.IsNullOrEmpty(slotype) && slotype.ToLower().Contains("gen4"))
                slotype = "SQLG4VM";
            else
                slotype = "SQLG5";

            var cmsQuery = string.Format(Queries.AppBelowQuery1, slotype);
            DataTable dt = sterlingCasContext.QueryCms(cmsQuery).Tables[0];
            if (dt.Rows.Count > 0)
            {
                result = command.Replace("Dynamic_Target_Ring_Name", dt.Rows[0]["target_tenant_ring"].ToString());
            }
            return result;
        }

        private string GetControlRingAddress(string shortName)
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("ProdWus2a", "cr1.westus2-a.control.database.windows.net");
            dictionary.Add("ProdWus1a", "cr1.westus1-a.control.database.windows.net");
            dictionary.Add("ProdWeu1a", "cr1.westeurope1-a.control.database.windows.net");
            dictionary.Add("ProdWCus1a", "cr1.westcentralus1-a.control.database.windows.net");
            dictionary.Add("Produseue2a", "cr1.useuapeast2-a.control.database.windows.net");
            dictionary.Add("Produseuc1a", "cr1.useuapcentral1-a.control.database.windows.net");
            dictionary.Add("ProdUkWe1a", "cr1.ukwest1-a.control.database.windows.net");
            dictionary.Add("ProdUkSo2a", "cr1.uksouth2-a.control.database.windows.net");
            dictionary.Add("ProdUkSo1a", "cr1.uksouth1-a.control.database.windows.net");
            dictionary.Add("ProdUkNo1a", "cr1.uknorth1-a.control.database.windows.net");
            dictionary.Add("ProdUaNo1a", "cr1.uaenorth1-a.control.database.windows.net");
            dictionary.Add("ProdUaCe1a", "cr1.uaecentral1-a.control.database.windows.net");
            dictionary.Add("ProdSEas1a", "cr1.southeastasia1-a.control.database.windows.net");
            dictionary.Add("ProdSCus1a", "cr1.southcentralus1-a.control.database.windows.net");
            dictionary.Add("ProdSaWe1a", "cr1.southafricawest1-a.control.database.windows.net");
            dictionary.Add("ProdSaNo1a", "cr1.southafricanorth1-a.control.database.windows.net");
            dictionary.Add("ProdNeu1a", "cr1.northeurope1-a.control.database.windows.net");
            dictionary.Add("ProdNCus1a", "cr1.northcentralus1-a.control.database.windows.net");
            dictionary.Add("ProdKoSo1a", "cr1.koreasouth1-a.control.database.windows.net");
            dictionary.Add("ProdKoCe1a", "cr1.koreacentral1-a.control.database.windows.net");
            dictionary.Add("ProdJawe1a", "cr1.japanwest1-a.control.database.windows.net");
            dictionary.Add("ProdJaea1a", "cr1.japaneast1-a.control.database.windows.net");
            dictionary.Add("ProdInWe1a", "cr1.indiawest1-a.control.database.windows.net");
            dictionary.Add("ProdInSo1a", "cr1.indiasouth1-a.control.database.windows.net");
            dictionary.Add("ProdInCe1a", "cr1.indiacentral1-a.control.database.windows.net");
            dictionary.Add("Prodfrancesa", "cr1.francesouth1-a.control.database.windows.net");
            dictionary.Add("Prodfranceca", "cr1.francecentral1-a.control.database.windows.net");
            dictionary.Add("ProdEus2a", "cr1.eastus2-a.control.database.windows.net");
            dictionary.Add("ProdEus1a", "cr1.eastus1-a.control.database.windows.net");
            dictionary.Add("ProdEas1a", "cr1.eastasia1-a.control.database.windows.net");
            dictionary.Add("ProdCus1a", "cr1.centralus1-a.control.database.windows.net");
            dictionary.Add("ProdCaEa1a", "cr1.canadaeast1-a.control.database.windows.net");
            dictionary.Add("ProdCaCe1a", "cr1.canadacentral1-a.control.database.windows.net");
            dictionary.Add("ProdBrso1a", "cr1.brazilsouth1-a.control.database.windows.net");
            dictionary.Add("ProdAuse1a", "cr2.australiasoutheast1-a.control.database.windows.net");
            dictionary.Add("ProdAuea1a", "cr2.australiaeast1-a.control.database.windows.net");
            dictionary.Add("ProdAuCe2a", "cr1.australiacentral2-a.control.database.windows.net");
            dictionary.Add("ProdAuCe1a", "cr1.australiacentral1-a.control.database.windows.net");

            if (dictionary.Keys.Contains(shortName))
                return dictionary[shortName];
            else
                return string.Empty;
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
