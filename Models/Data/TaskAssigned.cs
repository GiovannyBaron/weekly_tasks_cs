using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using weekly_tasks.Entities;

namespace weekly_tasks.Models.Data
{
    public class TaskAssigned
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }

        public int Week { get; set; }

        [ForeignKey("CreatorUser"), Column(Order = 0)]
        public string? CreatorUserId { get; set; }

        [ForeignKey("UserWhoOwns"), Column(Order = 1)]
        public string? UserWhoOwnsId { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public virtual User CreatorUser { get; set; }
        public virtual User UserWhoOwns { get; set; }
        public virtual WeeklyTask Task { get; set; }
    }
}
