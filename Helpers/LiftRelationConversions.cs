using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Helpers
{
    public class LiftRelationConversions
    {
        private LiftRelationConversions()
        {
            
        }

        private Dictionary<string, KeyValuePair<string, string>> liftToMuscleGroup;
        private Dictionary<string, int> liftToLiftFK;

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

        public int FromLiftToLiftFK(string key)
        {
            int value;
            if (liftToLiftFK.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return -1;
            }
        }

        public void SetLiftToLiftFK(Dictionary<string, int> dict)
        {
            liftToLiftFK = dict;
        }




        private readonly static LiftRelationConversions ltmgRelation = new LiftRelationConversions();
        public static LiftRelationConversions GetLiftToMuscleGroupRelations()
        {
            return ltmgRelation;
        }
    }
}
