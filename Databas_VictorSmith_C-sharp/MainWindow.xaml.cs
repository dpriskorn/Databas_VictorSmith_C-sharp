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
using Databas_VictorSmith_C_sharp.Models;
using Databas_VictorSmith_C_sharp.Repositories;

namespace Databas_VictorSmith_C_sharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void GetObserver_Click(object sender, RoutedEventArgs e)
        {
            List<Observer> listedObserver = new List<Observer>();
            listedObserver = CRUD.GetObserver().ToList();
            PresentObservers.ItemsSource = null;
            PresentObservers.ItemsSource = listedObserver;
        }

        private void AddObserver_Click(object sender, RoutedEventArgs e)
        {
            CRUD.AddObserver();
        }
        private void DeleteObserver_Click(object sender, RoutedEventArgs e)
        {
            Observer selectedObserver = (Observer)PresentObservers.SelectedItem;
            CRUD.DeleteObserver(selectedObserver);
        }

        private void PresentObservers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectObserver_Click(object sender, RoutedEventArgs e)
        {
            CRUD.SelectObserver(PresentObservers.SelectedItem as Observer);
        }
    }
}
