using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.Business
{
    interface IQuizManager : ISaveQuiz
    {
        QuestionAnswer LoadQuiz(int questionId);
        List<QuestionAnswer> GetAllQuestions();

    }
}
