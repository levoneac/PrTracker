using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Models
{
    [Table("Lifts")]
    public class Lifts
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LiftName { get; set; } = null!;

        [Required]
        public MuscleGroups PrimaryMuscleGroupId { get; set; } = null!;
        public MuscleGroups? SecondaryMuscleGroupId { get; set; }

        public ICollection<RecordedLifts> RecordedLifts { get; set; } = null!;
    }
}
