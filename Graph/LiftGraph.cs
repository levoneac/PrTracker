using OxyPlot.Series;
using OxyPlot;
using PrTracker.Model;
using System.Collections.ObjectModel;
using PrTracker.ViewModel;
using PrTracker.Data;

namespace PrTracker.Graph
{
    public class LiftGraph
    {
        public PlotController InteractionController { get; set; }
        private IQueryable<string> Placeholder {  get; set; }
        public ILookup<string, ShownLiftData> GroupedLiftDataByName { get; set; }

        public LiftGraph(MainWindowViewModel vm) 
        {
            vm.GraphCategoryChangeEvent += ViewModel_GraphCategoryChangeEvent;


            InteractionController = new PlotController();
            InteractionController.UnbindMouseDown(OxyMouseButton.Left);
            InteractionController.BindMouseEnter(PlotCommands.HoverSnapTrack);
            
                       
        }


        private void ViewModel_GraphCategoryChangeEvent(object? sender, EventArguments.GraphCategoryChangeArgs e)
        {
            //throw new NotImplementedException();

            //set GroupedLiftDataByName to the appropriate Graph

        }

        //Needs tp be called in mainview when database update has happened
        public void UpdateData(ObservableCollection<ShownLiftData> liftData)
        {
            GroupedLiftDataByName = liftData.ToLookup(i => i.LiftName);
        }

        public IEnumerable<ShownLiftData> GetLiftCategoryByName(string name)
        {
            return GroupedLiftDataByName[name];
        }

        

        public class Scatter
        {
            public ScatterSeries Plot { get; set; }
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

        public class OneRepMaxProgression
        {
            public LineSeries Plot { get; set; }

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
