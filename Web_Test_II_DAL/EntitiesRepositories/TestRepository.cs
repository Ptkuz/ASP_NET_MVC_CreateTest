using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class TestRepository : DbRepository<Test>
    {
        public TestRepository(WebTestDB db) : base(db)
        {
        }
        public override IQueryable<Test> Items =>
            base.Items.Include(item => item.Questions).Include(item=>item.Results);
    }
}
