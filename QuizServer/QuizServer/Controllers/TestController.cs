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
    [EnableCors("http://localhost:41093", "*", "*")]
    public class TestController : BaseController
    {
        private ITestDal _testDal;

        public TestController() : this(new TestDal())
        {
            
        }

        public TestController(ITestDal testDal)
        {
            _testDal = testDal;
        }

        [AuthorizeAccess]
        public HttpResponseMessage GetTests()
        {
            var tests = _testDal.GetAllTests().Select(t => new TestModel() {Id = t.Id, Name = t.Name + " " + t.Difficulty.DifficultyLevel}).ToList();
  
            return Request.CreateResponse(HttpStatusCode.OK,tests);
        }
    }
}
