using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.FilterAttribute;
using QuizServer.Model.Dtos;
using QuizServer.Model.Models;

namespace QuizServer.Controllers
{
    public class TestController : BaseController
    {
        private ITestDal _testDal;
        private IQuestionDal _questionDal;
        private ITestWithQuestionsDal _testsWithQuestionsDal;

        public TestController() : this(new TestDal(),new QuestionDal(),new TestWithQuestionDal())
        {
            
        }

        public TestController(ITestDal testDal,IQuestionDal questionDal,ITestWithQuestionsDal testsWithQuestionsDal)
        {
            _testDal = testDal;
            _questionDal = questionDal;
            _testsWithQuestionsDal = testsWithQuestionsDal;
        }

        [AuthorizeAccess]
        public HttpResponseMessage GetTests()
        {
            var tests = _testDal.GetAllTests().Select(t => new {t.Id, t.Name,Difficulty = t.Difficulty.DifficultyLevel,NameLevel = t.Name + " - " + t.Difficulty.DifficultyLevel}).ToList();
  
            return Request.CreateResponse(HttpStatusCode.OK,tests);
        }

        [AuthorizeAccess]
        public HttpResponseMessage PostTest([FromBody]TestDataModel model)
        {
            foreach (var subject in model.SubjectList)
            {
               if(_questionDal.GetAllQuestions().Count(q => q.CategoryId == subject.Id) < model.NumberOfQuestions)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = " Number of questions to high " });
            }

            var questionList = new List<Question>();

            foreach (var subject in model.SubjectList)
            {
                var questions =
                    _questionDal.GetAllQuestions()
                        .Where(q => q.CategoryId == subject.Id)
                        .OrderBy(r => Guid.NewGuid())
                        .Take(model.NumberOfQuestions).ToList();
                questionList.AddRange(questions);
         
            }

            var test = _testDal.AddTestWithQuestions(model.TestName, model.TestLevelId,
                new TimeSpan(model.Hours, model.Minutes, model.Seconds), questionList);


            return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Test has been created" });
        }

    }
}
