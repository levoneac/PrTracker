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
                ScatterSeries scatterSeries = new ScatterSeries();
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
                LineSeries lineSeries = new LineSeries();
                foreach (ShownLiftData lift in data)
                {
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(lift.Date), (double)lift.Weight));
                }
                newModel.Series.Add(lineSeries);

                newModel.Axes.Add(new DateTimeAxis
                {
                    Title = "Date",
                    Position = AxisPosition.Bottom,
                    MinimumDataMargin = 5,
                    MaximumDataMargin = 5
                    
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

        

        private class Scatter
        {
            public ScatterSeries PlotSerie { get; set; }
            public Scatter()
            {
                PlotModel model = new PlotModel
                {
                    Title = "All recorded lifts",
                };

                ScatterSeries scatterLifts = new ScatterSeries
                {
                    MarkerType = MarkerType.Cross,
                    MarkerFill = OxyColors.Aqua,
                    MarkerStroke = OxyColors.DarkGreen,
                    MarkerSize = 4,
                    MarkerStrokeThickness = 5,
                };
            }
        }

        private class OneRepMaxProgression
        {
            public LineSeries PlotSerie { get; set; }

            public OneRepMaxProgression()
            {
                PlotModel model = new PlotModel
                {
                    Title = "One rep max progression",
                };
            }

            //model.Axes.Add(new DateTimeAxis
            //{
            //    Position = AxisPosition.Bottom,
            //    Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-10)),
            //    Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(20))
            //});
            //model.Axes.Add(new LinearAxis
            //{
            //    Position = AxisPosition.Left,
            //    Minimum = 0,
            //    Maximum = 100
            //});


        }
    }
}
