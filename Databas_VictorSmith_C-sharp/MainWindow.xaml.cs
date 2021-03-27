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
    /// Measurements added will be held in a list until submitted.
    /// Measurements edited will be submitted at once.
    /// Measurements deleted will be submitted at once.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region INITIALIZATION
        public Observer selectedObserver = null;
        List<Observer> listOfObservers = null;
        List<Measurement> listOfNewMeasurements = null;
        List<Measurement> listOfMeasurements = null;
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
            // clear fields
            NameNewObserverInput.Text = "";
            FamilyNameNewObserverInput.Text = "";
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
            // Add and edit boxes share screen real estate and cannot both be shown
            if (EditObserverBox.Visibility == Visibility.Visible)
            {
                EditObserverBox.Visibility = Visibility.Hidden;
                AddObserverBox.Visibility = Visibility.Visible;
            }
            else
            {
                AddObserverBox.Visibility = Visibility.Visible;
            }
        }

        private void EditObserverButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedObserver != null)
            {
                // Add and edit boxes share screen real estate and cannot both be shown
                if (AddObserverBox.Visibility == Visibility.Visible)
                {
                    AddObserverBox.Visibility = Visibility.Hidden;
                    EditObserverBox.Visibility = Visibility.Visible;
                    // populate 
                    NameObserverInput.Text = selectedObserver.FirstName;
                    FamilyNameObserverInput.Text = selectedObserver.LastName;
                }
                else
                {
                    EditObserverBox.Visibility = Visibility.Visible;
                    NameObserverInput.Text = selectedObserver.FirstName;
                    FamilyNameObserverInput.Text = selectedObserver.LastName;
                }
            }
            else
            {
                MessageBox.Show("Ingen observatör vald. Välj observatör i listan.").ToString();
            }
        }

        private void NewObservationButton_Click(object sender, RoutedEventArgs e)
        {
            AddObservationBox.Visibility = Visibility.Visible;
        }

        private void EditObservationButton_Click(object sender, RoutedEventArgs e)
        {
            EditObservationBox.Visibility = Visibility.Visible;
        }

        private void DeleteObservationButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
      
        private void CancelNewObserverButton_Click(object sender, RoutedEventArgs e)
        {
            AddObserverBox.Visibility = Visibility.Hidden;
        }

        private void SubmitObservationButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void CancelObservationButton_Click(object sender, RoutedEventArgs e)
        {
            EditObservationBox.Visibility = Visibility.Hidden;
        }

        private void AddMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            AddMeasurementBox.Visibility = Visibility.Visible;
            //TODO populate listOfNewMeasurements
        }

        private void EditMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            EditMeasurementBox.Visibility = Visibility.Visible;
        }

        private void DeleteMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void SubmitNewObservationButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO commit mätpunktslista efter skapad observation med id.
        }

        private void CancelNewObservationButton_Click(object sender, RoutedEventArgs e)
        {
            listOfNewMeasurements = null;
            AddObservationBox.Visibility = Visibility.Hidden;
        }

        private void SubmitEditMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void CancelEditMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            EditMeasurementBox.Visibility = Visibility.Hidden;
        }

        private void SubmitNewMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO add measurement to listOfNewMeasurements
        }

        private void CancelNewMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            AddMeasurementBox.Visibility = Visibility.Hidden;
            listOfNewMeasurements = null;
        }

        private void SubmitEditObserverButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO commit to db
        }

        private void CancelEditObserverButton_Click(object sender, RoutedEventArgs e)
        {
            EditObserverBox.Visibility = Visibility.Hidden;
            // we dont't clear the fields, because they get populated every time anyways
        }

        //private void AddNewMeasurementButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AddMeasurementBox.Visibility = Visibility.Visible;
        //}

        //private void EditNewMeasurementButton_Click(object sender, RoutedEventArgs e)
        //{
        //    EditMeasurementBox.Visibility = Visibility.Visible;
        //}
    }
}
