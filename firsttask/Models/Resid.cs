using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class Resid
    {
        public int Id { get; set; }
        public DateTime ResidTime { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int PriceOfOne { get; set; }
        public int PriceOfAll { get; set; }
    }
}
