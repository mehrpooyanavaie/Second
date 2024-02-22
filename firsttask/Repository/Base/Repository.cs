using firsttask.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace firsttask.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : Models.Base.Entity
    {
        internal readonly MyFirstContext context;
        internal DbSet<T> dbset { get; set; }
        public Repository(MyFirstContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbset = context.Set<T>();
        }
        public virtual async Task<int> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await dbset.AddAsync(entity);
            return entity.Id;
        }
        //public virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        //{
        //    return dbset.Where(expression);
        //}
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await dbset.ToListAsync();
            return result;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(keyValues: id);
        }
        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Task.Run(() =>
            {
                dbset.Remove(entity);
            });
        }
        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            if (entity == null)
                return false;
            await DeleteAsync(entity);
            return true;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Task.Run(() =>
            {
                dbset.Update(entity);
            });
        }
    }
}
