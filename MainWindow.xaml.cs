using PrTracker.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool running = false;
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
        }

        private void btnToggleRun_Click(object sender, RoutedEventArgs e)
        {
            //if(running == false)
            //{
            //    tbStatus.Text = "Online";
            //    btnToggleRun.Content = "Stop";
            //} else
            //{
            //    tbStatus.Text = "Offline";
            //    btnToggleRun.Content = "Kjør";
            //}
            //running = !running;

        }

        private void TrackerControl_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}