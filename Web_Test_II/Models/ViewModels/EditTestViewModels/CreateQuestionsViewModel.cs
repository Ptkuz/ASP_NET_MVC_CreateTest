using System.ComponentModel;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.EditTestViewModels
{
    public class CreateQuestionsViewModel
    {

        [DisplayName("Список вопросов")]
        public List<string> Questions { get; set; }

        public CreateQuestionsViewModel() { }

    }
}
