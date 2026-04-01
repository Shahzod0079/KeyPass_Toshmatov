using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyPass_Toshmatov.Models
{
    public class Storage
    {
        [Key]

        public int Id { get; set; }

        public string Name { get; set; }
        public string? Url { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public virtual User user { get; set; }
        public int UserId { get; internal set; }
    }
}
