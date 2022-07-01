using System.ComponentModel.DataAnnotations;
using Web_Test_II_Interfaces;

namespace Web_Test_II_DAL.Entityes.Base
{
    public class Entity : IEntity
    {
        [Required]
        public int Id { get; set; }

    }
}
