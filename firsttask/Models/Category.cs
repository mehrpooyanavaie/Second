using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace firsttask.Models
{
    public class Category:Base.Entity
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
