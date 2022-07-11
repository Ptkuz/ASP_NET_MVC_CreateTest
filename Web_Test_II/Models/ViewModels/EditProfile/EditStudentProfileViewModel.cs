using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.EditProfile
{
    public class EditStudentProfileViewModel
    {
        public Student Student { get; set; }
        public User User { get; set; }
        public List<Group> Groups { get; set; }

        public EditStudentProfileViewModel() { }


        public EditStudentProfileViewModel(Student student, User user, List<Group> groups)
        {
            Student = student;
            User = user;
            Groups = groups;
        }

    }
}
