using Microsoft.EntityFrameworkCore;
using OxyPlot;
using PrTracker.Data;
using PrTracker.Models;
using PrTracker.Model;
using PrTracker.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;

namespace PrTracker.ViewModel
{
    public interface IMainWindowViewModel
    {
        RelayCommand AddCommand { get; }
        RelayCommand DeleteCommand { get; }
        ObservableCollection<ShownLiftData> Items { get; set; }
        RelayCommand SaveCommand { get; }
        ShownLiftData SelectedItem { get; set; }
        ObservableCollection<string> ExistingLifts { get; set; }
        public string SelectedLift {  get; set; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public ObservableCollection<ShownLiftData> Items { get; set; }

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

        private ObservableCollection<string> existingLifts;

        public ObservableCollection<string> ExistingLifts
        {
            get { return existingLifts; }
            set
            {
                existingLifts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> existingMuscleGroups;

        public ObservableCollection<string> ExistingMuscleGroups
        {
            get { return existingMuscleGroups; }
            set { existingMuscleGroups = value; }
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

        public MainWindowViewModel(DBInteraction DBi)
        { 
            Items = DBi.GetShownLiftData();
            ExistingLifts = DBi.GetExistingLiftTypes();
            ExistingMuscleGroups = DBi.GetExistingMuscleGroups();


        }
      

        private void AddItem()
        {
            ShownLiftData newItem = new ShownLiftData
            {
                Id = Items.Last().Id + 1, //Probably just to keep track of newly added rows in the UI. DB should autoincrement so we dont clash
                LiftName = ExistingLifts[0] ?? "Bench",
                Weight = 0,
                Reps = 0,
                PrimaryMuscleGroup = 1, //get from liftname
                Date = DateTime.Now
            };
            
            Items.Add(newItem);
            SelectedItem = newItem;
        }

        private void DeleteItem()
        {
            Items.Remove(selectedItem);
        }

        private void Save()
        {
            //SQLite save
        }

        private bool CanSave()
        {
            //is db connected?
            //is usser authenticated to save?
            return true;
        }

    }
}
