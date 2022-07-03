using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels
{
	public class AnswersViewModel
	{
		public IQueryable<Answer> Answers { get; set; }
		public Question Question { get; set; }


		public AnswersViewModel() { }
		public AnswersViewModel(IQueryable<Answer> answers, Question question)
		{
			Answers = answers;
			Question = question;
		}
	}
}
