using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrTracker.View.UserControls
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl, INotifyPropertyChanged
    {
        public MenuBar()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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

        private void OnPropertyChanged( [CallerMemberName] string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void MenuItem_Click_FileNew(object sender, RoutedEventArgs e)
        {
           //
        }
    }
}
