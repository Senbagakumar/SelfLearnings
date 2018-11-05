using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Business
{
    public class Utilities
    {
        public const string AssessmentTypeLevel1= "Level 1";
        public const string AssessmentTypeLevel2 = "Level 2";
        public const string AssessmentTypeLevel3 = "Level 3";
        public const string AssessmentPendingStatus = "Pending";
        public const string AssessmentCompletedStatus = "Completed";
        public const string UserId = "UserId";
        public const string RedirectToLogin = "~/User/Login";
        public const string DefaultSelectionValue = "-- Select --";
        public const string Success = "Success";
        public const string Failiure = "Failiure";
        public const string Usermail = "UserMail";
        public const string RedirectToUser = "~/ManageUser/Index";
        public const string RedirectToAdmin = "~/Admin/Index";
        public const string Small = "Small";
        public const string Large = "Large";
        public const string OperatingUnit = "Operating Unit";
        public const string Others = "OTHERS";
        public const string OthersValue = "1000";
        public const char SplitValue = '-';
        public const string QuestionCode = "QCCode";
        public const string All = "-- All --";
        public const string Group6 = "Group6";
        public const string Group9 = "Group9";
        public const string SectorAll = "ALL";
        public const string SectorValue = "1001";

        public static int CalculateScoreByAns(int answerId)
        {
            int score = 0;
            switch (answerId)
            {
                case 1:
                    score = 5;
                    break;
                case 2:
                    score = 25;
                    break;
                case 3:
                    score = 50;
                    break;
                case 4:
                    score = 75;
                    break;
                case 5:
                    score = 95;
                    break;
                default:
                    score = 5;
                    break;

            }
            return score;
        }
    }
}