using PrTracker.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace PrTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            //MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}