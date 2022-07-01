using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Answer : Entity
    {
        public string Name { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
        public bool IsAnswer { get; set; }

    }
}
