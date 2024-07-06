using PrTracker.Model;
using PrTracker.MVVM;
using System.Collections.ObjectModel;

namespace PrTracker.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ExampleItem> Items { get; set; }

        public MainWindowViewModel() 
        {
            Items = new ObservableCollection<ExampleItem>();

            Items.Add(new ExampleItem
            {
                Id = 0,
                Name = "Test",
            });
            Items.Add(new ExampleItem
            {
                Id = 1,
                Name = "Mathias",
            });
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


    }
}
