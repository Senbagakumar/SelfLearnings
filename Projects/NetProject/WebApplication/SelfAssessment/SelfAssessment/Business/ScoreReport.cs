using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SelfAssessment.Business
{
    public class scorec
    {
        public List<Questions> Questions { get; set; }
        public int Type { get; set; }
    }
    public class ScoreReport
    {
        static string FinalScoreFalse = "FinalScoreFalse";
        static string FinalScoreTrue = "FinalScoreTrue";
        static string SectorFinalScoreFalse = "SectorFinalScoreFalse";
        static string SectorFinalScoreTrue = "SectorFinalScoreTrue";
        static string CalculateScoreTrue = "CalculateScoreTrue";
        static string CalculateScoreFalse = "CalculateScoreFalse";

        public static ScoreCalc CalculateScore(int userId, bool isFinalScore, string level, int assessmentId=0)
        {

            var listOfGroupQuestions = new List<GroupQuiz>();
            var uInfo = new Repository<Organization>();
            int sectorValue = int.Parse(Utilities.SectorValue);

            var questionIds = new List<int>();
            if (assessmentId == 0)
            {
                var userSectorId = uInfo.Filter(q => q.Id == userId).Select(q => q.SectorId).FirstOrDefault();
                if (userSectorId == 0)
                    userSectorId = uInfo.AssessmentContext.organizationLevelHistories.Where(q => q.Id == userId).Select(q => q.SectorId).FirstOrDefault();

                var assessmentDetails = uInfo.AssessmentContext.assessments.Where(q => q.Sector == userSectorId).FirstOrDefault();
                if (assessmentDetails == null || assessmentDetails.Sector == 0)
                    assessmentDetails = uInfo.AssessmentContext.assessments.Where(q => q.Sector == sectorValue).FirstOrDefault();
               
                questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == assessmentDetails.Id && q.Level == level).Select(q => q.QuestionId).ToList();
            }
            else
                questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == assessmentId && q.Level == level).Select(q => q.QuestionId).ToList();

            var lQuestions = uInfo.AssessmentContext.questions.Where(q => questionIds.Contains(q.Id)).GroupBy(q => q.GroupId).Select(q => new scorec() { Questions = q.ToList(), Type = q.Key }).OrderBy(t => t.Type).ToList();

            string cacheName = string.Empty;
            if (isFinalScore)
                cacheName = CalculateScoreTrue + userId;
            else
                cacheName = CalculateScoreFalse + userId;

            ScoreCalc scoreCalc = Caching.GetObjectFromCache<ScoreCalc>(cacheName, 120, () => CalculateScoresForReports(userId, isFinalScore, lQuestions));

            scoreCalc.UserId = userId;
            return scoreCalc;
        }

        private static ScoreCalc CalculateScoresForReports(int userId, bool isFinalScore, List<scorec> lQuestions)
        {
            var uInfo = new Repository<Organization>();
            var scoreCalc = new ScoreCalc();
            scoreCalc.Scores = new List<GraphDynam>();
            lQuestions.ForEach(t =>
            {
                var dynamicScore = new GraphDynam();
                var group = uInfo.AssessmentContext.groups.Where(q => q.Id == t.Type).FirstOrDefault();
                dynamicScore.GroupName = group.Name;
                double weight = group.Weight > 0 ? Convert.ToDouble(group.Weight) : 1;
                double score = 0;

                t.Questions.ForEach(v =>
                {
                    var isAnswer = uInfo.AssessmentContext.answers.Where(a => a.QuestionId == v.Id && a.GroupId == v.GroupId && a.UserId == userId).FirstOrDefault();
                    if (isAnswer != null)
                    {
                        score += Utilities.CalculateScoreByAns(isAnswer.UserOptionId);

                    }

                });

                if (isFinalScore)
                    score = score * weight;

                dynamicScore.MyScore = Convert.ToInt16((score / t.Questions.Count()));
                scoreCalc.Scores.Add(dynamicScore);
            });
            return scoreCalc;
        }

        public static Graph OrganizationScore(int currentUserId,string level)
        {
            ScoreCalc myScore = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreFalse+level,120, ()=> CalculateScore(currentUserId,false, level));
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.Filter(t => t.Id != currentUserId).Select(q => q.Id).ToList();
            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreFalse+level+id, 120, () => CalculateScore(id, false, level));
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[myScore.Scores.Count];
            graph.Org = new int[myScore.Scores.Count];
            graph.Groups = new string[myScore.Scores.Count];
            int i = 0;
            foreach (var grp in myScore.Scores)
            {                
                var other = otherOrg.Select(t => t.Scores.Where(v => v.GroupName == grp.GroupName)).ToList();
                var avg = other.Select(q => q.Average(t => t.MyScore)).FirstOrDefault();
                graph.OtherOrg[i] = Convert.ToInt16(avg);
                graph.Org[i]= Convert.ToInt16(grp.MyScore);
                graph.Groups[i] = grp.GroupName;
                i = i + 1;
            }
            return graph;

        }

        public static Graph OrganizationFinalScore(int currentUserId,string level)
        {
            ScoreCalc myScore = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreTrue+level, 120, () => CalculateScore(currentUserId, true, level));
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.Filter(t => t.Id != currentUserId).Select(q => q.Id).ToList();

            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreTrue+level+id, 120, () => CalculateScore(id, true, level));
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[1];
            graph.Org = new int[1];

            graph.Org[0] = myScore.Scores.Sum(q => q.MyScore);
            graph.OtherOrg[0] = otherOrg.Select(t => t.Scores.Sum(v => v.MyScore)).FirstOrDefault();

            return graph;

        }

        public static Graph OrganizationManufacturingScore(int currentUserId,string level)
        {
            ScoreCalc myScore = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreFalse+level, 120, () => CalculateScore(currentUserId, false, level));
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q=> q.u.Id != currentUserId).Select(q=> q.u.Id).ToList();
            if (lorgs.Count == 0)
                lorgs = uInfo.AssessmentContext.UserInfo.Where(q => q.Id != currentUserId).Select(q => q.Id).ToList();
            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreFalse+level+id, 120, () => CalculateScore(id, false, level));
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[myScore.Scores.Count];
            graph.Org = new int[myScore.Scores.Count];
            graph.Groups = new string[myScore.Scores.Count];
            int i = 0;
            foreach (var grp in myScore.Scores)
            {
                var other = otherOrg.Select(t => t.Scores.Where(v => v.GroupName == grp.GroupName)).ToList();
                var avg = other.Select(q => q.Average(t => t.MyScore)).FirstOrDefault();
                graph.OtherOrg[i] = Convert.ToInt16(avg);
                graph.Org[i] = Convert.ToInt16(grp.MyScore);
                graph.Groups[i] = grp.GroupName;
                i = i + 1;
            }
            return graph;

        }

        public static Graph OrganizationManufacturingFinalScore(int currentUserId,string level)
        {
            ScoreCalc myScore = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreTrue+level, 120, () => CalculateScore(currentUserId, true, level));
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q => q.u.Id != currentUserId).Select(q => q.u.Id).ToList();
            if (lorgs.Count == 0)
                lorgs = uInfo.AssessmentContext.UserInfo.Where(q => q.Id != currentUserId).Select(q => q.Id).ToList();
            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = Caching.GetObjectFromCache<ScoreCalc>(FinalScoreTrue+level+id, 120, () => CalculateScore(id, true, level));
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[1];
            graph.Org = new int[1];
            graph.Org[0] = myScore.Scores.Sum(q => q.MyScore);
            graph.OtherOrg[0] = otherOrg.Select(t => t.Scores.Sum(v => v.MyScore)).FirstOrDefault();
            return graph;

        }

        
        public static Graph SectorOrganizationScore(int sectorId,int subSectorId, string level, int assementId)
        {
            List<int> uids = GetUserIdForSectors(sectorId, subSectorId, level.Trim(), assementId );
            if (uids.Count == 0)
                return null;

            var otherOrg = new List<ScoreCalc>();
            var scores = new ScoreCalc();
            foreach (var id in uids)
            {
                scores = Caching.GetObjectFromCache<ScoreCalc>(SectorFinalScoreFalse + level + id, 120, () => CalculateScore(id, false, level, assementId));
                otherOrg.Add(scores);
            }

            var groups = new List<string>();
            otherOrg[0].Scores.ForEach(t =>
            {
                groups.Add(t.GroupName);
            });

            var graph = new Graph();
            graph.OtherOrg = new int[scores.Scores.Count];
            graph.Org = new int[scores.Scores.Count];
            graph.Groups = new string[scores.Scores.Count];
            int i = 0;
            foreach (var grp in groups)
            {
                try
                {
                    var other = otherOrg.Select(t => t.Scores.Where(v => v.GroupName == grp)).ToList();
                    if (other != null && other.Count > 0)
                    {
                        var oth = other.FirstOrDefault().FirstOrDefault();
                        if (oth != null)
                        {
                            var avg = other.Select(q => q.Average(t => t.MyScore)).FirstOrDefault();
                            graph.OtherOrg[i] = Convert.ToInt16(avg);
                            graph.Groups[i] = grp;
                            i = i + 1;
                        }

                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return graph;

        }

        private static List<int> GetUserIdForSectors(int sectorId, int subSectorId, string level, int assessmentId)
        {
            var uids = new List<int>();


            var uInfo = new Repository<Organization>();

            var lorgs = uInfo.Filter(q=> q.IsActive && q.CurrentAssignmentType == level && q.CurrentAssignmentStatus == Utilities.AssessmentCompletedStatus && q.AssessmentId == assessmentId).Select(q => new { Id = q.Id, SectorId = q.SectorId, SubSectorId = q.SubSectorId }).ToList();

            //var lorgs = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q => q.u.IsActive && q.u.CurrentAssignmentStatus == Utilities.AssessmentCompletedStatus).Select(q => new { Id = q.u.Id, SectorId = q.u.SectorId, SubSectorId = q.u.SubSectorId }).ToList();
            if (lorgs.Count == 0)
            {
                //lorgs = uInfo.AssessmentContext.organizationLevelHistories.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Select(q => new { Id = q.u.OrgId, SectorId = q.u.SectorId, SubSectorId = q.u.SubSectorId }).ToList();
                lorgs = uInfo.AssessmentContext.organizationLevelHistories.Where(q => q.Level == level && q.AssessmentId == assessmentId).Select(q => new { Id = q.Id, SectorId = q.SectorId, SubSectorId = q.SubSectorId }).ToList();
                if (lorgs.Count == 0)
                {
                    uids = uInfo.AssessmentContext.UserInfo.Where(q => q.IsActive && q.CurrentAssignmentStatus == Utilities.AssessmentCompletedStatus).Select(q => q.Id).ToList();
                }
            }
            if (sectorId > 0 && sectorId != 1001)
                lorgs = lorgs.Where(q => q.SectorId == sectorId).ToList();
            if (subSectorId > 0 && subSectorId != 1001)
                lorgs = lorgs.Where(q => q.SubSectorId == subSectorId).ToList();

            uids = lorgs.Select(q => q.Id).ToList();           

            return uids;
        }

        public static Graph SectorOrganizationFinalScore(int sectorId, int subSectorId, string level, int assessmentId)
        {
            List<int> uids = GetUserIdForSectors(sectorId, subSectorId,level, assessmentId);

            var otherOrg = new List<ScoreCalc>();
            foreach (var id in uids)
            {
                var scores = Caching.GetObjectFromCache<ScoreCalc>(SectorFinalScoreTrue+level+id, 120, () => CalculateScore(id, true, level, assessmentId));
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[1];
            graph.Org = new int[1];
            graph.OtherOrg[0] = otherOrg.Select(t => t.Scores.Sum(v => v.MyScore)).FirstOrDefault();
            return graph;

        }

    }
}