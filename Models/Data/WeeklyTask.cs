using System.ComponentModel.DataAnnotations;

namespace weekly_tasks.Models.Data
{
    public class WeeklyTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("documentar|reportes|reunion|operacion|capacitacion")]
        public string TaskName { get; set; }

    }
}