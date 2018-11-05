using SelfAssessment.DataAccess;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Business
{
    public class BaseHelper
    {
        public static List<SelectListItem> GetSectorValues()
        {
            var baseList = new List<SelectListItem>();

            using (var repo = new Repository<Sector>())
            {
                var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
                var allItem = new SelectListItem() { Text=Utilities.SectorAll,Value=Utilities.SectorValue };
                baseList = repo.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
                baseList.Insert(0, firstItem);
                baseList.Insert(1, allItem);
            }
            return baseList;
        }
        public static List<SelectListItem> GetSubSectorValues(int id=0)
        {
            var baseList = new List<SelectListItem>();

            using (var repo = new Repository<SubSector>())
            {
                //var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
                var allItem = new SelectListItem() { Text = Utilities.SectorAll, Value = Utilities.SectorValue };
                if(id>0 && id!=1000 && id!=1001)
                    baseList = repo.Filter(q => q.SectorId == id).Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
                else
                    baseList = repo.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
                

                //baseList.Insert(0, firstItem);
                baseList.Insert(0, allItem);
            }
            return baseList;
        }
    }
}