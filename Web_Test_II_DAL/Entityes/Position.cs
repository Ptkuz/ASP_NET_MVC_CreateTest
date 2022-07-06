using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Position : Entity
    {
        public string Name { get; set; } = null!;
        public virtual List<Mentor> Mentors { get; set; } = new List<Mentor>();
    }
}
