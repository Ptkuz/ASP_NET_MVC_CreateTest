using Microsoft.AspNetCore.Mvc;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.TestingViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Web_Test_II.Controllers
{
    public class TestingController : Controller
    {
        private readonly ILogger<TestingController> _logger;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Result> _resultRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Test> _testRepository;
        public TestingController(
            ILogger<TestingController> logger,
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<Result> resultRepository,
            IRepository<Student> studentRepository,
            IRepository<Test> testRepository
            )
        {
            _logger = logger;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _resultRepository = resultRepository;
            _studentRepository = studentRepository;
            _testRepository = testRepository;
        }


        [HttpGet]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> ViewAvailableTests()
        {
            var availableTests = await _testRepository.GetAvailableTests();
            var availableTestsToList = availableTests.ToList();

            List<int> countQuest = new List<int>();
            foreach (var test in availableTests)
            {
                var questionsInTest = await _questionRepository.GetQuestionsInTest(test.Id);
                countQuest.Add(questionsInTest.Count());
            }

            AvailableTestsViewModel model = new AvailableTestsViewModel(availableTestsToList, countQuest);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> OpenTest(int id)
        {

            var questions = await _questionRepository.GetQuestionsInTest(id);
            var questionsList = questions.ToList();
            OpenTestViewModel model = new OpenTestViewModel(questionsList);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> OpenTest(IFormCollection keys) 
        {
            int score = 0;
            string[] questionsId = keys["questionId"];
            foreach (var questionId in questionsId) 
            {
                var allAnswers = await _answerRepository.GetAnswersInQuestion(Int32.Parse(questionId)); // List всех ответов
                var answersCurrent = await _questionRepository.GetTrueAnswers(Int32.Parse(questionId)); // List правильных ответов
                bool checkTextBox = allAnswers.Count() == answersCurrent.Count(); // Если количество ответов равно количеству правильных ответов, значит это поля textbox
                string[] answers = keys["question_" + questionId]; // Массив выбранных ответов пользователя
                int countAnswer = 0; // Счетчик правильных ответов
                foreach (var answer in answers) 
                {
                    if (checkTextBox)
                    {
                        var firstAnswer = allAnswers.First();
                        if(firstAnswer.Name == answer)
                            countAnswer++;
                    }
                    else
                    {
                        bool answerParse = Int32.TryParse(answer, out int idAnswer);
                        if (answerParse)
                        {
                            if (answersCurrent.Contains(idAnswer))
                                countAnswer++;
                        }
                    }
                }
                if(answersCurrent.Count==countAnswer)
                    score++;
            
            }
            int result = score;
            return View();
        }

        
    }
}
