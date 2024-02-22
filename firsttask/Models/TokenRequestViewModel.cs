using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class TokenRequestViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required] 
        public string RefreshToken { get; set; }
    }
}
