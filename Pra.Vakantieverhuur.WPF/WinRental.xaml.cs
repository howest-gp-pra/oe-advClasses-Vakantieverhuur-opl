using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pra.Vakantieverhuur.CORE.Entities;
using Pra.Vakantieverhuur.CORE.Services;

namespace Pra.Vakantieverhuur.WPF
{
    /// <summary>
    /// Interaction logic for WinRental.xaml
    /// </summary>
    public partial class WinRental : Window
    {
        public string status;
        public Residence selectedResidence;
        public Rental selectedRental;
        public List<Rental> AllRentals;
        public WinRental()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTenants();
            DisplayResidenceData();
            if (status == "new")
            {
                dtpDateStart.SelectedDate = DateTime.Today;
                dtpDateEnd.SelectedDate = DateTime.Today.AddDays(7);
                txtPaid.Text = "0";
                ProcessData();
            }
            else
            {
                cmbTenant.SelectedItem = selectedRental.HollidayTenant;
                dtpDateStart.SelectedDate = selectedRental.DateStart;
                dtpDateEnd.SelectedDate = selectedRental.DateEnd;
                txtPaid.Text = selectedRental.Paid.ToString();
                chkDepositPaid.IsChecked = selectedRental.IsDeposidPaid;
                ProcessData();
            }
        }
        private void LoadTenants()
        {
            Tenants tenants = new Tenants();
            List<Tenant> allTenants = tenants.AllTenants;
            foreach (Tenant tenant in allTenants)
            {
                if (!tenant.IsBlackListed)
                {
                    cmbTenant.Items.Add(tenant);
                }
            }
        }
        private void DisplayResidenceData()
        {
            if (selectedResidence is HolidayHome)
            {
                chkPrivateSanitaryBlock.Visibility = Visibility.Hidden;
                chkDishwasher.Visibility = Visibility.Visible;
                chkWashingMachine.Visibility = Visibility.Visible;
                chkWoodStove.Visibility = Visibility.Visible;

                chkDishwasher.IsChecked = ((HolidayHome)selectedResidence).DishWasher;
                chkWashingMachine.IsChecked = ((HolidayHome)selectedResidence).WashingMachine;
                chkWoodStove.IsChecked = ((HolidayHome)selectedResidence).WoodStove;
                chkPrivateSanitaryBlock.IsChecked = false;

            }
            else
            {
                chkPrivateSanitaryBlock.Visibility = Visibility.Visible;
                chkDishwasher.Visibility = Visibility.Hidden;
                chkWashingMachine.Visibility = Visibility.Hidden;
                chkWoodStove.Visibility = Visibility.Hidden;

                chkDishwasher.IsChecked = false;
                chkWashingMachine.IsChecked = false;
                chkWoodStove.IsChecked = false;
                chkPrivateSanitaryBlock.IsChecked = ((Caravan)selectedResidence).PrivateSanitaryBlock;

            }
            txtResidenceName.Text = selectedResidence.ResidenceName;
            txtStreetAndNumber.Text = selectedResidence.StreetAndNumber;
            txtPostalCode.Text = selectedResidence.PostalCode.ToString();
            txtTown.Text = selectedResidence.Town;
            txtBasePrice.Text = selectedResidence.BasePrice.ToString();
            txtReducedPrice.Text = selectedResidence.ReducedPrice.ToString();
            txtDaysForReduction.Text = selectedResidence.DaysForReduction.ToString();
            txtDeposit.Text = selectedResidence.Deposit.ToString();
            txtMaxPersons.Text = selectedResidence.MaxPersons.ToString();
            chkMicrowave.IsChecked = selectedResidence.Microwave;
            chkTV.IsChecked = selectedResidence.TV;
        }
        private void dtpDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessData();
        }
        private void dtpDateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessData();
        }
        private void txtPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessData();
        }
        private void ProcessData()
        {
            if (!this.IsLoaded) return;

            Rentals rentals = new Rentals();
            if (dtpDateStart.SelectedDate == null) return;
            if (dtpDateEnd.SelectedDate == null) return;
            DateTime dateStart = (DateTime)dtpDateStart.SelectedDate;
            DateTime dateEnd = (DateTime)dtpDateEnd.SelectedDate;
            if (dateEnd <= dateStart)
            {
                dateEnd = dateStart.AddDays(1);
                dtpDateEnd.SelectedDate = dateEnd;
            }

            lblOverbooking.Content = "";
            btnSave.Visibility = Visibility.Visible;
            foreach (Rental rental in AllRentals)
            {
                if (rental.HollidayResidence == selectedResidence && (rental != selectedRental))
                {
                    if (dateStart >= rental.DateStart && dateStart < rental.DateEnd)
                    {
                        lblOverbooking.Content = "OVERBOEKING";
                        btnSave.Visibility = Visibility.Hidden;
                    }
                    if (dateEnd > rental.DateStart && dateEnd <= rental.DateEnd)
                    {
                        lblOverbooking.Content = "OVERBOEKING";
                        btnSave.Visibility = Visibility.Hidden;
                    }
                }
            }


            TimeSpan ts = (TimeSpan)(dtpDateEnd.SelectedDate - dtpDateStart.SelectedDate);
            int numberOfOvernightStays = (int)ts.TotalDays;
            lblNumberOfOvernightStays.Content = numberOfOvernightStays.ToString();

            int daysForReduction = selectedResidence.DaysForReduction;
            decimal toPay = ToPay();
            if (daysForReduction > numberOfOvernightStays)
            {
                lblTotalToPay.Content = $"{numberOfOvernightStays} x {selectedResidence.BasePrice} = {toPay}";
            }
            else
            {
                lblTotalToPay.Content = $"{numberOfOvernightStays} x {selectedResidence.ReducedPrice} = {toPay}";
            }

            decimal.TryParse(txtPaid.Text, out decimal paid);
            txtPaid.Text = paid.ToString();

            decimal toBePaid = toPay - paid;
            lblToBePaid.Content = toBePaid.ToString();

        }
        private decimal ToPay()
        {
            TimeSpan ts = (TimeSpan)(dtpDateEnd.SelectedDate - dtpDateStart.SelectedDate);
            int numberOfOvernightStays = (int)ts.TotalDays;
            int daysForReduction = selectedResidence.DaysForReduction;
            decimal toPay;
            if (daysForReduction > numberOfOvernightStays)
            {
                toPay = numberOfOvernightStays * selectedResidence.BasePrice;
            }
            else
            {
                toPay = numberOfOvernightStays * selectedResidence.ReducedPrice;
            }
            return toPay;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTenant.SelectedIndex == -1)
            {
                cmbTenant.Focus();
                return;
            }
            if (status == "new")
                selectedRental = new Rental();
            selectedRental.HollidayTenant = (Tenant)cmbTenant.SelectedItem;
            selectedRental.HollidayResidence = selectedResidence;
            selectedRental.DateStart = (DateTime)dtpDateStart.SelectedDate;
            selectedRental.DateEnd = (DateTime)dtpDateEnd.SelectedDate;
            selectedRental.IsDeposidPaid = (bool)chkDepositPaid.IsChecked;
            selectedRental.ToPay = ToPay();
            decimal.TryParse(txtPaid.Text, out decimal paid);
            selectedRental.Paid = paid;
            //if (situatie == "new")
            //    Verhuringen.AlleVerhuringen.Add(verhuur);
            this.Close();
        }
    }
}
