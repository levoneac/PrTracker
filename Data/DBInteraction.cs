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

        private List<Lifts> liftTable;

        public List<Lifts> LiftTable
        {
            get { return liftTable; }
            private set { liftTable = value; }
        }


        public DBInteraction(LiftContext db)
        {
            dB = db;
            LiftTable = dB.Lifts.ToList();
        }

        public ObservableCollection<KeyValuePair<int, string>> GetExistingMuscleGroups()
        {
            ObservableCollection<KeyValuePair<int, string>> data = new ObservableCollection<KeyValuePair<int, string>>();
            dB.MuscleGroups.ToList()
                .ForEach(i => data.Add(new KeyValuePair<int, string>(i.Id, i.PrimaryMuscleGroup)));
            return data;
        }

        public ObservableCollection<KeyValuePair<int, string>> GetExistingLiftTypes()
        {
            ObservableCollection<KeyValuePair<int, string>> data = new ObservableCollection<KeyValuePair<int, string>>();
            dB.Lifts.ToList()
                .ForEach(i => data.Add(new KeyValuePair<int, string>(i.Id, i.LiftName)));
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
                        LiftNameFK = liftTypes.Id,
                        LiftName = liftTypes.LiftName,
                        Weight = recorded.Weight,
                        Reps = recorded.Reps,
                        PrimaryMuscleGroup = liftTypes.PrimaryMuscleGroupId.Id,
                        SecondaryMuscleGroup = liftTypes.SecondaryMuscleGroupId.Id,
                        Date = recorded.DayOfLift,
                        IsNew = false
                    });

            intermediate.ToList()
                .ForEach(data.Add);
            return data;
        }

        public bool SaveNewRecordedLifts(ObservableCollection<ShownLiftData> data)
        {
            List<ShownLiftData> newlyAdded = data.Where(i => i.IsNew).ToList();

            //Get the base user to refer to
            //Adding users fully might come later
            var person = dB.People.Where(i => i.Id == 2);
            if (person is null || !person.Any())
            { 
                return false;
            }
            People personFK = person.First();

            foreach(ShownLiftData lift in newlyAdded)
            {
                //Get lift to refer to
                var lifts = dB.Lifts.Where(i => i.LiftName == lift.LiftName); //change to use FK
                if (lifts is null || !lifts.Any()) 
                {
                    return false;
                }
                Lifts liftFK = lifts.First();

                dB.RecordedLifts.Add(new RecordedLifts
                {
                    Lift = liftFK,
                    Reps = lift.Reps,
                    Weight = lift.Weight,
                    DayOfLift = lift.Date,
                    LifterId = personFK
                });
            }
            //Cant do this in the previous loop in case execution fails midway
            //and we would have lost track of the new lifts
            foreach (ShownLiftData lift in newlyAdded)
            {
                lift.IsNew = false;
            }
            return true;
        }
    }
}
