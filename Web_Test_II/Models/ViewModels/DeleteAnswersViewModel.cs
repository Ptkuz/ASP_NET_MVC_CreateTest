using System.ComponentModel;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels
{
    public class DeleteAnswersViewModel
    {
        public IQueryable<Answer> Answers { get; set; }

        public DeleteAnswersViewModel() { }

        public DeleteAnswersViewModel(IQueryable<Answer> answers)
        {
            Answers = answers;
        }

    }
}
