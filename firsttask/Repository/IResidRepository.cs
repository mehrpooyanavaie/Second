using firsttask.Models;

namespace firsttask.Repository
{
    public interface IResidRepository
    {
        public Task<int> AddResidAsync(ResidViewModel residViewModel);
    }
}
