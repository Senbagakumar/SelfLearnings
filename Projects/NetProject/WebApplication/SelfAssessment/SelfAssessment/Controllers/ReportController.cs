using SelfAssessment.Business;
using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class ReportController : Controller
    {

        private int GetUserIdIfIdIsEmpty(int id)
        {
            if (id == 0)
            {
                if (Session[Utilities.UserId] == null || string.IsNullOrEmpty(Session[Utilities.UserId].ToString()))
                    Response.Redirect(Utilities.RedirectToLogin);
                id = Convert.ToInt16(Session[Utilities.UserId].ToString());
            }
            return id;
        }
        // GET: Report
        public async Task<JsonResult> GetOrganizationalScoreLevel1(int id = 0,string level=null)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
            //    Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationScore(GetUserIdIfIdIsEmpty(id), level));
            return  Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetFinalScoreLevel1(int id = 0, string level = null)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationFinalScore(GetUserIdIfIdIsEmpty(id), level));
            return  Json(graph, JsonRequestBehavior.AllowGet);
            
        }

        public async Task<JsonResult> GetSectorOrganizationalScoreLevel1(int id = 0, string level = null)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
            //    Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationManufacturingScore(GetUserIdIfIdIsEmpty(id), level));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSectorFinalScoreLevel1(int id = 0, string level = null)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationManufacturingFinalScore(GetUserIdIfIdIsEmpty(id), level));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetOrganizationalScoreLevel2(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 50, 48, 40, 19, 86, 27, 90, 10, 25 },
            //    Org = new int[] { 50, 90, 50, 56, 22, 36, 85, 20, 10 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel2));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetFinalScoreLevel2(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationFinalScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel2));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSectorOrganizationalScoreLevel2(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
            //    Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationManufacturingScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel2));
            return  Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSectorFinalScoreLevel2(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationManufacturingFinalScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel2));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetOrganizationalScoreLevel3(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 75, 48, 40, 19, 86, 27, 90, 10, 25 },
            //    Org = new int[] { 75, 90, 50, 56, 22, 36, 85, 20, 10 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel3));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetFinalScoreLevel3(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationFinalScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel3));
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorOrganizationalScoreLevel3(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
            //    Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationManufacturingScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel3));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSectorFinalScoreLevel3(int id = 0)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.OrganizationManufacturingFinalScore(GetUserIdIfIdIsEmpty(id), Utilities.AssessmentTypeLevel3));
            return Json(graph, JsonRequestBehavior.AllowGet);
        }



        public async Task<JsonResult> SectorOrganizationScore(int sectorId, int subsectorId,string level,int assessementId)
        {

            //    var graph = new Graph()
            //    {
            //        OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
            //        Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            //    };
            var graph = await Task.Run(() => ScoreReport.SectorOrganizationScore(sectorId,subsectorId,level,assessementId));
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> SectorOrganizationFinalScore(int sectorId, int subsectorId, string level, int assessementId)
        {
            //var graph = new Graph()
            //{
            //    OtherOrg = new int[] { 700 },
            //    Org = new int[] { 500 }
            //};
            var graph = await Task.Run(() => ScoreReport.SectorOrganizationFinalScore(sectorId, subsectorId, level, assessementId));
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

    }
}