using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Helpers
{
    public class LiftToMuscleGroupRelations
    {
        private LiftToMuscleGroupRelations()
        {
            
        }

        private Dictionary<string, KeyValuePair<string, string>> liftToMuscleGroup;

        public KeyValuePair<string, string> FromLiftToMuscleGroup(string key)
        {
            KeyValuePair<string, string> value;
            if (liftToMuscleGroup.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return new KeyValuePair<string, string>("None", "None");
            }
        }

        public void SetLiftToMuscleGroup(Dictionary<string, KeyValuePair<string, string>> dict)
        {
            liftToMuscleGroup = dict;
        }


        private readonly static LiftToMuscleGroupRelations ltmgRelation = new LiftToMuscleGroupRelations();
        public static LiftToMuscleGroupRelations GetLiftToMuscleGroupRelations()
        {
            return ltmgRelation;
        }
    }
}
