using firsttask.Models;

namespace firsttask.Repository
{
    public interface IHavaleRepository
    {
        Task<int> AddHavaleAsync(HavaleViewModel havaleViewModel);
    }
}
