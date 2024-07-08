using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Models
{
    [Table("RecordedLifts")]
    public class RecordedLifts
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Lifts Lift { get; set; } = null!;

        [Required]
        public int Reps { get; set; }

        [Required]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Weight { get; set; }

        [Required]
        public DateTime DayOfLift { get; set; }

        [Required]
        public People LifterId { get; set; } = null!;
    }
}
