using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes.Base;
using Web_Test_II_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Web_Test_II_DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        private bool AutoSaveChanges { get; set; } = true;

        public DbRepository(WebTestDB db) 
        {
            _context = db;
            _entities = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _entities;

        public T Get(int id)=>
            Items.SingleOrDefault(item => item.Id == id);


        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) =>
            await Items.SingleOrDefaultAsync(item => item.Id == id, Cancel).ConfigureAwait(false);


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

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _context.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(Cancel);
        }
    }
}
