using PrTracker.Model;
using PrTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Data
{
    public class DBInteraction
    {

        public readonly LiftContext dB;

        public DBInteraction(LiftContext db)
        {
            dB = db;
        }

        public ObservableCollection<string> GetExistingMuscleGroups()
        {
            ObservableCollection<string> data = new ObservableCollection<string>();
            dB.MuscleGroups.ToList()
                .ForEach(i => data.Add(i.PrimaryMuscleGroup));
            return data;
        }

        public ObservableCollection<string> GetExistingLiftTypes()
        {
            ObservableCollection<string> data = new ObservableCollection<string>();
            dB.Lifts.ToList()
                .ForEach(i => data.Add(i.LiftName));
            return data;
        }

        public ObservableCollection<ShownLiftData> GetShownLiftData()
        {
            ObservableCollection<ShownLiftData> data = new ObservableCollection<ShownLiftData>();

            IQueryable<ShownLiftData> intermediate 
                = (from recorded in dB.RecordedLifts
                    join liftTypes in dB.Lifts on recorded.Lift.Id equals liftTypes.Id
                    where recorded.LifterId.Id == 2
                    select new ShownLiftData
                    {
                        Id = recorded.Id,
                        LiftName = liftTypes.LiftName,
                        Weight = recorded.Weight,
                        Reps = recorded.Reps,
                        PrimaryMuscleGroup = liftTypes.PrimaryMuscleGroupId.Id,
                        SecondaryMuscleGroup = liftTypes.SecondaryMuscleGroupId.Id,
                        Date = recorded.DayOfLift
                    });

            intermediate.ToList()
                .ForEach(data.Add);
            return data;
        }
    }
}
