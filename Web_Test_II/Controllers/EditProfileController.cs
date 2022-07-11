using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Controllers;
using Web_Test_II.Models.ViewModels.EditProfile;
using Web_Test_II_DAL.Models;


namespace Web_Test_II.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Mentor> _mentorRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Role> _roleRepository;

        public EditProfileController
            (
            IRepository<User> userRepository,
            IRepository<Student> studentRepository,
            IRepository<Mentor> mentorRepository,
            IRepository<Position> positionRepository,
            IRepository<Group> groupRepository,
            IRepository<Role> roleRepository
            ) 
        {
           _userRepository = userRepository;
            _studentRepository = studentRepository;
            _mentorRepository = mentorRepository;
            _positionRepository = positionRepository;
            _groupRepository = groupRepository;
            _roleRepository = roleRepository;     
        }


        [HttpGet]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> EditMentorProfile() 
        {
            int idMentor = AccountController.Mentor.Id;
            int idUser = AccountController.User.Id;

            var mentor = await _mentorRepository.GetAsync(idMentor);
            var user = await _userRepository.GetAsync(idUser);
            var positions = _positionRepository.Items.ToList();

            EditMentorProfileViewModel model = new EditMentorProfileViewModel(mentor, user, positions);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> EditMentorProfile(EditMentorProfileViewModel model, int positionId) 
        {
            var mentor = model.Mentor;
            var user = model.User;

            var mentorDB = await _mentorRepository.GetAsync(AccountController.Mentor.Id);
            var userDB = await _userRepository.GetAsync(AccountController.User.Id);
            var position = await _positionRepository.GetAsync(positionId);

            mentorDB.Surname = mentor.Surname;
            mentorDB.Name = mentor.Name;
            mentorDB.Patronymic = mentor.Patronymic;
            mentorDB.Position = position;

            userDB.Email = user.Email;
            userDB.Password = user.Password;

            await _mentorRepository.UpdateAsync(mentorDB);
            await _userRepository.UpdateAsync(userDB);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> EditStudentProfile()
        {
            int idStudent = AccountController.Student.Id;
            int idUser = AccountController.User.Id;

            var student = await _studentRepository.GetAsync(idStudent);
            var user = await _userRepository.GetAsync(idUser);
            var groups = _groupRepository.Items.ToList();

            EditStudentProfileViewModel model = new EditStudentProfileViewModel(student, user, groups);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> EditStudentProfile(EditStudentProfileViewModel model, int groupId)
        {
            var student = model.Student;
            var user = model.User;

            var studentDB = await _studentRepository.GetAsync(AccountController.Student.Id);
            var userDB = await _userRepository.GetAsync(AccountController.User.Id);
            var group = await _groupRepository.GetAsync(groupId);

            studentDB.Surname = student.Surname;
            studentDB.Name = student.Name;
            studentDB.Patronymic = student.Patronymic;
            studentDB.Group = group;

            userDB.Email = user.Email;
            userDB.Password = user.Password;

            await _studentRepository.UpdateAsync(studentDB);
            await _userRepository.UpdateAsync(userDB);
            return RedirectToAction("Index", "Home");
        }

    }

}
