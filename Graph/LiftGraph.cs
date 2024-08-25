using OxyPlot.Series;
using OxyPlot;
using PrTracker.Model;
using System.Collections.ObjectModel;
using PrTracker.ViewModel;
using PrTracker.Data;
using OxyPlot.Axes;

namespace PrTracker.Graph
{
    public class LiftGraph
    {
        public PlotController InteractionController { get; set; }
        private PlotModel LiftModel {  get; set; }
        private ILookup<string, ShownLiftData> GroupedLiftDataByName { get; set; }


        public delegate double OneRepMaxFormula(double weight, int reps);
        public OneRepMaxFormula? ORMForumula { get; set; }

        public enum GraphType
        {
            OneRepMax,
            AllLifts
        }

        public LiftGraph() 
        {
            InteractionController = new PlotController();
            InteractionController.UnbindMouseDown(OxyMouseButton.Left);
            InteractionController.BindMouseEnter(PlotCommands.HoverSnapTrack);
 
        }




        
        public PlotModel MakeLiftModel(GraphType type, string liftName)
        {
            PlotModel newModel = new PlotModel();
            IEnumerable<ShownLiftData> data = GroupedLiftDataByName[liftName];
            if (type == GraphType.AllLifts)
            {
                ScatterSeries scatterSeries = new ScatterSeries()
                {
                    MarkerType = MarkerType.Circle,
                };
                foreach (ShownLiftData lift in data)
                {
                    scatterSeries.Points.Add(new ScatterPoint(lift.Reps, (double)lift.Weight));
                }
                newModel.Series.Add(scatterSeries);

                newModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Weight",
                    Minimum = 0,
                    MaximumDataMargin = 10
                });

                newModel.Axes.Add(new LinearAxis
                {
                    Position= AxisPosition.Bottom,
                    Title = "Reps",
                    Minimum = 0,
                    MaximumDataMargin = 3
                    
                });

            }
            else if (type == GraphType.OneRepMax)
            {
                LineSeries lineSeries = new LineSeries()
                {

                };

                SortedDictionary<DateTime, double> maxOnDate = new SortedDictionary<DateTime, double>();
                double oneRepMax;
                DateTime day;
                foreach (ShownLiftData lift in data)
                {
                    oneRepMax = CalculateOneRepMax((double)lift.Weight, lift.Reps);
                    day = lift.Date.Date;
                    if (maxOnDate.TryGetValue(day, out double weight))
                    {
                        if(oneRepMax > weight)
                        {
                            maxOnDate[day] = oneRepMax;
                        }
                    } else
                    {
                        maxOnDate[day] = oneRepMax;
                    }
                }
                foreach (KeyValuePair<DateTime, double> max in maxOnDate)
                {
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(max.Key), max.Value));
                }
                newModel.Series.Add(lineSeries);

                newModel.Axes.Add(new DateTimeAxis
                {
                    Title = "Date",
                    Position = AxisPosition.Bottom,
                    MinimumDataMargin = 5,
                    MaximumDataMargin = 5,
                    StringFormat = "MM/dd/yyyy"

                });
                newModel.Axes.Add(new LinearAxis
                {
                    Title = "Weight",
                    Position = AxisPosition.Left,
                    Minimum = 0,
                    MaximumDataMargin = 10
                });
            }
            
            return newModel;
        }

        private double CalculateOneRepMax(double weight, int reps)
        {
            if(weight < 0 || reps < 0)
            {
                return 0;
            }
            if(ORMForumula is null)
            {
                return weight * (1 + (0.025 * reps));
            }
            return ORMForumula(weight, reps);
        }


        //Needs tp be called in mainview when database update has happened
        public void UpdateData(ObservableCollection<ShownLiftData> liftData)
        {
            GroupedLiftDataByName = liftData.ToLookup(i => i.LiftName);
        }

        public IEnumerable<ShownLiftData> GetLiftsInCategoryByName(string name)
        {
            if(GroupedLiftDataByName is not null)
            {
                return GroupedLiftDataByName[name];
            }
            return Enumerable.Empty<ShownLiftData>();
        }
    }
}
