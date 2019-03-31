using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SenLogic
{
   public class Shared_Class
    {
       public static DataSet LoadPortingValue()
       {
           var db = new DBEngine();
           var dtPorting = new DataSet();
           db.ExecuteQuery("Select Commtrack1, commBaud1,IsStaticDynamic from ScreenCommon", null, dtPorting, "Porting");          
           return dtPorting;
          

       }

    }
   public class BaseEntity
   {
       public int Id { get; set;}
       public string Name { get; set; }
   }
}
