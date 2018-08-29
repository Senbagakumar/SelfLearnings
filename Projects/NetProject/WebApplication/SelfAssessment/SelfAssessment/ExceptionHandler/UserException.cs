using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.ExceptionHandler
{
    public class UserException : Exception
    {       
        public UserException(string message) : base(message)
        {
            
        }
    }

    public class ValidationInformation
    {
        public List<string> ErrorMessages { get; set; }
        public bool IsSuccess { get; set; }
    }
}