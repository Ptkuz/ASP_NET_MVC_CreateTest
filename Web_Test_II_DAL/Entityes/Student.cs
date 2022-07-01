using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Student : Entity
    {
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public virtual Group Group { get; set; } = null!;
        public virtual List<Result> Results { get; set; } = new List<Result>();



    }
}
