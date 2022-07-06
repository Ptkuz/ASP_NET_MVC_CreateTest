using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Test_II.Models;
using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.CreateTestViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Web_Test_II.Controllers
{
    public class CreateTestController : Controller
    {
        private readonly ILogger<CreateTestController> _logger;

        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Test> _testRepository;





        public CreateTestController(
            ILogger<CreateTestController> logger,
            IRepository<Question> questionRepository,
            IRepository<Test> testRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;
            _testRepository = testRepository;

        }

        [HttpGet]
        [Authorize(Roles = "mentor")]
        public IActionResult CreateTest()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}