using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL.EntitiesRepositories
{
    internal class StudentRepository : DbRepository<Student>
    {
        public StudentRepository(WebTestDB db) : base(db)
        {
        }

        public override IQueryable<Student> Items =>
            base.Items.Include(item => item.Group).Include(item => item.Group);
    }
}
