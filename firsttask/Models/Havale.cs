namespace firsttask.Models
{
    public class Havale
    {
        public int Id { get; set; }
        public DateTime HavaleTime { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int PriceOfOne { get; set; }
        public int PriceOfAll { get; set; }
    }
}
