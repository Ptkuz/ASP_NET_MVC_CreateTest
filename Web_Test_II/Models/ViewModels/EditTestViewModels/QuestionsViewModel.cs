using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

namespace Web_Test_II.Models.ViewModels.EditTestViewModels
{
    public class QuestionsViewModel
    {
        public List<Question> Questions { get; set; }
        public List<int> CountAnswers { get; set; }
        public List<int> CountTrueAnswers { get; set; }


        public QuestionsViewModel() { }
        public QuestionsViewModel(List<Question> questions, List<int> countAnswers, List<int> countTrueAnswers)
        {
            Questions = questions;
            CountAnswers = countAnswers;
            CountTrueAnswers = countTrueAnswers;
        }
    }
}
