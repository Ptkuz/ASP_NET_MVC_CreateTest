using System.ComponentModel.DataAnnotations;
using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Student : Person
    {
        [Required(ErrorMessage = "Группа не выбрана")]
        [Display(Name = "Группа:")]
        public virtual Group Group { get; set; } = null!;
        public virtual List<Result> Results { get; set; } = new List<Result>();



    }
}
