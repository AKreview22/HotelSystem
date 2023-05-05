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
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        private HotelContext _context;

        public ReservationForm()
        {
            InitializeComponent();
            _context = new HotelContext();
            PopulateRoomNumbers();

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            if (!ValidateRoomNumber())
            {
                return;
            }

            if (!validateInputs())
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            Reservation reservation = new Reservation
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                BirthDay = (DateTime)BirthDayDatePicker.SelectedDate,
                Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString(),
                PhoneNumber = PhoneNumberTextBox.Text,
                EmailAddress = EmailAddressTextBox.Text,
                NumberOfGuests = int.Parse(NumberOfGuestsTextBox.Text),
                RoomType = (RoomTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                RoomFloor = int.TryParse((RoomFloorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()?.Substring(0, 1), out int roomFloorNumber) ? roomFloorNumber : 0,
                //RoomNumber = int.Parse(RoomNumberTextBox.Text),
                RoomNumber = int.Parse(RoomNumberComboBox.SelectedItem.ToString()),
                PaymentType = ((ComboBoxItem)PaymentTypeComboBox.SelectedItem).Content.ToString(),
                CardType = ((ComboBoxItem)CardTypeComboBox.SelectedItem).Content.ToString(),
                CardNumber = CardNumberTextBox.Text,
                CardExpiration = (DateTime)CardExpirationDatePicker.SelectedDate,
                CardCvc = int.Parse(CardCvcTextBox.Text),
                ArrivalTime = (DateTime)ArrivalTimeDatePicker.SelectedDate,
                LeavingTime = (DateTime)LeavingTimeDatePicker.SelectedDate,
                IsCheckedIn = (bool)IsCheckedInCheckBox.IsChecked,
                NeedsBreakfast = (bool)NeedsBreakfastCheckBox.IsChecked,
                NeedsLunch = (bool)NeedsLunchCheckBox.IsChecked,
                NeedsDinner = (bool)NeedsDinnerCheckBox.IsChecked,
                NeedsCleaning = (bool)NeedsCleaningCheckBox.IsChecked,
                NeedsTowel = (bool)NeedsTowelCheckBox.IsChecked,
                HasSpecialSurprise = (bool)HasSpecialSurpriseCheckBox.IsChecked,
                FoodSupplyStatus = (bool)FoodSupplyStatusCheckBox.IsChecked,
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            int days = (int)(reservation.LeavingTime - reservation.ArrivalTime).TotalDays;

            PopulateRoomNumbers();
            string message = $"Days you are staying for: {days}\nBased on that, Your food bill is {reservation.FoodBill:C2}.\nYour total bill is {reservation.TotalBill:C2}.\nReservation created successfully.";
            MessageBox.Show(message);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var selectedTab = ((TabControl)e.Source).SelectedItem as TabItem;
                if (selectedTab.Header.Equals("Reservations"))
                {
                    // update DataGrid
                    ReservationsDataGrid.ItemsSource = _context.Reservations.ToList();
                }
                 
                if (selectedTab.Header.Equals("New Reservation"))
                {
                    // update ComboBox
                    ToDoListBox.ItemsSource = _context.Reservations.ToList();
                }

                if (selectedTab.Header.Equals("Rooms"))
                {
                    // update DataGrid
                    LoadRoomsData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                // Search for the text in all columns of the Reservations table
                using (var context = new HotelContext())
                {
                    var searchResults = context.Reservations
                        .Where(r =>
                            r.Id.ToString().Equals(searchText) ||
                            r.FirstName.Contains(searchText) ||
                            r.LastName.Contains(searchText) ||
                            r.EmailAddress.Contains(searchText) ||
                            r.Gender.Equals(searchText) ||
                            r.PhoneNumber.Equals(searchText) ||
                            r.RoomNumber.ToString().Equals(searchText) ||
                            r.ArrivalTime.ToString().Contains(searchText) ||
                            r.LeavingTime.ToString().Contains(searchText)
                        )
                        .ToList();

                    SearchResultsDataGrid.ItemsSource = searchResults;
                }
            }
        }

        private void LoadRoomsData()
        {
            using (var context = new HotelContext())
            {
                var checkedInRooms = context.Reservations
                    .Where(r => r.IsCheckedIn)
                    .ToList();

                var reservedRooms = context.Reservations
                    .Where(r => !r.IsCheckedIn)
                    .ToList();

                CheckedInRoomsDataGrid.ItemsSource = checkedInRooms;
                ReservedRoomsDataGrid.ItemsSource = reservedRooms;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the data source for the data grid
            var reservations = ReservationsDataGrid.ItemsSource as List<Reservation>;

            // Update the database with changes made to the data source
            using (var context = new HotelContext())
            {
                foreach (var reservation in reservations)
                {
                    context.Entry(reservation).State = EntityState.Modified;
                }

                context.SaveChanges();
            }

            MessageBox.Show("Changes saved successfully.");
        }

        private bool validateInputs()
        {
            // Check that all required fields are filled in
            if (string.IsNullOrEmpty(FirstNameTextBox.Text) ||
                string.IsNullOrEmpty(LastNameTextBox.Text) ||
                !BirthDayDatePicker.SelectedDate.HasValue ||
                GenderComboBox.SelectedItem == null ||
                string.IsNullOrEmpty(PhoneNumberTextBox.Text) ||
                string.IsNullOrEmpty(EmailAddressTextBox.Text) ||
                string.IsNullOrEmpty(NumberOfGuestsTextBox.Text) ||
                PaymentTypeComboBox.SelectedItem == null ||
                string.IsNullOrEmpty(CardNumberTextBox.Text) ||
                !CardExpirationDatePicker.SelectedDate.HasValue ||
                string.IsNullOrEmpty(CardCvcTextBox.Text) ||
                !ArrivalTimeDatePicker.SelectedDate.HasValue ||
                !LeavingTimeDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }

            // Check that all numeric inputs are valid integers
            int numberOfGuests;
            if (!int.TryParse(NumberOfGuestsTextBox.Text, out numberOfGuests))
            {
                MessageBox.Show("Please enter a valid number for Number of Guests.");
                return false;
            }

            if (RoomNumberComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a room number.");
                return false;
            }

            int cardCvc;
            if (!int.TryParse(CardCvcTextBox.Text, out cardCvc))
            {
                MessageBox.Show("Please enter a valid number for Card CVC.");
                return false;
            }

            // All inputs are valid
            return true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CardExpirationDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker.SelectedDate < DateTime.Now.Date)
            {
                datePicker.SelectedDate = DateTime.Now.Date;
            }
        }

        private void ArrivalTimeDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure ArrivalTimeDatePicker.SelectedDate is not in the past
            if (ArrivalTimeDatePicker.SelectedDate < DateTime.Today)
            {
                ArrivalTimeDatePicker.SelectedDate = DateTime.Today;
            }

            // Ensure LeavingTimeDatePicker.SelectedDate is after ArrivalTimeDatePicker.SelectedDate
            if (LeavingTimeDatePicker.SelectedDate < ArrivalTimeDatePicker.SelectedDate)
            {
                LeavingTimeDatePicker.SelectedDate = ArrivalTimeDatePicker.SelectedDate;
            }
        }

        private void LeavingTimeDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure LeavingTimeDatePicker.SelectedDate is not in the past
            if (LeavingTimeDatePicker.SelectedDate < DateTime.Today)
            {
                LeavingTimeDatePicker.SelectedDate = DateTime.Today;
            }

            // Ensure LeavingTimeDatePicker.SelectedDate is after ArrivalTimeDatePicker.SelectedDate
            if (LeavingTimeDatePicker.SelectedDate < ArrivalTimeDatePicker.SelectedDate)
            {
                LeavingTimeDatePicker.SelectedDate = ArrivalTimeDatePicker.SelectedDate;
            }
        }

        private void BirthDayDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure BirthDayDatePicker.SelectedDate is at least 16 years old
            DateTime currentDate = DateTime.Now;
            DateTime minDate = currentDate.AddYears(-16);

            if (BirthDayDatePicker.SelectedDate > minDate)
            {
                BirthDayDatePicker.SelectedDate = minDate;
            }
        }

        private bool ValidateRoomNumber()
        {
            // If the selected item in the combobox is not a valid integer, exit the method
            if (!int.TryParse(RoomNumberComboBox.SelectedItem?.ToString(), out int roomNumber))
            {
                MessageBox.Show("Enter Numbers only");
                return false;
            }

            if (LeavingTimeDatePicker.SelectedDate == null || ArrivalTimeDatePicker.SelectedDate == null)
            {
                // Display an error message to the user or handle the null values in some other way
                MessageBox.Show("Enter Arrival and leaving dates first");
                return false;
            }

            using var context = new HotelContext();
            // Check if there are any reservations that conflict with the selected leaving time and room number
            DateTime leavingTime = LeavingTimeDatePicker.SelectedDate.Value;
            DateTime arrivingTime = ArrivalTimeDatePicker.SelectedDate.Value;
            bool hasConflict = context.Reservations.Any(r => r.RoomNumber == roomNumber &&
                                                             r.ArrivalTime < leavingTime &&
                                                             r.LeavingTime > arrivingTime);

            if (hasConflict)
            {
                // Display an error message to the user
                MessageBox.Show("There is already a reservation for this room with an arriving time that conflicts with the selected leaving time.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void EditReservationButton_Click(object sender, RoutedEventArgs e)
        {

            ShowEdit();

            // Show a message to indicate that editing mode has been enabled
            MessageBox.Show("Editing mode enabled.");
        }
        private void todolistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Reservation selectedReservation = (Reservation)e.AddedItems[0];

                // Populate the input fields with the reservation data
                FirstNameTextBox.Text = selectedReservation.FirstName;
                LastNameTextBox.Text = selectedReservation.LastName;
                BirthDayDatePicker.SelectedDate = selectedReservation.BirthDay;
                GenderComboBox.SelectedItem = GenderComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == selectedReservation.Gender);
                PhoneNumberTextBox.Text = selectedReservation.PhoneNumber;
                EmailAddressTextBox.Text = selectedReservation.EmailAddress;
                ArrivalTimeDatePicker.SelectedDate = selectedReservation.ArrivalTime;
                LeavingTimeDatePicker.SelectedDate = selectedReservation.LeavingTime;
                NumberOfGuestsTextBox.Text = selectedReservation.NumberOfGuests.ToString();
                RoomTypeComboBox.SelectedItem = RoomTypeComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == selectedReservation.RoomType);
                RoomFloorComboBox.SelectedItem = RoomFloorComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString().StartsWith(selectedReservation.RoomFloor.ToString()));

                //RoomNumberTextBox.Text = selectedReservation.RoomNumber.ToString();

                int newRoomNumber = selectedReservation.RoomNumber;
                // Get the current items source
                var itemsSource = RoomNumberComboBox.ItemsSource as IEnumerable<int>;

                // Create a new collection with the new item
                var newItemsSource = new List<int>(itemsSource) { newRoomNumber };

                // Set the new collection as the items source of the ComboBox
                RoomNumberComboBox.ItemsSource = newItemsSource;

                // Select the new item
                RoomNumberComboBox.SelectedItem = newRoomNumber;



                PaymentTypeComboBox.SelectedItem = PaymentTypeComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == selectedReservation.PaymentType);
                CardTypeComboBox.SelectedItem = CardTypeComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == selectedReservation.CardType);
                CardNumberTextBox.Text = selectedReservation.CardNumber;
                CardExpirationDatePicker.SelectedDate = selectedReservation.CardExpiration;
                CardCvcTextBox.Text = selectedReservation.CardCvc.ToString();
                IsCheckedInCheckBox.IsChecked = selectedReservation.IsCheckedIn;
                NeedsBreakfastCheckBox.IsChecked = selectedReservation.NeedsBreakfast;
                NeedsLunchCheckBox.IsChecked = selectedReservation.NeedsLunch;
                NeedsDinnerCheckBox.IsChecked = selectedReservation.NeedsDinner;
                NeedsCleaningCheckBox.IsChecked = selectedReservation.NeedsCleaning;
                NeedsTowelCheckBox.IsChecked = selectedReservation.NeedsTowel;
                HasSpecialSurpriseCheckBox.IsChecked = selectedReservation.HasSpecialSurprise;
                FoodSupplyStatusCheckBox.IsChecked = selectedReservation.FoodSupplyStatus;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            if (!validateInputs())
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Get the ID of the reservation to edit from the selected item
            if (!(ToDoListBox.SelectedItem is Reservation selectedReservation))
            {
                MessageBox.Show("Please select a reservation to edit.");
                return;
            }

            // Get the reservation from the database
            var reservation = _context.Reservations.Find(selectedReservation.Id);

            if (reservation == null)
            {
                MessageBox.Show($"Reservation with ID {selectedReservation.Id} not found.");
                return;
            }

            // Update the reservation properties with the new values
            reservation.FirstName = FirstNameTextBox.Text;
            reservation.LastName = LastNameTextBox.Text;
            reservation.BirthDay = (DateTime)BirthDayDatePicker.SelectedDate;
            reservation.Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString();
            reservation.PhoneNumber = PhoneNumberTextBox.Text;
            reservation.EmailAddress = EmailAddressTextBox.Text;
            reservation.ArrivalTime = (DateTime)ArrivalTimeDatePicker.SelectedDate;
            reservation.LeavingTime = (DateTime)LeavingTimeDatePicker.SelectedDate;
            reservation.NumberOfGuests = int.Parse(NumberOfGuestsTextBox.Text);
            reservation.RoomType = ((ComboBoxItem)RoomTypeComboBox.SelectedItem).Content.ToString();
            reservation.RoomFloor = int.TryParse((RoomFloorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()?.Substring(0, 1), out int roomFloorNumber) ? roomFloorNumber : 0;
            //reservation.RoomNumber = int.Parse(RoomNumberTextBox.Text);
            reservation.RoomNumber = int.Parse(RoomNumberComboBox.SelectedItem.ToString());
            reservation.PaymentType = ((ComboBoxItem)PaymentTypeComboBox.SelectedItem).Content.ToString();
            reservation.CardType = ((ComboBoxItem)CardTypeComboBox.SelectedItem).Content.ToString();
            reservation.CardNumber = CardNumberTextBox.Text;
            reservation.CardExpiration = CardExpirationDatePicker.SelectedDate;
            reservation.CardCvc = int.Parse(CardCvcTextBox.Text);
            reservation.IsCheckedIn = (bool)IsCheckedInCheckBox.IsChecked;
            reservation.NeedsBreakfast = (bool)NeedsBreakfastCheckBox.IsChecked;
            reservation.NeedsLunch = (bool)NeedsLunchCheckBox.IsChecked;
            reservation.NeedsDinner = (bool)NeedsDinnerCheckBox.IsChecked;
            reservation.NeedsCleaning = (bool)NeedsCleaningCheckBox.IsChecked;
            reservation.NeedsTowel = (bool)NeedsTowelCheckBox.IsChecked;
            reservation.HasSpecialSurprise = (bool)HasSpecialSurpriseCheckBox.IsChecked;
            reservation.FoodSupplyStatus = (bool)FoodSupplyStatusCheckBox.IsChecked;

            // Save the changes to the database
            _context.SaveChanges();

            // Display a message to the user indicating that the update was successful
            MessageBox.Show("Reservation updated successfully.");

            HideEdit();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the ID of the reservation to delete from the selected item
            if (!(ToDoListBox.SelectedItem is Reservation selectedReservation))
            {
                MessageBox.Show("Please select a reservation to delete.");
                return;
            }

            int id = selectedReservation.Id;

            var reservation = _context.Reservations.Find(id);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();

                MessageBox.Show("Reservation deleted successfully.");

                HideEdit();
            }
            else
            {
                MessageBox.Show("Reservation not found.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HideEdit();
        }
        private void ShowEdit()
        {
            // Show List
            ToDoListBox.Visibility = Visibility.Visible;

            // Enable the Update button
            UpdateButton.IsEnabled = true;
            UpdateButton.Opacity = 1;

            // Enable the Delete button
            DeleteButton.IsEnabled = true;
            DeleteButton.Opacity = 1;

            // Enable the Cancel button
            CancelButton.IsEnabled = true;
            CancelButton.Opacity = 1;

            // Disable the Submit button
            SubmitButton.IsEnabled = false;
            SubmitButton.Opacity = 0.3;

            // Disable the Edit button
            EditButton.IsEnabled = false;
            EditButton.Opacity = 0.3;

            ToDoListBox.ItemsSource = _context.Reservations.ToList();
            PopulateRoomNumbers();

        }

        private void HideEdit()
        {
            // Hide List
            ToDoListBox.Visibility = Visibility.Hidden;

            // Enable the Submit button
            SubmitButton.IsEnabled = true;
            SubmitButton.Opacity = 1;

            // Enable the Edit button
            EditButton.IsEnabled = true;
            EditButton.Opacity = 1;

            // Disable the Update button
            UpdateButton.IsEnabled = false;
            UpdateButton.Opacity = 0.3;

            // Disable the Delete button
            DeleteButton.IsEnabled = false;
            DeleteButton.Opacity = 0.3;

            // Disable the Cancel button
            CancelButton.IsEnabled = false;
            CancelButton.Opacity = 0;

            ToDoListBox.ItemsSource = _context.Reservations.ToList();
            PopulateRoomNumbers();

        }

        private void PopulateRoomNumbers()
        {
            List<int> roomNumbers = new List<int>();
            for (int floor = 1; floor <= 5; floor++)
            {
                for (int room = 1; room <= 10; room++)
                {
                    int roomNumber = floor * 100 + room;
                    roomNumbers.Add(roomNumber);
                }
            }

            using var context = new HotelContext();
            // Remove reserved rooms from the list
            foreach (var reservation in context.Reservations)
            {
                roomNumbers.Remove(reservation.RoomNumber);
            }

            RoomNumberComboBox.ItemsSource = roomNumbers;
        }





    }
}
