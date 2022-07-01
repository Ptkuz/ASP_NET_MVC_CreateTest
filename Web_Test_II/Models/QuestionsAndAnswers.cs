using System.ComponentModel;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models
{
    public class QuestionsAndAnswers
    {
        [DisplayName("Название теста")]
        public Test Test { get; set; }

        [DisplayName("Список вопросов")]
        public List<string> Questions { get; set; }

        [DisplayName("Список ответов")]
        public List<string> Answers { get; set; }

        public QuestionsAndAnswers() 
        {
           
        }
    }
}
