using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
