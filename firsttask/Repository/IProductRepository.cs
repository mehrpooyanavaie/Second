using firsttask.Models;
using firsttask.Repository.Base;

namespace firsttask.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<ProductReport>> ReportAllAsync();
        Task<ProductReport> ReportOneAsync(int productId);
    }
}
