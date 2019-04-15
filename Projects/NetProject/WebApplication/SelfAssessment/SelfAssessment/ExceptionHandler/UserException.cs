using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.ExceptionHandler
{
    public class UserException
    {
        
        public static void LogException(Exception ex)
        {
            var errorDetails = new { errorMsg=ex.Message, InnerException=ex.InnerException?.Message, source=ex.Source, stackTrace=ex.StackTrace, data=ex.Data, error=ex.ToString()  };
            using (Repository<Log> repository = new Repository<Log>())
            {
                repository.Create(new Log() { Type = Utilities.Error, Details = errorDetails.ToString(), CreatedDate=DateTime.UtcNow });
                repository.SaveChanges();
            }
        }

        public static void LogInformation(string Info)
        {
            using (Repository<Log> repository = new Repository<Log>())
            {
                repository.Create(new Log() { Type = Utilities.Information, Details = Info, CreatedDate=DateTime.UtcNow });
                repository.SaveChanges();
            }
        }
    }

    public class ValidationInformation
    {
        public List<string> ErrorMessages { get; set; }
        public bool IsSuccess { get; set; }
    }
}