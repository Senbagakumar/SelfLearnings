using FileDownload.Controllers;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
        public static string CreateCsv(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append(column.ColumnName);
                sb.Append(",");
            }
            sb.AppendLine();
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append(row[column]);
                    sb.Append(",");
                }
                sb.AppendLine();
            }
            return sb.ToString();

        }
        public static void CreatePdf(string pdfFilename, DataTable dt)
        {
            PDFform pdfForm = new PDFform(dt, pdfFilename);

            // Create a MigraDoc document
            Document document = pdfForm.CreateDocument();
            document.UseCmykColor = true;

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

            // Set the MigraDoc document
            pdfRenderer.Document = document;


            // Create the PDF document
            pdfRenderer.RenderDocument();

            pdfRenderer.Save(pdfFilename);
        }

        public static DataTable GetReport(int userId,string level=null)
        {
            var groupQuizManager = new GroupQuizManager(userId);
            var listOfQuestions = groupQuizManager.GetAllQuestions(level);
            groupQuizManager.AllQuestions = listOfQuestions;
            var dt = groupQuizManager.ExportPdf();
            return dt;
        }

        public static void DeleteOldFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.Contains(".csv") || file.Extension.Contains(".pdf"))
                {
                    var olddate = file.Name.Replace(file.Extension, "");
                    DateTime oldDate = DateTime.ParseExact(olddate, "ddMMyyyyHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    if (oldDate <= DateTime.Now)
                        file.Delete();
                }
            }
        }
        
    }
}