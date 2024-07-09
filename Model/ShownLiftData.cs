using PrTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Model
{
    public class ShownLiftData : ViewModelBase
    {
        private string liftName;
        private int primaryMuscleGroup;
        private int secondaryMuscleGroup;

        public int LiftNameFK { get; set; }
        public string LiftName
        {
            get { return liftName; }
            set
            {
                liftName = value;
                PrimaryMuscleGroup = 111;
                OnPropertyChanged();
            }
        }
        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public int PrimaryMuscleGroup
        {
            get { return primaryMuscleGroup; }
            set
            {
                primaryMuscleGroup = value;
                OnPropertyChanged();
            }
        }
        public int SecondaryMuscleGroup
        {
            get { return secondaryMuscleGroup; }
            set
            {
                secondaryMuscleGroup = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date { get; set; }
        public bool IsNew { get; set; }
    
    }
}
