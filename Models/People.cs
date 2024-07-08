using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Models
{
    [Table("People")]
    public class People
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime DateOfRegistration { get; set; }

        public ICollection<RecordedLifts> Lifts { get; set; } = null!;

    }
}
