using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class QuestionRepository : DbRepository<Question>
    {
        public QuestionRepository(WebTestDB db) : base(db)
        {
        }

        public override IQueryable<Question> Items =>
            base.Items.Include(item => item.Test);


    }
}
