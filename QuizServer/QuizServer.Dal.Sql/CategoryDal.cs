using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class CategoryDal : ICategoryDal
    {
        public List<Category> GetAllCategories()
        {
            using (var context = new QuizEntities())
            {
                return context.Categories.ToList();
            }
        }

        public Category AddCategory(string name)
        {
            using (var context = new QuizEntities())
            {
                var category = new Category()
                {
                    Name = name
                };
                var savedCategory = context.Categories.Add(category);
                context.SaveChanges();
                return savedCategory;
            }
        }

        public void DeleteCategory(int id)
        {
            using (var context = new QuizEntities())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
