using firsttask.Data;
using firsttask.Models;
using firsttask.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace firsttask.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyFirstContext context) : base(context)
        {

        }
        override public async Task<bool> DeleteByIdAsync(int id)
        {
            var mycategory = await context.Categories.FindAsync(id);
            if (mycategory == null)
                return false;
            if (!mycategory.Products.IsNullOrEmpty())//in database cascade
                return false;
            return await base.DeleteByIdAsync(id);
        }

        override public async Task<IEnumerable<Category>> GetAllAsync()
        {
            IEnumerable<Category> categories = await context.Categories.Include(l => l.Products).ToListAsync();
            return categories;
        }
        override public async Task<Category> GetByIdAsync(int id)
        {
            var mycategory = await context.Categories.Include(l => l.Products).SingleOrDefaultAsync(x => x.Id == id);
            return mycategory;
        }
    }
}
