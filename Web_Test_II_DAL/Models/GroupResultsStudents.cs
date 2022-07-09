using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II_DAL.Models
{
    public class GroupResultsStudents
    {
        public int IdTest { get; set; }
        public string NameTest { get; set; }
        public int CountQuestions { get; set; }
        public double Points { get; set; }
        public int CountTrying { get; set; }


    }
}
