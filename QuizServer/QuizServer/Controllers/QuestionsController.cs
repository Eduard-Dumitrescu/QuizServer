using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.FilterAttribute;
using QuizServer.Model.Models;

namespace QuizServer.Controllers
{
    public class QuestionsController : BaseController
    {
        public IQuestionDal _questionDal;
        public IAnswerDal _answerDal;

        public QuestionsController() : this(new QuestionDal(),new AnswerDal())
        {
                
        }

        public QuestionsController(IQuestionDal questionDal, IAnswerDal answerDal)
        {
            _questionDal = questionDal;
            _answerDal = answerDal;
        }


        [AuthorizeAccess]
        public HttpResponseMessage PostQuestion([FromBody]QuestionDataModel model)
        {
            var rez = _questionDal.AddQueston(model.Text, model.DifficultyID, model.SubjectId, model.TypeId);

            foreach (var answer in model.Answers)
            {
                if (String.IsNullOrEmpty(answer.Text))
                    continue;
                _answerDal.AddAnswer(answer.Text, answer.Value, rez.Id);
            }

            return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Question has been created" });
        }

    }
}
