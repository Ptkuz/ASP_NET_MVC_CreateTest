using Web_Test_II.Services.Interfaces;
using Web_Test_II_Interfaces;
using Web_Test_II_DAL.Entityes;
using System.Net.Mail;
using System.Net;

namespace Web_Test_II.Services
{
    public class UserRegistration : IUserRegistration
    {
        private readonly IRepository<Role> _roleRepository;

        public static int? idUser { get; set; }

        public UserRegistration(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

       

        public async Task<bool> SendEmailAsync(User user, Mentor mentor)
        {
            try
            {
                MailAddress from = new MailAddress("ServiceCenterEFWPF@yandex.ru", "Сайт тестирования - Gurrex");
                MailAddress to = new MailAddress(user.Email);
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Данные для входа на сайт тестирования";
                message.Body = SendInformationMentor(user, mentor);
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                smtp.Credentials = new NetworkCredential("ServiceCenterEFWPF", "Assassins2012");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw new Exception("При отправке электронного письма произошла ошибка!");
                throw;
            }
        }

        public async Task<bool> SendEmailAsync(User user, Student student)
        {
            try
            {
                MailAddress from = new MailAddress("ServiceCenterEFWPF@yandex.ru", "Сайт тестирования - Gurrex");
                MailAddress to = new MailAddress(user.Email);
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Данные для входа на сайт тестирования";
                message.Body = SendInformationStudent(user, student);
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                smtp.Credentials = new NetworkCredential("ServiceCenterEFWPF", "Assassins2012");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw new Exception("При отправке электронного письма произошла ошибка!");
                throw;
            }
        }


        private string SendInformationMentor(User user, Mentor mentor) =>
                $"{mentor.Name} {mentor.Patronymic}, вы успешно зарегестрованы на сайте тестирования студентов в качестве '{mentor.Position.Name}'. \n" +
                $"Ваш логин для входа на сайт: {user.Email}\n" +
                $"Ваш пароль для входа на сайт: {user.Password}\n" +
                $"Если возникнут вопросы, обратитесь к администратору сайта.";

        private string SendInformationStudent(User user, Student student) =>
                $"{student.Name} {student.Patronymic}, вы успешно зарегестрованы на сайте тестирования " +
                $"студентов в качестве учащегося группы '{student.Group.Name}'. \n" +
                $"Ваш логин для входа на сайт: {user.Email}\n" +
                $"Ваш пароль для входа на сайт: {user.Password}\n" +
                $"Если возникнут вопросы, обратитесь к администратору сайта.";


    }
}
