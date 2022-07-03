using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

namespace Web_Test_II.Models.ViewModels
{
    public class QuestionsViewModel
    {
        public IQueryable<Question> Questions { get; set; }


        public QuestionsViewModel() { }
        public QuestionsViewModel(IQueryable<Question> questions)
		{
            Questions = questions;
		}
	}
}
