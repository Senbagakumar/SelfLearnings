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
        // GET: Report
        public async Task<JsonResult> GetOrganizationalScoreLevel1(int id = 0)
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
                Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            };

            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetFinalScoreLevel1()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 700 },
                Org = new int[] { 500 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorOrganizationalScoreLevel1()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
                Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorFinalScoreLevel1()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 700 },
                Org = new int[] { 500 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetOrganizationalScoreLevel2()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 50, 48, 40, 19, 86, 27, 90, 10, 25 },
                Org = new int[] { 50, 90, 50, 56, 22, 36, 85, 20, 10 }
            };

            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetFinalScoreLevel2()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 700 },
                Org = new int[] { 500 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorOrganizationalScoreLevel2()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
                Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorFinalScoreLevel2()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 700 },
                Org = new int[] { 500 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetOrganizationalScoreLevel3()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 75, 48, 40, 19, 86, 27, 90, 10, 25 },
                Org = new int[] { 75, 90, 50, 56, 22, 36, 85, 20, 10 }
            };

            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetFinalScoreLevel3()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 700 },
                Org = new int[] { 500 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorOrganizationalScoreLevel3()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 28, 48, 40, 19, 86, 27, 90, 10, 25 },
                Org = new int[] { 25, 90, 50, 56, 22, 36, 85, 20, 10 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

        public async Task<JsonResult> GetSectorFinalScoreLevel3()
        {
            var graph = new Graph()
            {
                OtherOrg = new int[] { 700 },
                Org = new int[] { 500 }
            };
            return await Task.Run(() => Json(graph, JsonRequestBehavior.AllowGet));
        }

    }
}