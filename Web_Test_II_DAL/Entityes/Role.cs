using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Test_II_DAL.Entityes.Base;

namespace Web_Test_II_DAL.Entityes
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }

        public Role() 
        { 
            Users = new List<User>();
        }

    }
}
