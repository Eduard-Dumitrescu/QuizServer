using System.Collections.Generic;
using System.Linq;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class DifficultyDal : IDifficultyDal
    {
        public List<Difficulty> GetDifficultyLevelList()
        {
            using (var context = new QuizEntities())
            {
                return context.Difficulties.ToList();
            }
        }

        public Difficulty AddDifficultyLevel(string name)
        {
            using (var context = new QuizEntities())
            {
                var difficulty = new Difficulty()
                {
                    DifficultyLevel = name
                };

                var savedDifficulty = context.Difficulties.Add(difficulty);
                context.SaveChanges();

                return savedDifficulty;
            }
        }

        public void DeleteDifficultyLevel(int id)
        {
            using (var context = new QuizEntities())
            {
                var difficulty = context.Difficulties.FirstOrDefault(c => c.Id == id);

                context.Difficulties.Remove(difficulty);

                context.SaveChanges();
            }
        }
    }
}
