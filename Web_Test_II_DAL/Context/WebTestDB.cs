using Microsoft.EntityFrameworkCore;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II_DAL.Context
{
    public class WebTestDB : DbContext
    {
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;

        public WebTestDB(DbContextOptions<WebTestDB> options) : base(options) 
        {
            Database.EnsureCreated();
        }



    }
}
