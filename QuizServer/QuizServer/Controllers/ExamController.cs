using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Ajax.Utilities;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.FilterAttribute;
using QuizServer.Model.Dtos;
using QuizServer.Model.Models;

namespace QuizServer.Controllers
{
    public class ExamController : BaseController
    {
        private IExamDal _examDal;
        private IUserDal _userDal;
        private ITestDal _testDal;

        public ExamController() : this(new ExamDal(),new UserDal(), new TestDal())
        {
            
        }
        public ExamController(IExamDal examDal,IUserDal userDal,ITestDal testDal)
        {
            _examDal = examDal;
            _userDal = userDal;
            _testDal = testDal;
        }

        [Route("api/Exams")]
        [AuthorizeAccess]
        public HttpResponseMessage GetExams()
        {
            var examList = _examDal.GetAllExams().Select(e => new {e.Id,e.UserID,e.TestID,e.StartTime,e.EndTime,e.Grade,User = new {e.User.Id,e.User.Email} });
            return Request.CreateResponse(HttpStatusCode.OK, examList);
        }

        [Route("api/Exam/AssignTest")]
        [AuthorizeAccess]
        public HttpResponseMessage PostExam([FromBody]ExamModel model)
        {
            var nullData = model == null;

            if (nullData)
                return Request.CreateResponse(HttpStatusCode.BadRequest,new {Message = "Invalid data"});
            if(_userDal.GetUserById(model.UserId) == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "User not found" });
            if(_testDal.GetTestById(model.TestId) == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Test not found" });
            var userExam = _examDal.GetExamByUserId(model.UserId);

            if (userExam != null && String.IsNullOrEmpty(userExam.Grade))
            {
                _examDal.UpdateUserTest(model.UserId, model.TestId);
                return Request.CreateResponse(HttpStatusCode.Created, new { Message = "User was assigned test" });
            }
                

            _examDal.AssignTestToUser(model.UserId, model.TestId);

            return Request.CreateResponse(HttpStatusCode.Created, new {Message = "User was assigned test"});
        }

        [Route("api/Exams/Subjects")]
        //[AuthorizeAccess]
        public HttpResponseMessage GetSubjects()
        {
            var exam = _examDal.GetExamByUserId(GetUser().Id);
            var test = _testDal.GetTestWithQuestionsById(exam.TestID);

            var subjectList = test.Questions.Select(t => new {Id = t.Category.Id, Name = t.Category.Name}).DistinctBy(d => d.Name).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, subjectList);
        }

        [Route("api/Exams/QuestionsWithAnswers")]
        //[AuthorizeAccess]
        public HttpResponseMessage PostQuestionsWithAnswers([FromBody]DataModel model)
        {
            var exam = _examDal.GetExamByUserId(GetUser().Id);

            if(exam == null || !String.IsNullOrEmpty(exam.Grade))
                return Request.CreateResponse(HttpStatusCode.Forbidden, new { Message = "User does not have test assigned " });

            var test = _testDal.GetTestWithQuestionsById(exam.TestID);

            var questionsList = test.Questions.Where(t => t.CategoryId == model.dataNumber).
                    Select(q => new
                {
                    q.Id,
                    q.QuestionText,
                    Difficulty = new { q.Difficulty.Id,q.Difficulty.DifficultyLevel},
                    Category = new {q.Category.Id,q.Category.Name},
                    Answers =  q.Answers.Select(a => new {a.Id,a.AnswerText})
                }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, questionsList);
        }

        [Route("api/Exams/TimeLeft")]
        public HttpResponseMessage GetTimeLeft()
        {
            var exam = _examDal.GetExamByUserId(GetUser().Id);

            if (exam == null || !String.IsNullOrEmpty(exam.Grade))
                return Request.CreateResponse(HttpStatusCode.Forbidden, new { Message = "User does not have test assigned " });

            var test = _testDal.GetTestWithQuestionsById(exam.TestID);

            var elapsed = DateTime.Now.Subtract(exam.StartTime.Value);
            var testTime = test.Duration.Value;

            var timeLeft = elapsed.TotalSeconds > testTime.TotalSeconds ? new {Hours = 0,Minutes = 0, Seconds = 0} : new { Hours = testTime.Subtract(elapsed).Hours, Minutes = testTime.Subtract(elapsed).Minutes, Seconds = testTime.Subtract(elapsed).Seconds };
           

            return Request.CreateResponse(HttpStatusCode.OK, timeLeft);
        }

        [Route("api/Exams/StartTimer")]
        public HttpResponseMessage PutStartTimer()
        {
            var exam = _examDal.GetExamByUserId(GetUser().Id);

            if (exam == null || !String.IsNullOrEmpty(exam.Grade))
                return Request.CreateResponse(HttpStatusCode.Forbidden, new { Message = "User does not have test assigned " });

            if(exam.StartTime.HasValue)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
            {
                _examDal.StartTimerById(exam.Id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            
        }

        private User GetUser()
        {
            var authorize = Request.Headers.Authorization;
            var userSession = _userSessionDal.GetUserSessionById(new Guid(authorize.ToString()));
            return _userDal.GetUserById(userSession.UserID);
        }



    }
}
