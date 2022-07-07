using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Test_II_DAL.Entityes.Base
{
    public class Person : Entity
    {
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
        public User? User { get; set; }
        public int UserKey { get; set; }
    }
}
