using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firsttask.Models
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; } //because id in applicationuser is TKey
        public string Token { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateExpire { get; set; }
        public string UserId { get; set; }//because id in applicationuser is TKey
        [ForeignKey(nameof(UserId))]
        public AppliacationUser User { get; set; }

    }
}
