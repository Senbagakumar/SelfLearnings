using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
namespace SenLogic
{
    public static class Shared_Functions
    {
        public static void Show_Message(string strMsg)
        {
            MessageBox.Show(strMsg, "SenLogic");
        }

      
        public static void Logged_StartStop(string userName, string type)
        {
            string path = Application.StartupPath + "\\Log";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filepath = Application.StartupPath + "\\Log\\LogOn" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt";
            if (!File.Exists(filepath))
            {
                FileStream fs = File.Create(filepath);
                fs.Close();
            }
            File.AppendAllText(filepath, System.Environment.NewLine + type + " On:" + DateTime.Now.ToString() + " By:" + userName);

        }
        public static void Logged_ErrorMessage(string Message)
        {
            string path = Application.StartupPath + "\\Log";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filepath = Application.StartupPath + "\\Log\\ErrorLogOn" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt";
            if (!File.Exists(filepath))
            {
                FileStream fs = File.Create(filepath);
                fs.Close();
            }
            
               File.AppendAllText(filepath, System.Environment.NewLine + " On:" + DateTime.Now.ToString() + " Message:" + Message);
            

        }
    }

}
