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
    public class CategoryController : BaseController
    {
        private ICategoryDal _categoryDal;

        public CategoryController() : this(new CategoryDal())
        {
            
        }

        public CategoryController(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [AuthorizeAccess]
        public HttpResponseMessage GetCategories()
        {
            var categoryList = _categoryDal.GetAllCategories().Select(c => new {c.Id,c.Name});

            return Request.CreateResponse(HttpStatusCode.OK, categoryList);
        }

        [AuthorizeAccess]
        public HttpResponseMessage PostCategory([FromBody]DataModel model)
        {
            if (String.IsNullOrEmpty(model.data) || String.IsNullOrWhiteSpace(model.data))
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Invalid input" });

            var categoryExists = _categoryDal.GetAllCategories().Any(c => c.Name == model.data);
            if(categoryExists)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Category already exists" });

            _categoryDal.AddCategory(model.data);
            return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Category has been created" });
        }

        [AuthorizeAccess]
        public HttpResponseMessage DeleteCategory(int id)
        {
            var categoryExists = _categoryDal.GetAllCategories().Any(c => c.Id == id);

            if(!categoryExists)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Category doesn't exist" });

           _categoryDal.DeleteCategory(id);
            return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Category has been deleted" });
        }

    }
}
