using System.ComponentModel.DataAnnotations;

namespace KeyPass_Toshmatov.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? LastAuth { get; set; }
    }
}