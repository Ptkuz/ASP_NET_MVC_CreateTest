using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Test_II_Interfaces
{
    public interface IRepository<T> where T: class, IEntity, new()
    {
        IQueryable<T> Items { get; }
        T Get(int id);
        T GetLast();

        Task<IQueryable<T>> GetQuestionsInTest(int idTest, CancellationToken Cancel = default);
        Task<IQueryable<T>> GetAnswersInQuestion(int idQuestion, CancellationToken Cancel = default);
        Task<T> GetAsync(int id, CancellationToken Cancel = default);
        Task<T> AddAsync(T item, CancellationToken Cancel = default);
        Task UpdateAsync(T item, CancellationToken Cancel = default);
        Task<int> RemoveQuestionAsync(int id, CancellationToken Cancel = default);
        Task<int> RemoveAnswerAsync(int id, CancellationToken Cancel = default);
        Task RemoveAsync(int id, CancellationToken Cancel = default);
        Task<bool> GetCountTrueAnswers(int idTest, CancellationToken Cancel = default);
        Task<IQueryable<T>> GetAvailableTests();
        Task<List<int>> GetTrueAnswers(int idQuestion, CancellationToken Cancel = default);
        Task<T> GetUserAsync(string email, string password, CancellationToken Cancel = default);
        Task<T> GetUserAsync(string email, CancellationToken Cancel = default);
        Task<T> GetMentorOfUserAsync(int idUser, CancellationToken Cancel = default);
        Task<T> GetStudentOfUserAsync(int idUser, CancellationToken Cancel = default);
        Task<IQueryable<object>> GetResultsStudentAsync(int idStudent, CancellationToken Cancel = default);
        Task<IQueryable<object>> GetAllResultstAsync(CancellationToken Cancel = default);
        void DisposeContextAsync();
        Task SaveChangesAsync();
    }
}
