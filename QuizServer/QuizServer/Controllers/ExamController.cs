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


    }
}
