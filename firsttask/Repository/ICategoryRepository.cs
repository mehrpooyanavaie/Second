using firsttask.Models;
using firsttask.Repository.Base;

namespace firsttask.Repository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        //Task<List<CategoryViewModel>> GetAllAsync(int page, int pagesize);
        //Task<CategoryViewModel> GetByIdAsync(int id);
        //Task<int> AddAsync(PostCategoryModel category);
        //Task<bool> DeleteAsync(int id);
        //Task<int> UpdateAsync(int id, PostCategoryModel category);
    }
}
