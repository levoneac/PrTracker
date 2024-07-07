using PrTracker.Model;
using PrTracker.MVVM;
using System.Collections.ObjectModel;

namespace PrTracker.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ExampleItem> Items { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem()); //, canExecute => { return true; });
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => selectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());


        public MainWindowViewModel() 
        {
            Items = new ObservableCollection<ExampleItem>();
        }

        private ExampleItem selectedItem;

        public ExampleItem SelectedItem
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
            Items.Add(new ExampleItem
            {
                Id = 0,
                Name = "Placeholder",
            });
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
