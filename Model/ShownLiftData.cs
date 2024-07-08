using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Model
{
    public class ShownLiftData
    {
        public string LiftName { get; set; }
        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public int PrimaryMuscleGroup { get; set; }
        public int SecondaryMuscleGroup { get; set; }
        public DateTime Date { get; set; }
    
    }
}
