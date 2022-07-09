using Web_Test_II_DAL.Entityes;


namespace Web_Test_II.Models.ViewModels.DataOperationsViewModels
{
    public class StudentsViewModel
    {
        public List<Student> Students { get; set; }
        public StudentsViewModel(List<Student> students) 
        { 
            Students = students;    
        }
    }
}
