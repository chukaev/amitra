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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace AminaTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public bool FirstClick { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //TourAdressTextBox.Text
            FirstClick = !FirstClick;

            if (FirstClick)
            {
                Ring.IsActive = true;
                FindingLabel.Visibility = Visibility.Visible;
                ResultsTab.Visibility = Visibility.Hidden;
                ComparisonGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                Ring.IsActive = false;
                FindingLabel.Visibility = Visibility.Hidden;
                ResultsTab.Visibility = Visibility.Visible;
                ComparisonGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
