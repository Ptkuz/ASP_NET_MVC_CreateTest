using Microsoft.AspNetCore.Mvc;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

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


       
    }
}
