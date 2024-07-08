using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Models
{
    [Table("MuscleGroups")]
    public class MuscleGroups
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PrimaryMuscleGroup { get; set; } = null!;

    }
}
