using System.ComponentModel;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.EditTestViewModels
{
    public class AnswersViewModel
    {
        public IQueryable<Answer> Answers { get; set; }

        public AnswersViewModel() { }

        public AnswersViewModel(IQueryable<Answer> answers)
        {
            Answers = answers;
        }

    }
}
