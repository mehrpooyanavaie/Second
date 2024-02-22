using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class AppliacationUser : IdentityUser
    {
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(20)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Custom { get; set; }
    }
}
