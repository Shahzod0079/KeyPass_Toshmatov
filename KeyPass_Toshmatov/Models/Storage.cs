using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyPass_Toshmatov.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Внешний ключ
        public int UserId { get; set; }

        // Навигационное свойство
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}