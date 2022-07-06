using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class MentorRepository : DbRepository<Mentor>
    {
        public MentorRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<Mentor> Items =>
            base.Items.Include(item => item.User);
    }
}
