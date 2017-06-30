using System;
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
    public class DifficultyController : BaseController
    {
        private IDifficultyDal _difficultyDal;

        public DifficultyController() : this(new DifficultyDal())
        {

        }

        public DifficultyController(IDifficultyDal DifficultyDal)
        {
            _difficultyDal = DifficultyDal;
        }

        [AuthorizeAccess]
        public HttpResponseMessage GetCategories()
        {
            var DifficultyList = _difficultyDal.GetDifficultyLevelList().Select(c => new {c.Id,Name = c.DifficultyLevel});

            return Request.CreateResponse(HttpStatusCode.OK, DifficultyList);
        }

        [AuthorizeAccess]
        public HttpResponseMessage PostDifficulty([FromBody] DataModel model)
        {
            if(String.IsNullOrEmpty(model.data) || String.IsNullOrWhiteSpace(model.data))
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Invalid input" });

            var DifficultyExists = _difficultyDal.GetDifficultyLevelList().Any(c => c.DifficultyLevel == model.data);
            if (DifficultyExists)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new {Message = "Difficulty already exists"});

            _difficultyDal.AddDifficultyLevel(model.data);
            return Request.CreateResponse(HttpStatusCode.Created, new {Message = "Difficulty has been created"});
        }

        [AuthorizeAccess]
        public HttpResponseMessage DeleteDifficulty(int id)
        {
            var DifficultyExists = _difficultyDal.GetDifficultyLevelList().Any(c => c.Id == id);

            if (!DifficultyExists)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new {Message = "Difficulty doesn't exist"});

            _difficultyDal.DeleteDifficultyLevel(id);
            return Request.CreateResponse(HttpStatusCode.Created, new {Message = "Difficulty has been deleted"});
        }
    }
}
