using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.DataOperationsViewModels
{
    public class MentorsViewModel
    {
        public List<Mentor> Mentors { get; set; }
        public MentorsViewModel(List<Mentor> mentors)
        {
            Mentors = mentors;
        }
    }
}
