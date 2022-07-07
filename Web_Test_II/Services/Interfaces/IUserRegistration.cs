using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Services.Interfaces
{
    public interface IUserRegistration
    {
        Task<bool> SendEmailAsync(User user, Mentor mentor);
        Task<bool> SendEmailAsync(User user, Student student);
    }
}
