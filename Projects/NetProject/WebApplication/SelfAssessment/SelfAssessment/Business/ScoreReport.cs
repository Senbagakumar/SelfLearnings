using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Business
{
    public class ScoreReport
    {
        public static ScoreCalc CalculateScore(int userId, bool isFinalScore, string level)
        {
            var scoreCalc = new ScoreCalc();
            var listOfGroupQuestions = new List<GroupQuiz>();
            var uInfo = new Repository<Organization>();
            //var details = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q => q.u.Id == userId).FirstOrDefault();
            //var questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == details.a.Id && q.Level == details.u.CurrentAssignmentType).Select(q => q.QuestionId).ToList();

            var questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == userId && q.Level == level).Select(q => q.QuestionId).ToList();
            var lQuestions = uInfo.AssessmentContext.questions.Where(q => questionIds.Contains(q.Id)).GroupBy(q => q.GroupId).Select(q => new { Questions = q.ToList(), Type = q.Key }).OrderBy(t => t.Type).ToList();

            scoreCalc.UserId = userId;
            scoreCalc.Scores = new List<GraphDynam>();

            double score = 0;
            lQuestions.ForEach(t =>
            {
                var dynamicScore = new GraphDynam();
                dynamicScore.GroupName = uInfo.AssessmentContext.groups.Where(q => q.Id == t.Type).FirstOrDefault().Name;

                t.Questions.ForEach(v => 
                {
                    var isAnswer = uInfo.AssessmentContext.answers.Where(a => a.QuestionId == v.Id && a.GroupId == v.GroupId && a.UserId == userId).FirstOrDefault();
                    if (isAnswer != null)
                    {
                        score += Utilities.CalculateScoreByAns(isAnswer.UserOptionId);
                        if (isFinalScore && (dynamicScore.GroupName == Utilities.Group6 || dynamicScore.GroupName == Utilities.Group9))
                            score = score * 1.5;
                    }

                });
                dynamicScore.MyScore =Convert.ToInt16((score /t.Questions.Count()));
                scoreCalc.Scores.Add(dynamicScore);
            });
            return scoreCalc;
        }    
        
        public static Graph OrganizationScore(int currentUserId,string level)
        {
            ScoreCalc myScore = CalculateScore(currentUserId,false, level);
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.Filter(t => t.Id != currentUserId).Select(q=> q.Id).ToList();

            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = CalculateScore(id,false, level);
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[9];
            graph.Org = new int[9];
            int i = 0;
            foreach (var grp in myScore.Scores)
            {                
                var other = otherOrg.Select(t => t.Scores.Where(v => v.GroupName == grp.GroupName)).ToList();
                var avg = other.Select(q => q.Average(t => t.MyScore)).FirstOrDefault();
                graph.OtherOrg[i] = Convert.ToInt16(avg);
                graph.Org[i]= Convert.ToInt16(grp.MyScore);
            }
            return graph;

        }

        public static Graph OrganizationFinalScore(int currentUserId,string level)
        {
            ScoreCalc myScore = CalculateScore(currentUserId, false, level);
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.Filter(t => t.Id != currentUserId).Select(q => q.Id).ToList();

            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = CalculateScore(id, false, level);
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[1];
            graph.Org = new int[1];
            graph.Org[0] = myScore.Scores.Sum(q => q.MyScore);
            graph.OtherOrg[0] = otherOrg.Select(t => t.Scores.Sum(v => v.MyScore)).First();
            return graph;

        }

        public static Graph OrganizationManufacturingScore(int currentUserId,string level)
        {
            ScoreCalc myScore = CalculateScore(currentUserId, false, level);
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q=> q.u.Id != currentUserId).Select(q=> q.u.Id).ToList();
            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = CalculateScore(id, false, level);
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[9];
            graph.Org = new int[9];
            int i = 0;
            foreach (var grp in myScore.Scores)
            {
                var other = otherOrg.Select(t => t.Scores.Where(v => v.GroupName == grp.GroupName)).ToList();
                var avg = other.Select(q => q.Average(t => t.MyScore)).FirstOrDefault();
                graph.OtherOrg[i] = Convert.ToInt16(avg);
                graph.Org[i] = Convert.ToInt16(grp.MyScore);
            }
            return graph;

        }

        public static Graph OrganizationManufacturingFinalScore(int currentUserId,string level)
        {
            ScoreCalc myScore = CalculateScore(currentUserId, false, level);
            var uInfo = new Repository<Organization>();
            var lorgs = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q => q.u.Id != currentUserId).Select(q => q.u.Id).ToList();

            var otherOrg = new List<ScoreCalc>();
            foreach (var id in lorgs)
            {
                var scores = CalculateScore(id, false,level);
                otherOrg.Add(scores);
            }

            var graph = new Graph();
            graph.OtherOrg = new int[1];
            graph.Org = new int[1];
            graph.Org[0] = myScore.Scores.Sum(q => q.MyScore);
            graph.OtherOrg[0] = otherOrg.Select(t => t.Scores.Sum(v => v.MyScore)).First();
            return graph;

        }

    }
}