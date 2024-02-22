using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using firsttask.Data;

namespace firsttask.Models
{
    public class Product:Base.Entity
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
        [Required]
        public DateTime ExpireTime { get; set; }
    }
}
