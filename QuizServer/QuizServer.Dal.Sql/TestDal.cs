using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class TestDal : ITestDal
    {
        public List<Test> GetAllTests()
        {
            using (var context = new QuizEntities())
            {
                return context.Tests.Include(c => c.Difficulty).ToList();
            }
        }
    }
}
