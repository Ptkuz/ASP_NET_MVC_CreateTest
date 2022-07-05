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
        Task SaveChangesAsync();
    }
}
