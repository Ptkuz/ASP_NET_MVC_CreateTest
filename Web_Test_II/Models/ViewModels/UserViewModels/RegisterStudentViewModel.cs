using System.ComponentModel.DataAnnotations;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.UserViewModels
{
    public class RegisterStudentViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Неверный формат Email")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Пароль не соответствует шаблону")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Пароль")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Фамилия не задана")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректная фамилия")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Фамилия:")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Имя не задано")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Имя:")]
        public string Name { get; set; } = null!;

        [Display(Name = "Отчество:")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное отчество")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        public string Patronymic { get; set; } = null!;

        [Required(ErrorMessage = "Группа не выбрана")]
        [Display(Name = "Группа:")]
        public virtual Group Group { get; set; } = null!;
    }
}
