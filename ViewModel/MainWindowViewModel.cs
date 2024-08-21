using PrTracker.Data;
using PrTracker.Model;
using PrTracker.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PrTracker.Helpers;
using System.Windows.Input;
using System.Text.RegularExpressions;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using PrTracker.Graph;
using PrTracker.EventArguments;
using static PrTracker.Graph.LiftGraph;
using PrTracker.View;

namespace PrTracker.ViewModel
{
    public interface IMainWindowViewModel
    {
        RelayCommand AddCommand { get; }
        RelayCommand DeleteCommand { get; }
        ObservableCollection<ShownLiftData> MainLiftView { get; set; }
        RelayCommand SaveCommand { get; }
        ShownLiftData SelectedItem { get; set; }
        ObservableCollection<KeyValuePair<int, string>> ExistingLifts { get; set; }
        ObservableCollection<KeyValuePair<int, string>> ExistingMuscleGroups { get; set; }
        public string SelectedLift {  get; set; }

        AddNewLiftType? NewLiftWindow { get; set; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public event EventHandler<GraphCategoryChangeArgs> GraphCategoryChangeEvent;

        private bool isOneRepMax = false;

        public bool IsOneRepMax
        {
            get { return isOneRepMax; }
            set 
            { 
                isOneRepMax = value;
                OnPropertyChanged();
                GraphCategoryChangeEvent?.Invoke(this, new GraphCategoryChangeArgs(IsOneRepMax, GraphCurrentSelectedLift));
            }
        }

        private string graphCurrentSelectedLift = "Deadlift";

        public string GraphCurrentSelectedLift
        {
            get { return graphCurrentSelectedLift; }
            set 
            { 
                graphCurrentSelectedLift = value;
                OnPropertyChanged();
                GraphCategoryChangeEvent?.Invoke(this, new GraphCategoryChangeArgs(IsOneRepMax, GraphCurrentSelectedLift));
            }
        }



        private ObservableCollection<ShownLiftData> mainLiftView;
        public ObservableCollection<ShownLiftData> MainLiftView
        {
            get { return mainLiftView; }
            set
            {
                mainLiftView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem()); //, canExecute => { return true; });
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => selectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public RelayCommand OpenNewLiftWindowCommand => new RelayCommand(execute => OpenNewLiftWindow(), canExecute => CanOpenNewLiftWindow());
        public RelayCommand AddNewLiftCommand => new RelayCommand(execute => AddNewLift(), canExecute => CanAddNewLift());


        private ShownLiftData selectedItem;
        public ShownLiftData SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> existingLifts;

        public ObservableCollection<KeyValuePair<int, string>> ExistingLifts
        {
            get { return existingLifts; }
            set
            {
                existingLifts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> existingLiftsValues;

        public ObservableCollection<string> ExistingLiftsValues
        {
            get { return existingLiftsValues; }
            set 
            { 
                existingLiftsValues = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<KeyValuePair<int, string>> existingMuscleGroups;

        public ObservableCollection<KeyValuePair<int, string>> ExistingMuscleGroups
        {
            get { return existingMuscleGroups; }
            set 
            { 
                existingMuscleGroups = value;
                OnPropertyChanged();
            }
        }


        private string selectedLift;

        public string SelectedLift
        {
            get { return selectedLift; }
            set 
            { 
                selectedLift = value;
                OnPropertyChanged();
            }
        }

        private string newLiftName;

        public string NewLiftName
        {
            get { return newLiftName; }
            set { 
                newLiftName = value;
                OnPropertyChanged();
            }
        }


        private AddNewLiftType? newLiftWindow;
        public AddNewLiftType NewLiftWindow
        {
            get { return newLiftWindow; }
            set
            {
                if(newLiftWindow is not null)
                {
                    newLiftWindow.Close();
                }
                newLiftWindow = value;
                newLiftWindow.Show();
            }
        }


        private PlotModel liftModel;

        public PlotModel LiftModel
        {
            get { return liftModel; }
            private set 
            { 
                liftModel = value;
                OnPropertyChanged();
            }
        }

        private PlotController cControl;

        public PlotController CControl
        {
            get { return cControl; }
            private set { cControl = value; }
        }

        public MainWindowViewModel ThisContext { get; set; }



        private readonly DBInteraction dbi;
        private readonly LiftRelationConversions liftToMuscleGroupRelations;
        private readonly LiftGraph Graph;

        public MainWindowViewModel(DBInteraction DBi)
        {
            ThisContext = this;
            dbi = DBi;

            ExistingLiftsValues = new ObservableCollection<string>();
            liftToMuscleGroupRelations = LiftRelationConversions.GetLiftToMuscleGroupRelations();
            MainLiftView = dbi.GetShownLiftData();
            DBInteraction.DbUpdateEvent += DBi_DbUpdateEvent;
            UpdateInfoFromDatabase();

            Graph = new LiftGraph();
            CControl = Graph.InteractionController;
            Graph.UpdateData(MainLiftView);
            GraphCategoryChangeEvent += Handle_GraphCategoryChangeEvent;
            LiftModel = Graph.MakeLiftModel(GraphType.AllLifts, GraphCurrentSelectedLift);

        }

        //Maybe this doesnt need to be an event, but make code feel more intuitive
        private void Handle_GraphCategoryChangeEvent(object? sender, GraphCategoryChangeArgs e)
        {
            GraphType graphType = GetLiftGraphType(e.IsOneRepMax);

            LiftModel = Graph.MakeLiftModel(graphType, e.LiftName);
        }

        private GraphType GetLiftGraphType(bool isOneRepMax)
        {
            GraphType graphType;
            if (isOneRepMax)
            {
                graphType = GraphType.OneRepMax;
            }
            else
            {
                graphType = GraphType.AllLifts;
            }
            return graphType;
        }

        public void UpdateInfoFromDatabase()
        {
            ExistingLifts = dbi.GetExistingLiftTypes();
            ExistingLifts.ToList().ForEach(i => ExistingLiftsValues.Add(i.Value));
            ExistingMuscleGroups = dbi.GetExistingMuscleGroups();
        }

        private void DBi_DbUpdateEvent(object? sender, string e)
        {
            Trace.WriteLine(e);
            UpdateInfoFromDatabase();

            //A lot of double work here. Look into later
            Graph.UpdateData(MainLiftView);
            LiftModel = Graph.MakeLiftModel(GetLiftGraphType(IsOneRepMax), GraphCurrentSelectedLift);
            Trace.WriteLine("UI UPDATED");
        }

        private void AddItem()
        {
            string liftName;
            int liftNameFK;
            if(ExistingLifts.Count <= 0)
            {
                liftName = "Deadlift";
                liftNameFK = 1;
            } 
            else
            {
                liftName = ExistingLifts[0].Value;
                liftNameFK = ExistingLifts[0].Key;
            }

            KeyValuePair<string, string> muscleGroups = liftToMuscleGroupRelations.FromLiftToMuscleGroup(liftName);


            ShownLiftData newItem = new ShownLiftData()
            {
                Id = -123, //Wont be sent. Can be used to check if item has been added in this sesion
                LiftNameFK = liftNameFK,
                LiftName = liftName,
                Weight = 0,
                Reps = 0,
                PrimaryMuscleGroup = muscleGroups.Key,
                SecondaryMuscleGroup = muscleGroups.Value,
                Date = DateTime.Now,
                IsNew = true,
            };
            
            MainLiftView.Add(newItem);
            SelectedItem = newItem;
        }

        private void DeleteItem()
        {
            if (!dbi.DeleteSelectedLift(selectedItem))
            {
                Trace.WriteLine("ERR");
            }
            dbi.dB.SaveChanges();
            MainLiftView.Remove(selectedItem);
        }

        private void Save()
        {

            if (!dbi.SaveNewRecordedLifts(MainLiftView))
            {
                Trace.WriteLine("ERR");
            }
            dbi.dB.SaveChanges();
            SelectedItem = SelectedItem; //updates the property so that the UI greys out the already selected item after saving
            
        }

        private bool CanSave()
        {
            //is db connected?
            //is usser authenticated to save?
            return true;
        }

        private void OpenNewLiftWindow()
        {
            NewLiftWindow = new AddNewLiftType(ThisContext);
        }

        private bool CanOpenNewLiftWindow()
        {
            AddNewLiftType? window = NewLiftWindow;
            if(window?.IsVisible == true)
            {
                return false;
            }
            return true;
        }


        private void AddNewLift()
        {
            //add to db
            //update the list of avaliable lifts
            NewLiftWindow.Close();
        }

        private bool CanAddNewLift()
        {
            //if all fields are filled
            //dont need input check for liftname, as i expect the user to have a specific intention with their naming
            return true;
        }




    }
}
