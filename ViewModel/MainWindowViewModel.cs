using Microsoft.EntityFrameworkCore;
using OxyPlot;
using PrTracker.Data;
using PrTracker.Models;
using PrTracker.Model;
using PrTracker.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
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

        public readonly DBInteraction dbi;

        public MainWindowViewModel(DBInteraction DBi)
        {
            dbi = DBi;

            MainLiftView = dbi.GetShownLiftData();
            ExistingLifts = dbi.GetExistingLiftTypes();

            ExistingLiftsValues = new ObservableCollection<string>();
            ExistingLifts.ToList().ForEach(i => ExistingLiftsValues.Add(i.Value));

            ExistingMuscleGroups = dbi.GetExistingMuscleGroups();
        
            Trace.WriteLine($"KEY OF 2: {ExistingMuscleGroups.Where(i => i.Key == 2).First().Value}");
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

            int muscleGroup = 9;  //Default muscle group for deadlift which is normally the first lift
            if (dbi.LiftTable.Count <= 0)
            {
                //Logic to add lift first?
            }
            else
            {
                var found = dbi.LiftTable.Where(i => i.LiftName == liftName);
                if (found is not null && found.Any())
                {
                    muscleGroup = found.First().PrimaryMuscleGroupId.Id;
                }
                                           
            }

            ShownLiftData newItem = new ShownLiftData
            {
                LiftNameFK = liftNameFK, 
                LiftName = liftName,
                Weight = 0,
                Reps = 0,
                PrimaryMuscleGroup = muscleGroup, //get from liftname
                Date = DateTime.Now,
                IsNew = true,
            };
            
            MainLiftView.Add(newItem);
            SelectedItem = newItem;
        }

        private void DeleteItem()
        {
            MainLiftView.Remove(selectedItem);
        }

        private void Save()
        {

            if (!dbi.SaveNewRecordedLifts(MainLiftView))
            {
                Trace.WriteLine("ERRRRRRRRRRRRRRRRRRRRRRRRRROOR");
            }
            dbi.dB.SaveChanges();
            

        }

        private bool CanSave()
        {
            //is db connected?
            //is usser authenticated to save?
            return true;
        }

    }
}
