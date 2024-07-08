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
        ObservableCollection<MuscleGroup> Items { get; set; }
        RelayCommand SaveCommand { get; }
        MuscleGroup SelectedItem { get; set; }
        string Username { get; set; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public ObservableCollection<MuscleGroup> Items { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem()); //, canExecute => { return true; });
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => selectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());


        public MainWindowViewModel(LiftContext DB)
        {
 
            dB = DB;
            //MuscleGroups m = new MuscleGroups()
            //{
            //    Id = 15,
            //    PrimaryMuscleGroup = "test",
            //};
            //
            //dB.MuscleGroups.Add(m);
            //dB.SaveChanges();

            Trace.WriteLine($"DBDATA: {dB.MuscleGroups.Where(l => l.Id == 1).FirstOrDefault()}.");
            Trace.WriteLine($"ISSQLITE: {dB.Database.IsSqlite()}");
            Trace.WriteLine($"Db: {dB.MuscleGroups.FromSql($"SELECT * FROM MuscleGroups").First()}");

            Items = new ObservableCollection<MuscleGroup>();
            dB.MuscleGroups.ToList().ForEach(i => Items.Add(new MuscleGroup{
                Id = i.Id,
                MuscleGroupName = i.PrimaryMuscleGroup
            }));
        }
      
        private readonly LiftContext dB;

        private MuscleGroup selectedItem;
        public MuscleGroup SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Username"));
                OnPropertyChanged();
            }
        }



        private void AddItem()
        {
            MuscleGroup newItem = new MuscleGroup
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
