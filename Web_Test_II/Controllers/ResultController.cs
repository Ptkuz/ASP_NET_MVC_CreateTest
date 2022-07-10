using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;
using Web_Test_II.Models.ViewModels.ResultViewModel;
using Web_Test_II_DAL.Models;

namespace Web_Test_II.Controllers
{
    public class ResultController : Controller
    {
        private readonly ILogger<ResultController> _logger;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Result> _resultRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Test> _testRepository;
        public ResultController(
            ILogger<ResultController> logger,
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
        [Authorize(Roles = "student")]
        public async Task<IActionResult> ViewResultsStudent()
        {
            var idStudent = AccountController.Student.Id;
            var resultStudent = await _resultRepository.GetResultsStudentAsync(idStudent);
            ResultStudentViewModel model = null;
            if (resultStudent != null) 
            {
                model = new ResultStudentViewModel(resultStudent as IQueryable<GroupResultsStudents>);
                return View(model);
            }
            else
                return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetResults(IFormCollection keys, int id) 
        {
            int score = 0;
            string[] questionsId = keys["questionId"];
            foreach (var questionId in questionsId)
            {
                var allAnswers = await _answerRepository.GetAnswersInQuestion(Int32.Parse(questionId)); // List всех ответов
                var answersCurrent = await _questionRepository.GetTrueAnswers(Int32.Parse(questionId)); // List правильных ответов
                bool checkTextBox = allAnswers.Count() == answersCurrent.Count(); // Если количество ответов равно количеству правильных ответов, значит это поля textbox
                string[] answers = keys["question_" + questionId]; // Массив выбранных ответов пользователя
                int countAnswer = 0; // Счетчик правильных ответов
                foreach (var answer in answers)
                {
                    if (checkTextBox)
                    {
                        var firstAnswer = allAnswers.First();
                        if (firstAnswer.Name == answer)
                            countAnswer++;
                    }
                    else
                    {
                        bool answerParse = Int32.TryParse(answer, out int idAnswer);
                        if (answerParse)
                        {
                            if (answersCurrent.Contains(idAnswer))
                                countAnswer++;
                        }
                    }
                }
                if (answersCurrent.Count == countAnswer)
                    score++;

            }
            var student = AccountController.Student;
            var test = await _testRepository.GetAsync(id);
            Result result = new Result();
            result.Student = student;
            result.Test = test;
            result.Points = score;

            await _resultRepository.AddAsync(result);
            var resultStudent = await _resultRepository.GetResultsStudentAsync(student.Id);
            ResultStudentViewModel model = new ResultStudentViewModel(resultStudent as IQueryable<GroupResultsStudents>);
            return RedirectToAction("ViewResultsStudent", "Result");
        }

        [HttpGet]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> ViewAllResults()
        {
            var resultStudent = _resultRepository.GetAllResultstAsync();
            ResultStudentViewModel model = new ResultStudentViewModel(resultStudent as IQueryable<GroupResultsStudents>);      
                return View(model);
        }

    }
}
