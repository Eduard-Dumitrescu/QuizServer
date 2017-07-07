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
    public class QuestionTypeController : BaseController
    {
        private IQuestionTypeDal _questionTypeDal;

        public QuestionTypeController() : this(new QuestionTypeDal())
        {

        }

        public QuestionTypeController(IQuestionTypeDal QuestionTypeDal)
        {
            _questionTypeDal = QuestionTypeDal;
        }

        [AuthorizeAccess]
        public HttpResponseMessage GetQuestionType()
        {
            var QuestionTypeList = _questionTypeDal.GetAllQuestionTypes().Select(c => new { c.Id, c.Name });

            return Request.CreateResponse(HttpStatusCode.OK, QuestionTypeList);
        }

    }
}
