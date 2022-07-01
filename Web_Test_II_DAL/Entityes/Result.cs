using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Result : Entity
    {     
        public virtual Student Student { get; set; } = null!;
        public virtual Test Test { get; set; } = null!;
        public double Points { get; set; }
        public int TryCount { get; set; }
    }
}
