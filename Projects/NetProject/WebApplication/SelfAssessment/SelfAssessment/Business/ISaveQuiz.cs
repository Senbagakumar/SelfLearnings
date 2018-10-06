using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.Business
{
    interface ISaveQuiz
    {
        void SaveAnswer(QuestionQuiz answers);
        void CompleteQuiz();
        bool MoveToNextQuestion(int questionId);
        int CalculateScore();
    }
}
