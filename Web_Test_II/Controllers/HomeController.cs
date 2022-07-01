using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Test_II.Models;
using Web_Test_II_DAL;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

namespace Web_Test_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRepository<Student> _studentRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Student> studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
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
            => View();

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