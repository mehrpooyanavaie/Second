using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class PostProductModel
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public DateTime ExpireTime { get; set; }
    }
}
