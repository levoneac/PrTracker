using PrTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrTracker.View
{
    /// <summary>
    /// Interaction logic for AddNewLiftType.xaml
    /// </summary>
    public partial class AddNewLiftType : Window
    {
        public AddNewLiftType(IMainWindowViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
        }
    }
}
