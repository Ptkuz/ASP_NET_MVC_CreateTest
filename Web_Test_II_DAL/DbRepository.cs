using Microsoft.EntityFrameworkCore;
using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_DAL.Entityes.Base;
using Web_Test_II_Interfaces;

namespace Web_Test_II_DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;
        private readonly DbSet<Question> questions;
        private readonly DbSet<Answer> answers;
        private readonly DbSet<Test> tests;
        private readonly DbSet<Mentor> mentors;
        private readonly DbSet<Student> students;
        private readonly DbSet<User> users;
        private readonly DbSet<Result> results;
        private readonly DbSet<Group> groups;

        private bool AutoSaveChanges { get; set; } = true;

        public DbRepository(WebTestDB db)
        {
            _context = db;
            _entities = db.Set<T>();
            questions = db.Set<Question>();
            answers = db.Set<Answer>();
            tests = db.Set<Test>();
            mentors = db.Set<Mentor>();
            students = db.Set<Student>();
            users = db.Set<User>();
            results = db.Set<Result>();
            groups = db.Set<Group>();

        }

        public virtual IQueryable<T> Items => _entities;
        public virtual IQueryable<Question> Questions => questions;
        public virtual IQueryable<Answer> Answers => answers;
        public virtual IQueryable<Test> Tests => tests;
        public virtual IQueryable<Mentor> Mentors => mentors;
        public virtual IQueryable<Student> Students => students;
        public virtual IQueryable<User> Users => users;
        public virtual IQueryable<Result> Results => results;
        public virtual IQueryable<Group> Groups => groups;

        public T Get(int id)
        {

            T item = Items.SingleOrDefault(item => item.Id == id);
            return item;
        }


        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) =>
            await Items.SingleOrDefaultAsync(item => item.Id == id, Cancel).ConfigureAwait(false);



        public T GetLast() =>
             Items.ToList().Last();

        public async Task<IQueryable<T>> GetQuestionsInTest(int idTest, CancellationToken Cancel = default)
        {

            var items = await Questions.Where(item => item.Test.Id == idTest).Include(item => item.Answers).ToListAsync();
            return (IQueryable<T>)items.AsQueryable();

        }

        public async Task<IQueryable<T>> GetAnswersInQuestion(int idQuestion, CancellationToken Cancel = default)
        {
            var items = await Answers.Where(item => item.Question.Id == idQuestion).ToListAsync();
            return (IQueryable<T>)items.AsQueryable();

        }

        public async Task<bool> GetCountTrueAnswers(int idTest, CancellationToken Cancel = default)
        {
            int countTrue = 0;
            var questions = await Questions.Where(item => item.Test.Id == idTest).ToListAsync();
            var countQuestions = questions.Count;
            foreach (var question in questions)
            {
                var answers = await Answers.Where(item => item.Question.Id == question.Id && item.IsAnswer == true).ToListAsync();
                if (answers.Count > 0)
                    countTrue++;
            }
            if (countQuestions == countTrue && questions.Count != 0)
                return true;
            return false;
        }

        public async Task<IQueryable<T>> GetAvailableTests()
        {
            var items = await Tests.Where(item => item.IsAvtive == true).ToListAsync();
            return (IQueryable<T>)items.AsQueryable();
        }

        public async Task<List<int>> GetTrueAnswers(int idQuestion, CancellationToken Cancel = default)
        {
            List<int> idTrueAnswers = new List<int>();
            var answers = await Answers.Where(item => item.Question.Id == idQuestion && item.IsAnswer == true).ToListAsync();
            foreach (var answer in answers)
            {
                idTrueAnswers.Add(answer.Id);
            }
            return idTrueAnswers;
        }

        public async Task<T> GetUserAsync(string email, string password, CancellationToken Cancel = default)
        {
            User user = await Users.FirstOrDefaultAsync(items => items.Email == email && items.Password == password).ConfigureAwait(false);
            if (user != null)
            {
                T genericUser = user as T;
                return genericUser;
            }
            return null;
        }

        public async Task<T> GetUserAsync(string email, CancellationToken Cancel = default)
        {
            User user = await Users.FirstOrDefaultAsync(items => items.Email == email).ConfigureAwait(false);
            if (user != null)
            {
                T genericUser = user as T;
                return genericUser;
            }
            return null;
        }

        public async Task<T> GetMentorOfUserAsync(int idUser, CancellationToken Cancel = default) 
        { 
            Mentor mentor = await Mentors.Include(item => item.Position).FirstOrDefaultAsync(items => items.UserKey == idUser).ConfigureAwait(false);
            if (mentor != null) 
            {
                T genericMentor = mentor as T;
                return genericMentor;
            }
            return null;
        }

        public async Task<T> GetStudentOfUserAsync(int idUser, CancellationToken Cancel = default)
        {
            Student student = await Students.Include(item=>item.Group).FirstOrDefaultAsync(items => items.UserKey == idUser).ConfigureAwait(false);
            if (student != null)
            {
                T genericStudent = student as T;
                return genericStudent;
            }
            return null;
        }

        public async Task<IQueryable<T>> GetResultsStudentAsync(int idStudent, CancellationToken Cancel = default) 
        {
            var resultStudent = await Results.Include(item=>item.Test.Questions).Include(item=>item.Student).Where(item => item.Student.Id==idStudent).ToListAsync();
            return (IQueryable<T>)resultStudent.AsQueryable();
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _context.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _context.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<int> RemoveQuestionAsync(int id, CancellationToken Cancel = default)
        {
            var question = await Questions.AsNoTracking().Include(item => item.Test).
                FirstOrDefaultAsync(item => item.Id == id).
                ConfigureAwait(false);
            int idTest = question.Test.Id;
            _context.Remove(new T { Id = id });

            if (AutoSaveChanges)
                await _context.SaveChangesAsync(Cancel);
            return idTest;
        }

        public async Task<int> RemoveAnswerAsync(int id, CancellationToken Cancel = default)
        {
            var answer = await Answers.AsNoTracking().Include(item => item.Question).
                FirstOrDefaultAsync(item => item.Id == id).
                ConfigureAwait(false);
            int idQuestrion = answer.Question.Id;
            _context.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(Cancel);
            return idQuestrion;

        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _context.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(Cancel);
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync().ConfigureAwait(false);

        public void DisposeContextAsync()
        {
            _context.Dispose();
        }


    }
}
