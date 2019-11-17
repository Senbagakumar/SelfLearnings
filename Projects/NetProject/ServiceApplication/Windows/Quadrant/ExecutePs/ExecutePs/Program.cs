using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ExecutePs
{
    class Program
    {

        public void CSVtoDataTable()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=AuditLog;Integrated Security=True");
            string filepath = @"C:\Users\v-sesiga\Desktop\PBIAudiLogs\PBIAuditLogs\20190404.csv";
            filepath = @"C:\Users\v-sesiga\Desktop\PBIAudiLogs\Test.csv";
            StreamReader sr = new StreamReader(filepath);
            string line = sr.ReadLine();
            string[] value = line.Split(',');
            DataTable dt = new DataTable();
            DataRow row;
            foreach (string dc in value)
            {
                dt.Columns.Add(new DataColumn(dc));
            }

            while (!sr.EndOfStream)
            {
                value = sr.ReadLine().Split(',');
                if (value.Length == dt.Columns.Count)
                {
                    row = dt.NewRow();
                    row.ItemArray = value;
                    dt.Rows.Add(row);
                }
            }
            SqlBulkCopy bc = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock);
            bc.DestinationTableName = "CSVTest";
            bc.BatchSize = dt.Rows.Count;
            con.Open();
            bc.WriteToServer(dt);
            bc.Close();
            con.Close();
        }
        static void Main(string[] args)
        {
            try
            {
                PowerShellHost ps = new PowerShellHost();
                ps.ExecuteScriptFile("PBIAuditLogExport.ps1");
                //new Program().CSVtoDataTable();
            }
            catch(Exception ex)
            {
               
            }
        }
    }
}
