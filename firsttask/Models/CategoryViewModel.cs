using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductsNames { get; set; }
    }
}
