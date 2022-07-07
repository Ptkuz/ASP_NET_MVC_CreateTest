using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class User : Entity
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

        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Mentor? Mentor { get; set; }
    }
}
