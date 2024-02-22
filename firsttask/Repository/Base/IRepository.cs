using System.Linq.Expressions;
using System.Threading.Tasks;

namespace firsttask.Repository.Base
{
    public interface IRepository<T> where T : Models.Base.Entity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);


    }
}
