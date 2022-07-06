using Microsoft.Extensions.DependencyInjection;
using Web_Test_II_Interfaces;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_DAL.EntitiesRepositories;

namespace Web_Test_II_DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services)=> services
            .AddTransient<IRepository<Answer>, AnswerRepository>()
            .AddTransient<IRepository<Group>, GroupRepository>()
            .AddTransient<IRepository<Mentor>, MentorRepository>()
            .AddTransient<IRepository<Position>, PositionRepository>()
            .AddTransient<IRepository<Question>, QuestionRepository>()
            .AddTransient<IRepository<Result>, ResultRepository>()
            .AddTransient<IRepository<Role>, RoleRepository>()
            .AddTransient<IRepository<Student>, StudentRepository>()
            .AddTransient<IRepository<Test>, TestRepository>()
            .AddTransient<IRepository<User>, UserRepository>()
            ;
    }
}
