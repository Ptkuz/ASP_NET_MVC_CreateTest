using System.ComponentModel.DataAnnotations;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.DataOperationsViewModels
{
    public class GroupsViewModel
    {
        public List<Group> Groups { get; set; }


        [Required(ErrorMessage = "Не указано название группы")]
        [Display(Name = "Группа")]
        public Group Group { get; set; }

        public GroupsViewModel() { }

        public GroupsViewModel(List<Group> groups)
        {
            Groups = groups;
        }
    }
}
