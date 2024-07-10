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
        private readonly LiftRelationConversions liftRealtions = LiftRelationConversions.GetLiftToMuscleGroupRelations();
        private string liftName;
        private string primaryMuscleGroup;
        private string secondaryMuscleGroup;

        public int Id { get; set; }
        public int LiftNameFK { get; set; }
        public string LiftName
        {
            get { return liftName; }
            set
            {
                liftName = value;
                PrimaryMuscleGroup = liftRealtions.FromLiftToMuscleGroup(value).Key;
                SecondaryMuscleGroup = liftRealtions.FromLiftToMuscleGroup(value).Value;
                LiftNameFK = liftRealtions.FromLiftToLiftFK(value);

                OnPropertyChanged();
            }
        }

        private decimal weight;

        public decimal Weight
        {
            get { return weight; }
            set 
            { 
                weight = value; 
                OnPropertyChanged();
            }
        }


        private int reps;

        public int Reps
        {
            get { return reps; }
            set 
            { 
                reps = value;
                OnPropertyChanged();
            }
        }

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
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set 
            { 
                date = value; 
                OnPropertyChanged();
            }
        }

        public bool IsNew { get; set; }
    
    }
}
