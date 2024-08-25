using PrTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Model
{
    public class ShownMuscleGroups : ViewModelBase
    {
		private string? primary;

		public string Primary
		{
			get {
                if (primary is null)
                {
                    return "";
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
					return "";
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
