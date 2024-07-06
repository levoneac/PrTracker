using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Models
{
    class LiftTypes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LiftName { get; set; } = null!;

        [Required]
        public string LiftType { get; set; } = null!;

        public ICollection<RecordedLifts> RecordedLifts { get; set; } = null!;
    }
}
