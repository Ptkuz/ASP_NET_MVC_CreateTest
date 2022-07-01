using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Group : Entity
    {
        public string Name { get; set; } = null!;
        public virtual List<Student> Students { get; set; } = new List<Student>();

    }
}
