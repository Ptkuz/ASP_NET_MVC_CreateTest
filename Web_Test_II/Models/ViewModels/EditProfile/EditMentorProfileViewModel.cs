using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.EditProfile
{
    public class EditMentorProfileViewModel
    {
        public Mentor Mentor { get; set; }
        public User User { get; set; }
        public List<Position> Positions { get; set; }

        public EditMentorProfileViewModel() { }


        public EditMentorProfileViewModel(Mentor mentor, User user, List<Position> positions) 
        { 
            Mentor = mentor;
            User = user;
            Positions = positions;
        }

    }
}
