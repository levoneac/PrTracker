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

        private readonly LiftContext dB;

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

        public MainWindowViewModel(LiftContext DB)
        {
 
            dB = DB;

            IQueryable<ShownLiftData> mainView = (from recorded in dB.RecordedLifts
                            join liftTypes in dB.Lifts on recorded.Lift.Id equals liftTypes.Id
                            where recorded.LifterId.Id == 2
                            select new ShownLiftData
                            {
                                LiftName = liftTypes.LiftName,
                                Weight = recorded.Weight,
                                Reps = recorded.Reps,
                                PrimaryMuscleGroup = liftTypes.PrimaryMuscleGroupId.Id,
                                SecondaryMuscleGroup = liftTypes.SecondaryMuscleGroupId.Id,
                                Date = recorded.DayOfLift
                            });


            Items = new ObservableCollection<ShownLiftData>();

            foreach (ShownLiftData row in mainView)
            {
                Trace.WriteLine($"ROW: {row.ToString()}");
                Items.Add(row);
            }

            ExistingLifts = new ObservableCollection<string>();
            dB.Lifts.ToList().ForEach(i => ExistingLifts.Add(i.LiftName));
        }
      





        private void AddItem()
        {
            ShownLiftData newItem = new ShownLiftData
            {
                Id = 0,
                MuscleGroupName = "Placeholder",
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
