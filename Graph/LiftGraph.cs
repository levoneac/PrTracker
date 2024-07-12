using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using PrTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using PrTracker.Data;

namespace PrTracker.Graph
{
    public class LiftGraph
    {
        public PlotController InteractionController { get; set; }
        private IQueryable<string> Placeholder {  get; set; }
        public ILookup<string, ShownLiftData> GroupedLiftDataByName { get; set; }

        public LiftGraph(ObservableCollection<ShownLiftData> liftData) 
        {
            InteractionController = new PlotController();
            InteractionController.UnbindMouseDown(OxyMouseButton.Left);
            InteractionController.BindMouseEnter(PlotCommands.HoverSnapTrack);

            GroupedLiftDataByName = liftData.ToLookup(i => i.LiftName);
           

            DBInteraction.DbUpdateEvent += DBInteraction_DbUpdateEvent;

            //liftData.Where(i => i.LiftName == "Overhead Press")
            //    .ToList()
            //    .ForEach(j => scatterLifts.Points.Add(new ScatterPoint(DateTimeAxis.ToDouble(j.Date), (double)j.Weight, 10)));
            //model.Series.Add(scatterLifts);

        }

        private void DBInteraction_DbUpdateEvent(object? sender, string e)
        {
            //Should i just recalculate all? or should i try to insert them in the grooups

            //E1:
            //newlifts.tolookup
            //foreach lift in newlifts
            //  GRoupedByLiftName[lift].add(lift)
            throw new NotImplementedException();
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
