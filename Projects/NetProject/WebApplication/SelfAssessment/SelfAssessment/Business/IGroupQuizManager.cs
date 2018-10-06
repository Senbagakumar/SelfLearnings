using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.Business
{
    public interface IGroupQuizManager : IGroupSaveQuiz
    {
        GroupQuiz LoadQuiz(int groupId);
        List<GroupQuiz> GetAllQuestions();

    }
}
