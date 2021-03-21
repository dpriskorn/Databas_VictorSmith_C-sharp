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
        // <initialization>
        public Observer selectedObserver;
        IEnumerable<Observer> listOfObservers;
        // </initialization>

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetObserver_Click(object sender, RoutedEventArgs e)
        {
            listOfObservers = CRUD.GetObserver();
            // Uppdatera listan på användares begäran för att se aktuella observatörer.
            PresentObservers.ItemsSource = null;
            PresentObservers.ItemsSource = listOfObservers;
        }

        private void AddObserver_Click(object sender, RoutedEventArgs e)
        {
            // Lägger till fake-observatörer för att kunna testa funktioner.
            CRUD.AddObserver();
        }
        private void DeleteObserver_Click(object sender, RoutedEventArgs e)
        {          
            CRUD.DeleteObserver(selectedObserver);
            // Uppdaterar listan för att reflektera vilka observatörer som existerar.
            PresentObservers.ItemsSource = null;
            PresentObservers.ItemsSource = listOfObservers;
        }

        private void PresentObservers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Uppdatera listan så att vald observatör faktiskt finns i databasen.
            CRUD.GetObserver();
            selectedObserver = (Observer)PresentObservers.SelectedItem;
        }

        private void SelectObserver_Click(object sender, RoutedEventArgs e)
        {
            CRUD.SelectObserver(PresentObservers.SelectedItem as Observer);
        }
    }
}
