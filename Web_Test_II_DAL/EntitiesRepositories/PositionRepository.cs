using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class PositionRepository : DbRepository<Position>
    {
        public PositionRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<Position> Items =>
            base.Items.Include(item => item.Mentors);
    }
}
