using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class GroupRepository : DbRepository<Group>
    {
        public GroupRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<Group> Items =>
            base.Items.Include(item => item.Students);
    }
}
