using HotelManagementSystem.Context.Entities;
using HotelManagementSystem.Context;
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
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HotelManagementSystem
{
    /// <summary>
    /// Interaction logic for KitchenForm.xaml
    /// </summary>
    public partial class KitchenForm : Window
    {
        private HotelContext _context;

        public KitchenForm()
        {
            InitializeComponent();
            _context = new HotelContext();
        }

        

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var selectedTab = ((TabControl)e.Source).SelectedItem as TabItem;
                if (selectedTab.Header.Equals("Overview"))
                {
                    // update DataGrid
                    ReservationsDataGrid.ItemsSource = _context.Reservations.ToList();
                }
                else if (selectedTab.Header.Equals("ToDo"))
                {
                    // update ComboBox
                    ToDoListBox.ItemsSource = _context.Reservations.ToList();
                }
            }
        }

        private void todolistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Reservation selectedReservation = (Reservation)e.AddedItems[0];

                // Update text blocks
                FirstNameTextBlock.Text = selectedReservation.FirstName;
                LastNameTextBlock.Text = selectedReservation.LastName;
                BirthDayTextBlock.Text = selectedReservation.BirthDay.ToString("d");
                GenderTextBlock.Text = selectedReservation.Gender;
                PhoneNumberTextBlock.Text = selectedReservation.PhoneNumber;
                EmailAddressTextBlock.Text = selectedReservation.EmailAddress;
                NumberOfGuestsTextBlock.Text = selectedReservation.NumberOfGuests.ToString();
                RoomTypeTextBlock.Text = selectedReservation.RoomType;
                RoomFloorTextBlock.Text = selectedReservation.RoomFloor.ToString();
                RoomNumberTextBlock.Text = selectedReservation.RoomNumber.ToString();

                // Set checkbox values
                NeedsBreakfastCheckBox.IsChecked = selectedReservation.NeedsBreakfast;
                NeedsLunchCheckBox.IsChecked = selectedReservation.NeedsLunch;
                NeedsDinnerCheckBox.IsChecked = selectedReservation.NeedsDinner;
                NeedsCleaningCheckBox.IsChecked = selectedReservation.NeedsCleaning;
                NeedsTowelCheckBox.IsChecked = selectedReservation.NeedsTowel;
                HasSpecialSurpriseCheckBox.IsChecked = selectedReservation.HasSpecialSurprise;
                FoodSupplyCheckBox.IsChecked = selectedReservation.FoodSupplyStatus;
            }
        }

        private void FoodSupplyCheckBox_Checking(object sender, RoutedEventArgs e)
        {
            Reservation selectedReservation = (Reservation)ToDoListBox.SelectedItem;
            var reservation = _context.Reservations.Find(selectedReservation.Id);
            reservation.FoodSupplyStatus= FoodSupplyCheckBox.IsChecked ?? false;
            _context.SaveChanges();

        }





    }
}
