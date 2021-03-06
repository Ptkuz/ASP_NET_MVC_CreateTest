using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Web_Test_II.Models.ViewModels.EditTestViewModels;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

namespace Web_Test_II.Controllers
{
    public class EditTestController : Controller
    {
        private readonly ILogger<EditTestController> _logger;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Result> _resultRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Test> _testRepository;





        public EditTestController(
            ILogger<EditTestController> logger,
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
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> ViewTests()
        {
            var allTests = _testRepository.Items.ToList();
            List<int> countQuest = new List<int>();
            foreach (var test in allTests)
            {
                var questionsInTest = await _questionRepository.GetQuestionsInTest(test.Id);
                countQuest.Add(questionsInTest.Count());
            }

            TestViewModel model = new TestViewModel(allTests, countQuest);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> ViewQuestions(int id)
        {

            ViewBag.Id = id; // Передача id теста в представление

            var findTest = await _testRepository.GetAsync(id);
            var testLast = _testRepository.GetLast();
            Test test = null;
            if (id == 0)
                test = testLast;
            else
                test = findTest;

            var allQuestions = await _questionRepository.GetQuestionsInTest(test.Id);
            var listQuestion = allQuestions.ToList();
            List<int> countAnswers = new List<int>();
            List<int> countTrueAnswers = new List<int>();
            foreach (var question in listQuestion)
            {
                var answersInQuestion = await _answerRepository.GetAnswersInQuestion(question.Id);
                var trueAnswers = answersInQuestion.Where(item => item.IsAnswer == true);
                countAnswers.Add(answersInQuestion.Count());
                countTrueAnswers.Add(trueAnswers.Count());
            }
            QuestionsViewModel model = new QuestionsViewModel(listQuestion, countAnswers, countTrueAnswers);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> AddAnswers(int id)
        {
            ViewBag.id = id;
            var question = await _questionRepository.GetAsync(id);
            var answers = await _answerRepository.GetAnswersInQuestion(question.Id);

            CreateAnswersViewModel model = new CreateAnswersViewModel(answers);

            if (question != null)
                return View(model);
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> AddAnswers(CreateAnswersViewModel model, int id)
        {


            var question = await _questionRepository.GetAsync(id);

            int idTest = question.Test.Id; // ID теста для редиректа на ту же страницу
            List<Answer> answers = new List<Answer>();
            for (int i = 0; i < model.Answers.Count; i++)
            {
                string nameAnswer = model.Answers[i];
                string isAnswer = model.IsAnswer[i];
                var answer = new Answer() { Name = nameAnswer, IsAnswer = isAnswer == "Правильный" ? true : false, Question = question };
                answers.Add(answer);
            }
            foreach (Answer answer in answers)
                await _answerRepository.AddAsync(answer);

            return RedirectToAction("ViewQuestions", "EditTest", new { id = idTest });
        }

        [HttpGet]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> AddQuestions(int id)
        {
            var test = await _testRepository.GetAsync(id);

            if (test != null)
                return View();
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> AddQuestions(CreateQuestionsViewModel model, int id)
        {
            var test = await _testRepository.GetAsync(id);
            List<Question> questions = new List<Question>();
            for (int i = 0; i < model.Questions.Count; i++)
            {
                string nameQuestion = model.Questions[i];
                var question = new Question() { Name = nameQuestion, Test = test };
                questions.Add(question);
            }
            foreach (Question item in questions)
                await _questionRepository.AddAsync(item);

            return RedirectToAction("ViewQuestions", "EditTest", new { id = id });
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> EditQuestion(int id, string parameterName)
        {
            var question = await _questionRepository.GetAsync(id);
            question.Name = parameterName;

            int idTest = question.Test.Id;

            await _questionRepository.UpdateAsync(question);

            return RedirectToAction("ViewQuestions", "EditTest", new { id = idTest });
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> EditAnswer(int id, string parameterName, string parameterTrue)
        {
            var answer = await _answerRepository.GetAsync(id);
            answer.Name = parameterName;
            answer.IsAnswer = parameterTrue == "Правильно" ? true : false;
            int idQuestion = answer.Question.Id;
            await _answerRepository.UpdateAsync(answer);

            return RedirectToAction("ViewAnswers", "EditTest", new { id = idQuestion });
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> EditTest(int id, string parameterName)
        {
            var test = await _testRepository.GetAsync(id);
            test.Name = parameterName;
            await _testRepository.UpdateAsync(test);

            return RedirectToAction("ViewTests", "EditTest");
        }


        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            int idTest=0;
            try
            {
                idTest = await _questionRepository.RemoveQuestionAsync(id);

                return RedirectToAction("ViewQuestions", "EditTest", new { id = idTest });
            }
            catch (SqlException)
            {
                return RedirectToAction("ViewQuestions", "EditTest", new { id = idTest });
            }
        }

        [HttpGet]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> ViewAnswers(int id)
        {
            ViewBag.id = id;
            var answers = await _answerRepository.GetAnswersInQuestion(id);

            AnswersViewModel model = new AnswersViewModel(answers);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            int idQuestion = 0;
            try
            {
                idQuestion = await _answerRepository.RemoveAnswerAsync(id);
                return RedirectToAction("ViewAnswers", "EditTest", new { id = idQuestion });
            }
            catch (SqlException)
            {
                return RedirectToAction("ViewAnswers", "EditTest", new { id = idQuestion });
            }
        }


        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            try
            {
                await _testRepository.RemoveAsync(id);
                return RedirectToAction("ViewTests", "EditTest");
            }
            catch (SqlException)
            {
                return RedirectToAction("ViewTests", "EditTest");
            }
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> ActiveTest(int id)
        {
            var test = await _testRepository.GetAsync(id);
            bool trueAnswers = await _testRepository.GetCountTrueAnswers(id);
            if (trueAnswers)
            {
                test.IsAvtive = true;
                await _testRepository.UpdateAsync(test);
            }
            else
            {
                ViewBag.Message = "Вы не можете активировать тест, пока во всех вопросах не будет задано хотя бы одного правильного ответа!";
            }

            return RedirectToAction("ViewTests", "EditTest");
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public async Task<IActionResult> DeactiveTest(int id)
        {
            var test = await _testRepository.GetAsync(id);
            test.IsAvtive = false;
            await _testRepository.UpdateAsync(test);

            return RedirectToAction("ViewTests", "EditTest");
        }


    }
}
