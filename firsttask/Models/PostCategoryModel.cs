using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class PostCategoryModel
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
    }
}
