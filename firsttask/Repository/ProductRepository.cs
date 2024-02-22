using firsttask.Data;
using firsttask.Models;
using firsttask.Models.Base;
using firsttask.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace firsttask.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyFirstContext context) : base(context)
        {
        }
        override public async Task<int> AddAsync(Product product)
        {
            if (!((product.CategoryId > 0) && (product.CategoryId < await context.Products.CountAsync())))
                return -1;//return -1 if an error accures
            return await base.AddAsync(product);
        }
        override public async Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> myproducts = await context.Products.Include(l => l.Category).ToListAsync();
            return myproducts;
        }

        override public async Task<Product> GetByIdAsync(int id)
        {
            var myproduct = await context.Products.Include(l => l.Category).FirstOrDefaultAsync(x => x.Id == id);
            return myproduct;
        }

        public async Task<List<ProductReport>> ReportAllAsync()
        {
            List<Product> products = context.Products.Include(l => l.Category).ToList();
            products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
            ProductReport productReport;
            List<ProductReport> productsReport = new List<ProductReport>();
            foreach (var product in products)
            {
                productReport = new ProductReport();
                var q = await context.Resids.Where(x => x.ProductId == product.Id).ToListAsync();
                if (q.Count > 0)
                {
                    var c = await context.Havales.Where(x => x.ProductId == product.Id).ToListAsync();
                    int qsum = 0;
                    int csum = 0;
                    foreach (var x in q)
                    {
                        qsum += x.Quantity;
                    }
                    foreach (var x in c)
                    {
                        csum += x.Quantity;
                    }
                    productReport.Quantity = qsum - csum;
                }
                else
                {
                    productReport.Quantity = 0;
                }
                productReport.CategoryName = product.Category.CategoryName;
                productReport.ProductName = product.ProductName;
                productsReport.Add(productReport);
            }
            return productsReport;
        }

        public async Task<ProductReport> ReportOneAsync(int productId)
        {
            var products = await context.Products.Include(l => l.Category).ToListAsync();
            var product = products.SingleOrDefault(x => x.Id == productId);
            ProductReport productReport = new ProductReport();
            productReport.ProductName = product.ProductName;
            productReport.CategoryName = product.Category.CategoryName;
            var q = await context.Resids.Where(x => x.ProductId == product.Id).ToListAsync();
            if (q.Count > 0)
            {
                var c = await context.Havales.Where(x => x.ProductId == product.Id).ToListAsync();
                int qsum = 0;
                int csum = 0;
                foreach (var x in q)
                {
                    qsum += x.Quantity;
                }
                foreach (var x in c)
                {
                    csum += x.Quantity;
                }
                productReport.Quantity = qsum - csum;
            }
            else
            {
                productReport.Quantity = 0;
            }
            return productReport;
        }

        override public async Task UpdateAsync(Product product)
        {
            if (!((product.CategoryId > 0) && (product.CategoryId < await context.Products.CountAsync())))
                throw new ArgumentNullException(nameof(product));
            await base.UpdateAsync(product);
        }
    }
}
