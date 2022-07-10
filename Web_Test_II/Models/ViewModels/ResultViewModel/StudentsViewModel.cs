using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.ResultViewModel
{
    public class StudentsViewModel
    {
        public IQueryable<Student> Students { get; set; }

        public StudentsViewModel(IQueryable<Student> students) 
        { 
            Students = students;
        }

    }
}
