using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.UserViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web_Test_II.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Web_Test_II.Controllers
{
    public class AccountController : Controller 
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IUserRegistration _userRegistration;
        
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Mentor> _mentorRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Role> _roleRepository;

        public static User? User { get; set; }
        public static Mentor? Mentor { get; set; }
        public static Student? Student { get; set; }




        public AccountController(
            ILogger<AccountController> logger,
            IUserRegistration userRegistration,
            IRepository<User> userRepository,
            IRepository<Student> studentRepository,
            IRepository<Mentor> mentorRepository,
            IRepository<Position> positionRepository,
            IRepository<Group> groupRepository,
            IRepository<Role> roleRepository
            
            )
        {
            _logger = logger;
            _userRegistration = userRegistration;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _mentorRepository = mentorRepository;
            _positionRepository = positionRepository;
            _groupRepository = groupRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult RegisterMentor() 
        {
            var positions = _positionRepository.Items;
            RegisterMentorViewModel model = new RegisterMentorViewModel(positions);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMentor(RegisterMentorViewModel model, int positionId) 
        {
            var positions = _positionRepository.Items;
            model.Positions = positions;
            if (ModelState.IsValid)
            {
                User? userFind = await _userRepository.GetUserAsync(model.Email); // Ищем, есть ли такой user в базе данных
                if (userFind != null)
                    ModelState.AddModelError("Email", "Некорректные логин и(или) пароль");
                else
                {
                    var user = new User();                   
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.RoleId = 3;
                    await _userRepository.AddAsync(user);
                    var mentor = new Mentor();
                    var position = await _positionRepository.GetAsync(positionId);
                    var userDb = await _userRepository.GetUserAsync(model.Email, model.Password);
                    mentor.Surname = model.Surname;
                    mentor.Name = model.Name;
                    mentor.Patronymic = model.Patronymic;
                    mentor.Position = position;
                    mentor.UserKey = userDb.Id;
                    await _mentorRepository.AddAsync(mentor);
                    await _userRegistration.SendEmailAsync(user, mentor);
                    return RedirectToAction("Index", "Home");
                }
            }   
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult RegisterStudent()
        {
            var groups = _groupRepository.Items;
            RegisterStudentViewModel model = new RegisterStudentViewModel(groups);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel model, int groupId)
        {
            var groups = _groupRepository.Items;
            model.Groups = groups;
            if (ModelState.IsValid)
            {
                User? userFind = await _userRepository.GetUserAsync(model.Email); // Ищем, есть ли такой user в базе данных
                if (userFind != null)
                    ModelState.AddModelError("Email", "Некорректные логин и(или) пароль");
                else
                {
                    var user = new User();
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.RoleId = 2;
                    await _userRepository.AddAsync(user);
                    var student = new Student();
                    var group = await _groupRepository.GetAsync(groupId);
                    var userDb = await _userRepository.GetUserAsync(model.Email, model.Password);
                    student.Surname = model.Surname;
                    student.Name = model.Name;
                    student.Patronymic = model.Patronymic;
                    student.Group = group;
                    student.UserKey = userDb.Id;
                    await _studentRepository.AddAsync(student);
                    await _userRegistration.SendEmailAsync(user, student);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userRepository.GetUserAsync(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }


        private async Task Authenticate(User user)
        {
            var idRole = user.RoleId;
            var role = await _roleRepository.GetAsync(idRole);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            var mentor = await _mentorRepository.GetMentorOfUserAsync(user.Id);
            var student = await _studentRepository.GetStudentOfUserAsync(user.Id);
            User = user;
            Mentor = mentor;
            Student = student;
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        public async Task<IActionResult> Logout()
        {
            User = null;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
