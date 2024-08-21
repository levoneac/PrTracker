using Microsoft.EntityFrameworkCore;
using PrTracker.Helpers;
using PrTracker.Migrations;
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
       
        public static event EventHandler<string>? DbUpdateEvent;


        public readonly LiftContext dB;
        private readonly LiftRelationConversions liftToMuscleGroupRelations;
        private List<Lifts> liftTable;

        public List<Lifts> LiftTable
        {
            get { return liftTable; }
            private set { liftTable = value; }
        }


        public DBInteraction(LiftContext db)
        {
            dB = db;
            //1 month later: what?
            liftToMuscleGroupRelations = LiftRelationConversions.GetLiftToMuscleGroupRelations();
            liftToMuscleGroupRelations.SetLiftToMuscleGroup(GetLiftToMuscleGroupRelations());
            liftToMuscleGroupRelations.SetLiftToLiftFK(GetLiftToLliftFKRelations());
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

        public Dictionary<string, KeyValuePair<string, string>> GetLiftToMuscleGroupRelations()
        {
            IQueryable<KeyValuePair<string, KeyValuePair<string, string>>> intermediate
                = (from liftTypes in dB.Lifts
                   join primaryMuscleGroups in dB.MuscleGroups on liftTypes.PrimaryMuscleGroupId.Id equals primaryMuscleGroups.Id
                   join secondaryMuscleGroups in dB.MuscleGroups on liftTypes.SecondaryMuscleGroupId.Id equals secondaryMuscleGroups.Id
                   select new KeyValuePair<string, KeyValuePair<string, string>>
                   (
                       liftTypes.LiftName,
                       new KeyValuePair<string, string>(primaryMuscleGroups.PrimaryMuscleGroup, secondaryMuscleGroups.PrimaryMuscleGroup))
                    );
            return intermediate.ToDictionary();
        }

        public Dictionary<string, int> GetLiftToLliftFKRelations()
        {
            IQueryable<KeyValuePair<string, int>>? intermediate
                = (from liftTypes in dB.Lifts
                   select new KeyValuePair<string, int>(liftTypes.LiftName, liftTypes.Id));
            return intermediate.ToDictionary();
        }

        public ObservableCollection<ShownLiftData> GetShownLiftData()
        {
            ObservableCollection<ShownLiftData> data = new ObservableCollection<ShownLiftData>();

            IQueryable<ShownLiftData> intermediate
                = (from recorded in dB.RecordedLifts
                   join liftTypes in dB.Lifts on recorded.Lift.Id equals liftTypes.Id
                   join primaryMuscleGroups in dB.MuscleGroups on liftTypes.PrimaryMuscleGroupId.Id equals primaryMuscleGroups.Id
                   join secondaryMuscleGroups in dB.MuscleGroups on liftTypes.SecondaryMuscleGroupId.Id equals secondaryMuscleGroups.Id
                   where recorded.LifterId.Id == 2
                   select new ShownLiftData
                   {
                       Id = recorded.Id,
                       LiftNameFK = liftTypes.Id,
                       LiftName = liftTypes.LiftName,
                       Weight = recorded.Weight,
                       Reps = recorded.Reps,
                       PrimaryMuscleGroup = primaryMuscleGroups.PrimaryMuscleGroup,
                       SecondaryMuscleGroup = secondaryMuscleGroups.PrimaryMuscleGroup,
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
                IQueryable<Lifts> lifts;
                //Get lift to refer to
                //If we failed to set LiftNameFK, we can also rely on the LiftName, which should also be unique
                if(lift.LiftNameFK == -1)
                {
                    lifts = dB.Lifts.Where(i => i.LiftName == lift.LiftName); 
                    if (lifts is null || !lifts.Any())
                    {
                        return false;
                    }
                }
                else
                {
                    lifts = dB.Lifts.Where(i => i.Id == lift.LiftNameFK); 
                    if (lifts is null || !lifts.Any())
                    {
                        return false;
                    }
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
            //static events should have null as the sender
            DbUpdateEvent?.Invoke(null, "DATA SAVED");
            return true;
        }
        public bool DeleteSelectedLift(ShownLiftData data)
        {
            var findToDelete = dB.RecordedLifts.Where(i => i.Id == data.Id);
            if(findToDelete is null || !findToDelete.Any()){
                return false;
            }
            RecordedLifts liftToDelete = findToDelete.First();

            dB.RecordedLifts.Remove(liftToDelete);
            return true;
        }
    }
}
