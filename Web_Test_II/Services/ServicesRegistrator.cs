using Web_Test_II.Services.Interfaces;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Services
{
    public static class ServicesRegistrator 
    {
        public static IServiceCollection RegistrationServices(this IServiceCollection services) => services
             .AddTransient<IUserRegistration, UserRegistration>()
             ;
    }
}
