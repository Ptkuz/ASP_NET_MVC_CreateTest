using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class UserRepository : DbRepository<User>
    {
        public UserRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<User> Items =>
            base.Items.Include(item => item.Mentor).Include(item=>item.Student).Include(item=>item.Role);
    }
}
