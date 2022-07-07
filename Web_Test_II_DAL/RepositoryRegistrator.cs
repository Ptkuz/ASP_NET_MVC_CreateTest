using Microsoft.Extensions.DependencyInjection;
using Web_Test_II_Interfaces;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_DAL.EntitiesRepositories;

namespace Web_Test_II_DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services)=> services
            .AddScoped<IRepository<Answer>, AnswerRepository>()
            .AddScoped<IRepository<Group>, GroupRepository>()
            .AddScoped<IRepository<Mentor>, MentorRepository>()
            .AddScoped<IRepository<Position>, PositionRepository>()
            .AddScoped<IRepository<Question>, QuestionRepository>()
            .AddScoped<IRepository<Result>, ResultRepository>()
            .AddScoped<IRepository<Role>, RoleRepository>()
            .AddScoped<IRepository<Student>, StudentRepository>()
            .AddScoped<IRepository<Test>, TestRepository>()
            .AddScoped<IRepository<User>, UserRepository>()
            ;
    }
}
