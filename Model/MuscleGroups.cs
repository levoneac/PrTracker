using PrTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Model
{
    public class MuscleGroups : ViewModelBase
    {
		private string? primary;

		public string Primary
		{
			get {
                if (primary is null)
                {
                    return "None";
                }
                return primary;
            }
			set { 
				primary = value;
				OnPropertyChanged();
			}
		}

		private string? secondary;

		public string Secondary
		{
			get { 
				if(secondary is null)
				{
					return "None";
				}
				return secondary;
			}
			set { 
				secondary = value;
				OnPropertyChanged();
			}
		}


	}
}
