using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using weekly_tasks.Entities;

namespace weekly_tasks.Models.Data
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [ForeignKey("User")]
        public string? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
