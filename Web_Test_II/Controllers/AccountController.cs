using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.UserViewModels;

namespace Web_Test_II.Controllers
{
    public class AccountController : Controller 
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Mentor> _mentorRepository;
        private readonly IRepository<Position> _positionRepository;




        public AccountController(
            ILogger<AccountController> logger,
            IRepository<User> userRepository,
            IRepository<Student> studentRepository,
            IRepository<Mentor> mentorRepository,
            IRepository<Position> positionRepository
            )
        {
            _logger = logger;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _mentorRepository = mentorRepository;
            _positionRepository = positionRepository;
        }

        [HttpGet]
        public IActionResult RegisterMentor() 
        {
            var positions = _positionRepository.Items;
            RegisterMentorViewModel model = new RegisterMentorViewModel(positions);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMentor(RegisterMentorViewModel model, int position) 
        {


            return View(model);
        }


    }
}
