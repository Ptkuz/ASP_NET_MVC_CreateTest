using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class AnswerRepository : DbRepository<Answer>
    {
        public AnswerRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<Answer> Items =>
            base.Items.Include(item => item.Question);
    }
}
