using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.Business
{
    public interface IGroupSaveQuiz
    {
        void SaveAnswer(List<QuestionQuiz> answers);
        void CompleteQuiz();
        bool MoveToNextGroup(int groupId);
        int CalculateScore();
    }
}
