using PrTracker.Data;
using PrTracker.Helpers;
using PrTracker.MVVM;
using PrTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Model
{
    public class ShownLiftData() : ViewModelBase
    {
        private readonly LiftToMuscleGroupRelations liftToMuscleGroups = LiftToMuscleGroupRelations.GetLiftToMuscleGroupRelations();
        private string liftName;
        private string primaryMuscleGroup;
        private string secondaryMuscleGroup;

        public int LiftNameFK { get; set; }
        public string LiftName
        {
            get { return liftName; }
            set
            {
                liftName = value;
                PrimaryMuscleGroup = liftToMuscleGroups.FromLiftToMuscleGroup(value).Key;
                SecondaryMuscleGroup = liftToMuscleGroups.FromLiftToMuscleGroup(value).Value;

                OnPropertyChanged();
            }
        }
        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public string PrimaryMuscleGroup
        {
            get { return primaryMuscleGroup; }
            set
            {
                primaryMuscleGroup = value;
                OnPropertyChanged();
            }
        }
        public string SecondaryMuscleGroup
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
