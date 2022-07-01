using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Question : Entity
    {
        public string Name { get; set; } = null!;
        public virtual Test Test { get; set; } = null!;
        public virtual LinkedList<Answer> Answers { get; set; } = new LinkedList<Answer>();
    }
}
