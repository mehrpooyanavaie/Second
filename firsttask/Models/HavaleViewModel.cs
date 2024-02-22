using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class HavaleViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
