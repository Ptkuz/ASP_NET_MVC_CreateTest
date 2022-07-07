using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II_DAL.Context
{
    public class WebTestDB : DbContext
    {
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Mentor> Mentors { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public WebTestDB() { }

        public WebTestDB(DbContextOptions<WebTestDB> options) : base(options) 
        {
            //Database.EnsureDeletedAsync();
            Database.EnsureCreatedAsync();
        }

       

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder
               .Entity<User>()
               .HasOne(u => u.Student)
               .WithOne(s => s.User)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey<Student>(s => s.UserKey);

            builder
               .Entity<User>()
               .HasOne(u => u.Mentor)
               .WithOne(m => m.User)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey<Mentor>(m => m.UserKey);

            string adminRoleName = "admin";
            string studentRoleName = "student";
            string mentorRoleName = "mentor";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "Admin1234!";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role studentRole = new Role { Id = 2, Name = studentRoleName };
            Role mentorRole = new Role { Id = 3, Name = mentorRoleName };

            User adminUser = new User() { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            builder.Entity<Role>().HasData(new Role[] { adminRole, studentRole, mentorRole });
            builder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(builder);

        }




    }
}
