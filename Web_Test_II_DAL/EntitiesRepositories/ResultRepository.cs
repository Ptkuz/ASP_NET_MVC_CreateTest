using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class ResultRepository : DbRepository<Result>
    {
        public ResultRepository(WebTestDB db) : base(db)
        {
        }

        public override IQueryable<Result> Items =>
            base.Items.Include(item => item.Test).Include(item=>item.Student);
    }
}
