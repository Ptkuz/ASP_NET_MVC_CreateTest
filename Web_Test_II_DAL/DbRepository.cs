using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes.Base;
using Web_Test_II_Interfaces;
using Microsoft.EntityFrameworkCore;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II_DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;
        private readonly DbSet<Question> questions;
        private readonly DbSet<Answer> answers;

        private bool AutoSaveChanges { get; set; } = true;

        public DbRepository(WebTestDB db)
        {
            _context = db;
            _entities = db.Set<T>();
            questions = db.Set<Question>();
            answers = db.Set<Answer>();
        }

        public virtual IQueryable<T> Items => _entities;
        public virtual IQueryable<Question> Questions => questions;
        public virtual IQueryable<Answer> Answers => answers;

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
            var items = await Questions.Where(item => item.Test.Id == idTest).ToListAsync();
            return (IQueryable<T>)items.AsQueryable();

        }

        public async Task<IQueryable<T>> GetAnswersInQuestion(int idQuestion, CancellationToken Cancel = default)
        {
            var items = await Answers.Where(item => item.Question.Id == idQuestion).ToListAsync();
            return (IQueryable<T>)items.AsQueryable();

        }

        public async Task<bool> GetCountTrueAnswers(int idTest, CancellationToken Cancel  =default) 
        {
            int countTrue = 0;
            var questions = await Questions.Where(item => item.Test.Id == idTest).ToListAsync();
            var countQuestions = questions.Count;
            foreach (var question in questions) 
            {
                var answers = await Answers.Where(item => item.Question.Id == question.Id && item.IsAnswer == true).ToListAsync();
                if(answers.Count > 0)
                    countTrue++;
            }
            if(countQuestions==countTrue && questions.Count != 0)
                return true;
            return false;
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
            _context.Remove(new T { Id = id});
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

        public async Task SaveChangesAsync()=>
            await _context.SaveChangesAsync().ConfigureAwait(false);

        
    }
}
