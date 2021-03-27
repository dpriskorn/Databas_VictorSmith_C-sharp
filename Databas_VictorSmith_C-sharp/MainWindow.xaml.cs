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
        #region INITIALIZATION
        public Observer selectedObserver = null;
        List<Observer> listOfObservers = null;
        //System.Diagnostics.Trace.WriteLine($"MainWindow:INITIALIZATION");
        readonly List<Observer> initialListOfObservers = CRUD.GetObserverList();
        #endregion

        #region FETCHMETHODS
        public void FetchObservers()
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchObservers");
            // Update global variable
            listOfObservers = CRUD.GetObserverList();
            UpdateObserversListbox(listOfObservers);
        }
        public void FetchObservations(Observer observer)
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchObservations");
            List<Observation> listOfObservations = CRUD.UpdateObservationList(observer);
            UpdateObservationsListbox(listOfObservations);
        }
        public void FetchMeasurements(Observation observation)
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchMeasurements");
            List<Measurement> listOfMeasurements = CRUD.UpdateMeasurementList(observation);
            UpdateMeasurementsListbox(listOfMeasurements);
        }
        #endregion
        #region UIMETHODS
        public void UpdateObserversListbox(List<Observer> list)
        {
            Observers.ItemsSource = null;
            Observers.ItemsSource = list;
        }
        public void UpdateObservationsListbox(List<Observation> list)
        {
            Observations.ItemsSource = null;
            Observations.ItemsSource = list;
        }
        public void UpdateMeasurementsListbox(List<Measurement> list)
        {
            observationMeasurements.ItemsSource = null;
            observationMeasurements.ItemsSource = list;
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Observers.ItemsSource = null;
            Observers.ItemsSource = initialListOfObservers;
        }

        private void Observers_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Update the global variable
            System.Diagnostics.Trace.WriteLine($"MainWindow:Observers_SelectionChanged");
            selectedObserver = CRUD.GetObserver((sender as ListBox).SelectedItem as Observer);
            FetchObservations(selectedObserver);
        }

        private void SubmitNewObserverButton_Click(object sender, RoutedEventArgs e)
        {
            // Lägger till observatörer med För- och efternamn.
            string firstName, lastName;
            firstName = NameNewObserverInput.Text.ToString();
            lastName = FamilyNameNewObserverInput.Text.ToString();
            CRUD.AddObserver(firstName, lastName);
            MessageBox.Show($"Observatör tillagd.");
            AddObserverBox.Visibility = Visibility.Hidden;
            FetchObservers();
        }

        private void DeleteObserverButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedObserver == null)
            {
                MessageBox.Show("Ingen observatör vald. Välj observatör i listan.").ToString();
            }
            else
            {
                // Use the global variable
                CRUD.DeleteObserver(selectedObserver);
                selectedObserver = null;
                // Uppdaterar listan för att reflektera vilka observatörer som existerar.
                FetchObservers();
            }
        }

        private void AddObserverButton_Click(object sender, RoutedEventArgs e)
        {
            AddObserverBox.Visibility = Visibility.Visible;
        }
    }
}
