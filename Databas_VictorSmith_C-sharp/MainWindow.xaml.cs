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
        public Observer selectedObserver;
        IEnumerable<Observer> listOfObservers;
        readonly IEnumerable<Observer> initialListOfObservers = CRUD.UpdateObserverList();
        #endregion

        #region METHODS
        public void FetchObservers()
        {
            listOfObservers = CRUD.UpdateObserverList();
            Observers.ItemsSource = null;
            Observers.ItemsSource = listOfObservers;
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
            selectedObserver = CRUD.GetObserver((sender as ListBox).SelectedItem as Observer);
        }

        private void SubmitNewObserverButton_Click(object sender, RoutedEventArgs e)
        {
            // Lägger till observatörer med För- och efternamn.
            string firstName, lastName;
            firstName = NameNewObserverInput.Text.ToString();
            lastName = FamilyNameNewObserverInput.Text.ToString();
            CRUD.AddObserver(firstName, lastName);
            FetchObservers();
        }

        private void DeleteObserverButton_Click(object sender, RoutedEventArgs e)
        {
            CRUD.DeleteObserver(selectedObserver);
            // Uppdaterar listan för att reflektera vilka observatörer som existerar.
            FetchObservers();
        }
    }
}
