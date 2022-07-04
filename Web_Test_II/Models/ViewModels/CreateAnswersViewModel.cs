using System.ComponentModel;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels
{
	public class CreateAnswersViewModel
	{
        [DisplayName("Вопрос")]
        public Question Question { get; set; }

        [DisplayName("Список ответов")]
        public List<string> Answers { get; set; }
        public List<string> IsAnswer { get; set; }

        public IQueryable<Answer> AnswersDB { get; set; }

        public CreateAnswersViewModel() { }

        public CreateAnswersViewModel(IQueryable<Answer> answersDB)
        {
            AnswersDB = answersDB;
        }
    }
}
