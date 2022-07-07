using System.ComponentModel;
using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Test : Entity
    {
        [DisplayName("Название теста")]
        public string Name { get; set; } = null!;
        public bool IsAvtive { get; set; }
        public virtual List<Question> Questions { get; set; } = new List<Question>();
        public virtual List<Result> Results { get; set; } = new List<Result>();
    }
}
