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
using Pra.Vakantieverhuur.CORE.Entities;
using Pra.Vakantieverhuur.CORE.Services;

namespace Pra.Vakantieverhuur.WPF
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
        Residences residences;
        Rentals rentals;
        Tenants tenants;

        public List<Rental> allRentals;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            residences = new Residences();
            rentals = new Rentals();
            tenants = new Tenants();

            lstResidences.ItemsSource = residences.AllResidences;
            allRentals = rentals.AllRentals;


        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void cmbKindOfResidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cmbKindOfResidence.IsLoaded) 
                return;
            dgrRentals.Items.Clear();

            lstResidences.ItemsSource = null;
            if (cmbKindOfResidence.SelectedIndex == 0) lstResidences.ItemsSource = residences.AllResidences;
            else if (cmbKindOfResidence.SelectedIndex == 1) lstResidences.ItemsSource = residences.AllHolidayHomes;
            else lstResidences.ItemsSource = residences.AllCaravans;
        }

        private void lstResidences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgrRentals.Items.Clear();
            if (lstResidences.SelectedItem == null) return;

            Residence residence = (Residence)lstResidences.SelectedItem;
            foreach (Rental rental in allRentals)
            {
                if (rental.HollidayResidence == residence)
                {
                    dgrRentals.Items.Add(rental);
                }
            }
        }

        private void btnResidenceView_Click(object sender, RoutedEventArgs e)
        {
            if (lstResidences.SelectedIndex == -1) return;
            WinResidences winResidences = new WinResidences();
            winResidences.status = "view";
            winResidences.selectedResidence = (Residence)lstResidences.SelectedItem;
            winResidences.ShowDialog();
        }
        private void btnResidenceNew_Click(object sender, RoutedEventArgs e)
        {
            WinResidences winResidences = new WinResidences();
            winResidences.status = "new";
            winResidences.selectedResidence = null;
            winResidences.ShowDialog();

            if (winResidences.selectedResidence != null)
            {
                cmbKindOfResidence.SelectedIndex = 0;
                lstResidences.ItemsSource = null;
                residences.AllResidences.Add(winResidences.selectedResidence);
                // niet vergeten : ook AllHolidayHomes en AllCaravans dienen bijgewerkt te worden
                if (winResidences.selectedResidence is HolidayHome)
                    residences.AllHolidayHomes.Add((HolidayHome)winResidences.selectedResidence);
                else
                    residences.AllCaravans.Add((Caravan)winResidences.selectedResidence);
                lstResidences.ItemsSource = residences.AllResidences;
                lstResidences.SelectedItem = winResidences.selectedResidence;
            }
        }

        private void btnResidenceEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstResidences.SelectedIndex == -1) return;

            WinResidences winResidences = new WinResidences();
            winResidences.status = "edit";
            winResidences.selectedResidence = (Residence)lstResidences.SelectedItem;
            winResidences.ShowDialog();

            cmbKindOfResidence_SelectionChanged(null, null);
            lstResidences.SelectedItem = winResidences.selectedResidence;
        }

        private void btnResidenceDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstResidences.SelectedIndex == -1) return;

            if(MessageBox.Show("Vakantieverblijf verwijderen?","Delete",MessageBoxButton.YesNo, MessageBoxImage.Warning,MessageBoxResult.No ) == MessageBoxResult.Yes)
            {
                foreach(Rental rental in rentals.AllRentals)
                {
                    if(rental.HollidayResidence == (Residence) lstResidences.SelectedItem)
                    {
                        rentals.AllRentals.Remove(rental);
                    }

                }
                residences.AllResidences.Remove((Residence)lstResidences.SelectedItem);
                if ((Residence)lstResidences.SelectedItem is HolidayHome)
                    residences.AllHolidayHomes.Remove((HolidayHome)lstResidences.SelectedItem);
                else
                    residences.AllCaravans.Remove((Caravan)lstResidences.SelectedItem);

                cmbKindOfResidence_SelectionChanged(null, null);

            }

        }

        private void btnNewRental_Click(object sender, RoutedEventArgs e)
        {
            if (lstResidences.SelectedIndex == -1) return;

            if (!((Residence)lstResidences.SelectedItem).IsRentable)
            {
                MessageBox.Show("Dit verblijf kan momenteel niet verhuurd worden", "Niet toegelaten", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            WinRental winRental = new WinRental();
            winRental.status = "new";
            winRental.selectedResidence = (Residence)lstResidences.SelectedItem;
            winRental.AllRentals = allRentals;
            winRental.ShowDialog();


            dgrRentals.Items.Clear();
            allRentals.Add(winRental.selectedRental);
            lstResidences_SelectionChanged(null, null);
        }

        private void btnRentalEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Rental rental = (Rental)button.DataContext;

            WinRental winRental = new WinRental();
            winRental.status = "edit";
            winRental.selectedResidence = rental.HollidayResidence;
            winRental.selectedRental = rental;
            winRental.ShowDialog();

            lstResidences_SelectionChanged(null, null);
        }

        private void btnRentalDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Rental rental = (Rental)button.DataContext;

            if (MessageBox.Show("Deze verhuur annuleren?", "Verhuur verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                rentals.AllRentals.Remove(rental);
            }

            lstResidences_SelectionChanged(null, null);
        }


    }
}
