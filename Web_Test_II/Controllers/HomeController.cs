using Microsoft.AspNetCore.Mvc;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

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
    }
}
