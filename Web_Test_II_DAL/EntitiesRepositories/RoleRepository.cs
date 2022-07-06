using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class RoleRepository : DbRepository<Role>
    {
        public RoleRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<Role> Items =>
            base.Items.Include(item => item.Users);
    }
}
