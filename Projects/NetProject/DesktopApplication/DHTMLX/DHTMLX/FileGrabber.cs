using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DHTMLX
{
    internal sealed class TimeCheck
    {
        internal static void Check()
        {
            //Assembly.GetExecutingAssembly();
            //if (TimeCheck.getBuildTime().AddDays(32.0) < DateTime.Now)
            //    throw new Exception("Evaluation period has expired");
        }

        private static DateTime getBuildTime()
        {
            return new DateTime(2000, 1, 1).AddDays((double)Assembly.GetExecutingAssembly().GetName().Version.Build);
        }
    }
    public enum SourceType
    {
        JS,
        CSS,
    }
    public static class JSONHelper
    {
        public static string ToJSON(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static string ToJSON(object obj, int recursionDepth)
        {
            return new JavaScriptSerializer()
            {
                RecursionLimit = recursionDepth
            }.Serialize(obj);
        }
    }
    public static class HtmlHelperExtensions
    {
        public static DHTMLX DHTMLX(this HtmlHelper helper)
        {
            return new DHTMLX();
        }
    }
    public enum FileType
    {
        Local,
        Remote,
        Virtual,
    }
    public class FileGrabber
    {
        protected WebClient client;

        public string GetLocalFile(string path)
        {
            if (File.Exists(path))
                return File.ReadAllText(path);
            throw new Exception("Source file <" + path + "> not found");
        }

        public string GetRemoteFile(string path)
        {
            if (this.client == null)
                this.client = new WebClient();
            Stream stream = this.client.OpenRead(path);
            string end = new StreamReader(stream).ReadToEnd();
            stream.Close();
            return end;
        }
    }
}
