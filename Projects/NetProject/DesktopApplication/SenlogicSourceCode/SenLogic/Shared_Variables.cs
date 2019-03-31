using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.Drawing.Text;

namespace SenLogic
{
    public class Shared_Variables
    {
        public static DataSet dtAcessSetting;
        public static CultureInfo cultureinfo = new CultureInfo("ta-IN");
        public static DBEngine objDB=null;
        public static PrivateFontCollection pf = null;
        public static string TimeFormat = "HH:mm";
        public static string DateFormat = "dd/MM/yyyy";
        public static string TimeFormatWithSeconds = "HH:mm:ss";
        public static string TimeSpanFormat = @"hh\:mm";
        public static string TimeSpanFormatWithSeconds = @"hh\:mm\:ss";
        public const int FontSize = 10;
        public const string dotLine = "-----------------------------------------------------------------------------------------";

        public static string printPreviewEnable
        {
            get
            {
               return  ConfigurationManager.AppSettings["PrintPreviewIsEnable"].ToString();
            }
        }

        private static string GetFontName { get { return ConfigurationManager.AppSettings["FontName"].ToString(); } }

        public static int AdminCount
        {
            get;
            set;
        }

        public static string GetConnectionString
        {
           get;
           set;
        }
        public static int UserID
        {
            get;
            set;
        }
        public static string User
        {
            get;
            set;
        }
        public static Font GetFont
        { 
           get
            {
                return new Font(GetFontName, FontSize, FontStyle.Regular); 
            }
                
        }

        public static int SpaceValue
        {
            get
            {
                return 15;
            }
        }

        public static int SpaceValueForFinalCopy
        {
            get
            {
                return 20;
            }
        }

        public static Font GetFontForHeader
        {
            get
            {

                return new Font(GetFontName, FontSize+1);
            }

        }

        public static Font GetArialFont
        {
            get
            {
                return new Font(GetFontName, FontSize+2, FontStyle.Bold);
            }

        }

        public static int LineNumber
        {
            get;
            set;
        }
        public static int DelayTiming
        {
            get
            {
              return Convert.ToInt32(ConfigurationSettings.AppSettings.Get("DelayTiming")); 
            }
          
        }

        public static string UserStatus
        {
            get;
            set;
        }

        public static string UserRole
        {
            get;
            set;
        }

        public static string Get_Access_Permission(string pageName)
        {
            if (dtAcessSetting != null)
            {
                foreach (DataRow dr in dtAcessSetting.Tables["Acess"].Select("Page_Name='" + pageName + "'"))
                {
                    return dr["Access_Permission"].ToString();
                }
            }
            return "D";
        }
        public static DBEngine Engine
        {
            get
            {
                if (objDB == null)
                    objDB = new DBEngine();
                return objDB;           
            }
        }
        public static bool GeneralSettingChanged
        {
            get;
            set;
        }
        public static bool InstrumentSettingChanged
        {
            get;
            set;
        }
        public static bool SettingNeedLoad
        {
            get;
            set;
        }

      
    }
}
