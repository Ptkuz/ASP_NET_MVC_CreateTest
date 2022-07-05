using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Test_II.Models;
using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.CreateTestViewModels;

namespace Web_Test_II.Controllers
{
    public class CreateTestController : Controller
    {
        private readonly ILogger<CreateTestController> _logger;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Result> _resultRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Test> _testRepository;





        public CreateTestController(
            ILogger<CreateTestController> logger,
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
        public IActionResult CreateTest()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestViewModel model)
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
            return RedirectToAction("ViewQuestions", "EditTest");
        }

       


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