﻿using System;
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
    /// Measurements edited on existing added to an internal list until submitted.
    /// Measurements deleted will be deleted from an internal list and added to a deletionlist until submitted.
    /// When editing an observation the measurement-logic access observationBeingEdited to determine how to handle the data
    /// If editing existing observation:
    /// - add to listOfMeasurements and listOfNewMeasurements and update the listbox
    /// - edit (remove and add edited) from listOfMeasurements and add to listOfUpdatedMeasurements and update the listbox
    /// - delete from listOfMeasurements add to listOfDeletedMeasurements and update the listbox
    /// When submitting 
    /// - add using listOfNewMeasurements
    /// - update using listOfUpdatedMeasurements
    /// - delete using listOfDeletedMeasurements
    /// - clear the lists in the end
    /// If new observation:
    /// - add, edit (remove and add the new version) or delete from listOfNewMeasurements and update the listbox
    /// When submitting 
    /// - add using listOfNewMeasurements
    /// 
    /// The listboxes are:
    /// New Observation: newMeasurements
    /// Existing Observation: observationMeasurements
    /// </summary>
    public partial class MainWindow : Window
    {
        #region INITIALIZATION
        public Observer selectedObserver = null;
        public Observation selectedObservation = null;
        public Measurement measurementBeingEdited = null;
        public Observation observationBeingEdited = null;
        public bool observationBeingAdded = false;
        public bool measurementBeingAdded = false;
        public List<Observer> listOfObservers = null;
        public List<Observation> listOfObservations = null;
        public List<Measurement> listOfNewMeasurements = new List<Measurement>();
        public List<Measurement> listOfMeasurements = null; // used for the user-facing listbox
        public List<Measurement> listOfDeletedMeasurements = null;
        public List<Measurement> listOfUpdatedMeasurements = null;
        public List<Geolocation> listOfGeolocations = null;
        //public List<Area> listOfAreas = null;
        public List<Category> listOfCategories = null;
        public List<Unit> listOfUnits = null;
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
            listOfObservations = CRUD.GetObservationList(observer);
            UpdateObservationsListbox(listOfObservations);
        }
        public void FetchMeasurements(Observation observation)
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchMeasurements");
            listOfMeasurements = CRUD.GetMeasurementList(observation);
            UpdateMeasurementsListbox(listOfMeasurements);
        }
        public void FetchGeolocations()
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchGeolocations");
            listOfGeolocations = CRUD.GetGeolocationList();
            UpdateGeolocationsListbox(listOfGeolocations);
        }
        
        //public void FetchAreas()
        //{
        //    System.Diagnostics.Trace.WriteLine($"MainWindow:FetchAreas");
        //    listOfAreas = CRUD.GetAreaList();
        //    //UpdateAreasListbox(listOfAreas);
        //}
        public void FetchCategories()
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchCategories");
            listOfCategories = CRUD.GetCategoryList();
            UpdateCategoriesListbox(listOfCategories);
        }
        public void FetchUnits()
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:FetchUnits");
            listOfUnits = CRUD.GetUnitList();
            //UpdateUnitsListbox(listOfUnits);
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
        public void UpdateGeolocationsListbox(List<Geolocation> list)
        {
            // We update both at the same time
            editGeolocations.ItemsSource = null;
            editGeolocations.ItemsSource = list;
            Geolocations.ItemsSource = null;
            Geolocations.ItemsSource = list;
        }
        //public void UpdateAreasListbox(List<Area> list)
        //{
        //    // We update both at the same time
        //    editAreas.ItemsSource = null;
        //    editAreas.ItemsSource = list;
        //    Areas.ItemsSource = null;
        //    Areas.ItemsSource = list;
        //}
        public void UpdateCategoriesListbox(List<Category> list)
        {
            // We update both at the same time
            editCategories.ItemsSource = null;
            editCategories.ItemsSource = list;
            Categories.ItemsSource = null;
            Categories.ItemsSource = list;
        }
        //public void UpdateUnitsListbox(List<Unit> list)
        //{
        //    editUnits.ItemsSource = null;
        //    editUnits.ItemsSource = list;
        //    Units.ItemsSource = null;
        //    Units.ItemsSource = list;
        //}
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Observers.ItemsSource = null;
            Observers.ItemsSource = initialListOfObservers;
        }
        #region SELECTION
        private void Observers_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Update the global variable
            System.Diagnostics.Trace.WriteLine($"MainWindow:Observers_SelectionChanged");
            selectedObserver = CRUD.GetObserver((sender as ListBox).SelectedItem as Observer);
            FetchObservations(selectedObserver);
        }
        private void Observations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the global variable
            System.Diagnostics.Trace.WriteLine($"MainWindow:Observations_SelectionChanged");
            selectedObservation = CRUD.GetObservation((sender as ListBox).SelectedItem as Observation);
            //TODO fetch measurements?
            //FetchObservations(selectedObserver);
        }
        #endregion

        #region ADD
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
        private void AddObservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedObserver != null && observationBeingEdited == null)
            {
                AddObservationBox.Visibility = Visibility.Visible;
                FetchGeolocations();
                observationBeingAdded = true;
                //FetchAreas();
            }
            else if (observationBeingEdited != null)
            {
                MessageBox.Show("Redigering pågår. Avbryt eller spara redigeringen av observationen och försök igen.").ToString();
            }
            else
            {
                MessageBox.Show("Ingen observatör vald. Välj observatör först.").ToString();
            }
        }
        private void AddMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            if (measurementBeingEdited == null)
            {
                measurementBeingAdded = true;
                AddMeasurementBox.Visibility = Visibility.Visible;
                FetchCategories();
                FetchUnits();
            }
            else
            {
                MessageBox.Show("Redigering pågår. Avbryt eller spara redigeringen av mätpunkten och försök igen.").ToString();
            }
        }
        #endregion

        #region EDIT
        private void EditObserverButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Inte implementerad än.");
            //if (selectedObserver != null)
            //{
            //    // Add and edit boxes share screen real estate and cannot both be shown
            //    if (AddObserverBox.Visibility == Visibility.Visible)
            //    {
            //        AddObserverBox.Visibility = Visibility.Hidden;
            //        EditObserverBox.Visibility = Visibility.Visible;
            //    }
            //    else
            //    {
            //        EditObserverBox.Visibility = Visibility.Visible;
            //    }

            //    NameObserverInput.Text = selectedObserver.FirstName;
            //    FamilyNameObserverInput.Text = selectedObserver.LastName;
            //}
            //else
            //{
            //    MessageBox.Show("Ingen observatör vald. Välj observatör i listan.").ToString();
            //}
        }
        private void EditObservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedObservation != null && observationBeingAdded == false)
            {
                observationBeingEdited = selectedObservation;
                EditObservationBox.Visibility = Visibility.Visible;
                FetchMeasurements(selectedObservation);
                FetchGeolocations();
                //FetchAreas();
                //TODO fyll i nuvarande GPS punkt i labeln
                if (listOfGeolocations != null)
                {
                    Geolocation location = (Geolocation)listOfGeolocations.Find(g => g.Id == selectedObservation.Geolocation_Id);
                    currentGeolocation.Content = location.ToString();
                }
            }
            else if (observationBeingAdded == true)
            {
                MessageBox.Show("Registrering av ny observation pågår. Avbryt eller spara den nya observationen och försök igen.").ToString();
            }
            else if (listOfGeolocations == null)
            {
                MessageBox.Show("nullfel.").ToString();
            }

            else
            {
                MessageBox.Show("Ingen observation vald. Välj observation i listan.").ToString();
            }
        }
        private void EditMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            if (measurementBeingAdded == false && observationMeasurements.SelectedItem != null)
            {
                measurementBeingEdited = (Measurement)observationMeasurements.SelectedItem;
                EditMeasurementBox.Visibility = Visibility.Visible;
                FetchCategories();
                FetchUnits();
                editedValueInput.Text = measurementBeingEdited.Value.ToString();
            }
            else if (measurementBeingAdded == true && newMeasurements.SelectedItem != null)
            {
                measurementBeingEdited = (Measurement)newMeasurements.SelectedItem;
                EditMeasurementBox.Visibility = Visibility.Visible;
                FetchCategories();
                FetchUnits();
                editedValueInput.Text = measurementBeingEdited.Value.ToString();
            }
            else
            {
                MessageBox.Show("Registrering av ny mätpunkt pågår. Avbryt eller spara den nya mätpunkten och försök igen.").ToString();
            }
        }
        #endregion

        #region SUBMIT
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
        private void SubmitNewObservationButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitNewObservationButton_Click");
            if (Geolocations.SelectedItem != null && listOfNewMeasurements.Count > 0 && selectedObserver != null)
            {
                Geolocation geolocation = (Geolocation)Geolocations.SelectedItem;
                int observationId = CRUD.AddObservation(selectedObserver, geolocation.Id);
                foreach (Measurement measurement in listOfNewMeasurements)
                {
                    CRUD.AddMeasurement(measurement, observationId);
                }
                MessageBox.Show($"Observation tillagd.");
                FetchObservations(selectedObserver);
                AddObservationBox.Visibility = Visibility.Hidden;
                observationBeingAdded = false;
            }
            else if (Geolocations.SelectedItem == null)
            {
                MessageBox.Show($"GPS-punkt saknas.");
            }
            else if (listOfNewMeasurements.Count == 0)
            {
                MessageBox.Show($"Minst ett mätpunkt behövs.");
            }
        }
        private void SubmitNewMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            /// Logic
            /// First we check the input fields
            /// Then we create the new measurement object
            /// Add it to listOfNewMeasurements
            /// Then we branch 
            /// - existing observation
            /// - new observation
            /// Update the relevant listboxes
            /// Hide the UI
            if (Categories.SelectedItem != null && newValueInput.Text != "")
            {
                Category category = (Category)Categories.SelectedItem;
                System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitNewMeasurementButton_Click:category:id:{category.Id.ToString()}:unit_id:{category.Unit_Abbreviation}");
                double value;
                if (Double.TryParse(newValueInput.Text, out value))
                {
                    System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitNewMeasurementButton_Click:value:{value}");
                    Measurement measurement = new Measurement()
                    {
                        Category_Id = category.Id,
                        // We don't know the value because the observation has not been submitted yet
                        Observation_Id = 0,
                        Value = value,
                        Unit_Abbreviation = category.Unit_Abbreviation,
                        Category_Name = category.Category_Name
                    };
                    if (listOfNewMeasurements == null)
                    {
                        List<Measurement> listOfNewMeasurements = new List<Measurement>();
                    }
                    listOfNewMeasurements.Add(measurement);
                    if (observationBeingEdited != null)
                    {
                        System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitNewMeasurementButton_Click:update:observationMeasurements");
                        // add to listOfMeasurements also
                        listOfMeasurements.Add(measurement);
                        observationMeasurements.ItemsSource = null;
                        observationMeasurements.ItemsSource = listOfMeasurements;
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitNewMeasurementButton_Click:update:newMeasurements");
                        // New observation
                        newMeasurements.ItemsSource = null;
                        newMeasurements.ItemsSource = listOfNewMeasurements;
                    }
                    //MessageBox.Show($"Mätpunkt tillagd.");
                    AddMeasurementBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show($"Fel inmatning. Accepterar bara siffror med punkt som kommatecken.");
                }
            }
            else if (Categories.SelectedItem == null)
            {
                MessageBox.Show($"Kategori saknas.");
            }
            else if (editedValueInput.Text == "")
            {
                MessageBox.Show($"Mätvärde saknas.");
            }

        }

        private void SubmitEditObservationButton_Click(object sender, RoutedEventArgs e)
        {
            /// Logic
            /// First we check the fields and the lists
            /// - add using listOfNewMeasurements
            /// - update using listOfUpdatedMeasurements
            /// - delete using listOfDeletedMeasurements
            /// - clear the lists in the end
            /// - update listbox
            
            if (listOfNewMeasurements != null && listOfNewMeasurements.Count > 0)
            {
                foreach (Measurement measurement in listOfNewMeasurements)
                {
                    CRUD.AddMeasurement(measurement, observationBeingEdited.Id);
                }
            }
            if (listOfDeletedMeasurements != null && listOfDeletedMeasurements.Count > 0)
            {
                foreach (Measurement measurement in listOfDeletedMeasurements)
                {
                    CRUD.DeleteMeasurement(measurement);
                }
            }
            if (listOfUpdatedMeasurements != null && listOfUpdatedMeasurements.Count > 0)
            {
                foreach (Measurement measurement in listOfUpdatedMeasurements)
                {
                    CRUD.UpdateMeasurement(measurement);
                }
            }
            listOfNewMeasurements = null;
            listOfDeletedMeasurements = null;
            listOfUpdatedMeasurements = null;
            MessageBox.Show($"Observationen blev uppdaterad.");
            EditObservationBox.Visibility = Visibility.Hidden;
            FetchObservations(selectedObserver);
        }
        private void SubmitEditMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitMeasurementButton_Click");
            if (editCategories.SelectedItem != null && editedValueInput.Text != "")
            {
                // Collect values
                Category category = (Category)editCategories.SelectedItem;
                System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitMeasurementButton_Click:category:id:{category.Id.ToString()}:unit_id:{category.Unit_Abbreviation}");
                double value;
                if (Double.TryParse(editedValueInput.Text, out value))
                {
                    System.Diagnostics.Trace.WriteLine($"MainWindow:SubmitMeasurementButton_Click:value:{value}");
                    Measurement measurement = new Measurement()
                    {
                        Category_Id = category.Id,
                        // We don't know the value because the observation has not been submitted yet
                        Observation_Id = measurementBeingEdited.Id,
                        Value = value,
                        Unit_Abbreviation = category.Unit_Abbreviation,
                        Category_Name = category.Category_Name
                    };
                    if (observationBeingEdited != null)
                    {
                        if (listOfUpdatedMeasurements == null)
                        {
                            List<Measurement> listOfUpdatedMeasurements = new List<Measurement>();
                        }
                        listOfUpdatedMeasurements.Add(measurement);
                        // remove and readd to listOfMeasurements
                        listOfMeasurements.Remove(measurementBeingEdited);
                        listOfMeasurements.Add(measurement);
                        // update listbox
                        observationMeasurements.ItemsSource = null;
                        observationMeasurements.ItemsSource = listOfMeasurements;
                    }
                    else  // new observation
                    {
                        // remove and readd to listOfNewMeasurements
                        listOfNewMeasurements.Remove(measurementBeingEdited);
                        listOfNewMeasurements.Add(measurement);
                        newMeasurements.ItemsSource = null;
                        newMeasurements.ItemsSource = listOfNewMeasurements;
                    }
                    MessageBox.Show($"Mätpunkt tillagd.");
                    EditMeasurementBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show($"Fel inmatning. Accepterar bara siffror med punkt som kommatecken.");
                }
            }
            else if (editCategories.SelectedItem == null)
            {
                MessageBox.Show($"Kategori saknas.");
            }
            else if (editedValueInput.Text == "")
            {
                MessageBox.Show($"Mätvärde saknas.");
            }
            else
            {
                MessageBox.Show($"Okänt fel.");
            }
        }
        private void SubmitEditObserverButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO commit to db
        }
        #endregion

        #region DELETE
        private void DeleteObserverButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedObserver == null)
            {
                MessageBox.Show("Ingen observatör vald. Välj observatör i listan.").ToString();
            }
            else if (observationBeingEdited != null)
            {
                MessageBox.Show("En observation är under redigering. Abryt redigeringen för att kunna radera en observatör.").ToString();
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
        private void DeleteObservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedObservation == null)
            {
                MessageBox.Show("Ingen observation vald. Välj observation i listan.").ToString();
            }
            else if (observationBeingEdited != null)
            {
                MessageBox.Show("En observation är under redigering. Abryt redigeringen för att kunna radera en observation.").ToString();
            }
            else
            {
                // Use the global variable
                CRUD.DeleteObservation(selectedObservation);
                selectedObservation = null;
                // Uppdaterar listan för att reflektera vilka observationer som existerar.
                FetchObservations(selectedObserver);
            }
        }
        private void DeleteMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            if (observationBeingEdited != null && observationMeasurements.SelectedItem != null)
            {
                System.Diagnostics.Trace.WriteLine($"MainWindow:DeleteMeasurementButton_Click:listOfMeasurements.Count:{listOfMeasurements.Count}");
                if (listOfMeasurements != null && listOfMeasurements.Count > 0)
                {
                    Measurement measurement = (Measurement)observationMeasurements.SelectedItem;
                    listOfDeletedMeasurements.Add(measurement);
                    listOfMeasurements.Remove(measurement);
                    observationMeasurements.ItemsSource = null;
                    observationMeasurements.ItemsSource = listOfMeasurements;
                }
                // Silent fail :)
            }
            else if (newMeasurements.SelectedItem != null)
            {
                System.Diagnostics.Trace.WriteLine($"MainWindow:DeleteMeasurementButton_Click:listOfNewMeasurements.Count:{listOfNewMeasurements.Count}");
                if (listOfNewMeasurements != null && listOfNewMeasurements.Count > 0)
                {
                    Measurement measurement = (Measurement)newMeasurements.SelectedItem;
                    listOfNewMeasurements.Remove(measurement);
                    newMeasurements.ItemsSource = null;
                    newMeasurements.ItemsSource = listOfNewMeasurements;
                }
                // Silent fail :)
            }
            else
            {
                MessageBox.Show($"fel.");
            }
        }
        #endregion

        #region CANCEL
        private void CancelNewObserverButton_Click(object sender, RoutedEventArgs e)
        {
            AddObserverBox.Visibility = Visibility.Hidden;
        }
        private void CancelEditObservationButton_Click(object sender, RoutedEventArgs e)
        {
            EditObservationBox.Visibility = Visibility.Hidden;
            observationBeingEdited = null;
        }

        private void CancelNewObservationButton_Click(object sender, RoutedEventArgs e)
        {
            newMeasurements.ItemsSource = null;
            listOfNewMeasurements = null;
            AddObservationBox.Visibility = Visibility.Hidden;
            observationBeingAdded = false;
        }

        private void CancelEditMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            EditMeasurementBox.Visibility = Visibility.Hidden;
            measurementBeingEdited = null;
        }

        private void CancelMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            AddMeasurementBox.Visibility = Visibility.Hidden;
            listOfNewMeasurements = null;
            measurementBeingAdded = false;
        }

        private void CancelEditObserverButton_Click(object sender, RoutedEventArgs e)
        {
            EditObserverBox.Visibility = Visibility.Hidden;
            // we dont't clear the fields, because they get populated every time anyways
        }
        
        private void CancelNewMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Inte implementerad.");
        }
        #endregion
    }
}
