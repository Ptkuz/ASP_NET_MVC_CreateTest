using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Test_II.Models;
using Web_Test_II_DAL;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels;

namespace Web_Test_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Result> _resultRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Test> _testRepository;


        // Для хранения вопроса
        private Question question;


        public HomeController(
            ILogger<HomeController> logger,
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<Result> resultRepository,
            IRepository<Student> studentRepository,
            IRepository<Test> testRepository)
        {
            _logger = logger;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _resultRepository = resultRepository;
            _studentRepository = studentRepository;
            _testRepository = testRepository;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateTest()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateQuestionsViewModel model)
        {
            Test test = new Test();
            List<Question> questions = new List<Question>();
            test.Name = model.Test.Name;
            test.IsAvtive = false;
            await _testRepository.AddAsync(test);
            var testLast = _testRepository.GetLast();
            for (int i = 0; i < model.Questions.Count; i++)
            {
                string nameQuestion = model.Questions[i];
                var question = new Question() { Name = nameQuestion, Test = testLast };
                questions.Add(question);
            }
            foreach (Question question in questions)
                await _questionRepository.AddAsync(question);
            return RedirectToAction("ViewQuestions", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ViewQuestions() 
        {
            var testLast = _testRepository.GetLast();
            var allQuestions = await _questionRepository.GetQuestionsInTest(testLast.Id);
            QuestionsViewModel model = new QuestionsViewModel(allQuestions);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddAnswers(int id) 
        {
            question = _questionRepository.Get(id);
            if (question != null)
                return View();
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddAnswers(CreateAnswersViewModel model, int id) 
        { 
            var question = _questionRepository.Get(id);
            List<Answer> answers = new List<Answer>();
            for (int i = 0;i<model.Answers.Count;i++) 
            {
                string nameAnswer = model.Answers[i];
                string isAnswer = model.IsAnswer[i];
                var answer = new Answer() { Name = nameAnswer, IsAnswer = isAnswer == "Правильный" ? true : false, Question = question };
                answers.Add(answer);
            }
            foreach (Answer answer in answers)
                await _answerRepository.AddAsync(answer);
            return RedirectToAction("ViewQuestions", "Home");

        }


        [HttpGet]
        public IActionResult ViewTests()
            => View();

        [HttpGet]
        public IActionResult ViewStudents()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}