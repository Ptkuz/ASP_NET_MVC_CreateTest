using Microsoft.AspNetCore.Mvc;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.TestingViewModel;

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
        public async Task<IActionResult> OpenTest(int id) 
        {
           
            var questions = await _questionRepository.GetQuestionsInTest(id);
            OpenTestViewModel model = new OpenTestViewModel(questions);
            return View(model);
        }
    }
}
