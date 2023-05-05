using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Context.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int NumberOfGuests { get; set; }
        public string RoomType { get; set; }
        public int RoomFloor { get; set; }
        public int RoomNumber { get; set; }
        public decimal TotalBill
        {
            get
            {
                // Calculate days stayed
                int days = (int)(LeavingTime - ArrivalTime).TotalDays;
                decimal dailyRate = 100; // Example rate

                // Calculate total based on daily rate and food bill
                decimal total = days * dailyRate + FoodBill;

                return total;
            }
        }
        public string PaymentType { get; set; }
        public string? CardType { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? CardExpiration { get; set; }
        public int? CardCvc { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime LeavingTime { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool NeedsBreakfast { get; set; }
        public bool NeedsLunch { get; set; }
        public bool NeedsDinner { get; set; }
        public bool NeedsCleaning { get; set; }
        public bool NeedsTowel { get; set; }
        public bool HasSpecialSurprise { get; set; }
        public bool FoodSupplyStatus { get; set; }
        public decimal FoodBill {
            get
            {
                // Calculate days stayed
                int days = (int)(LeavingTime - ArrivalTime).TotalDays;

                decimal dinnerPrice = NeedsDinner ? 12 : 0;
                decimal lunchPrice = NeedsLunch ? 20 : 0;
                decimal breakfastPrice = NeedsBreakfast ? 5 : 0;

                decimal foodPerDay = dinnerPrice + lunchPrice + breakfastPrice;


                // Calculate total based on daily rate
                decimal total = days * foodPerDay;

                return total;
            }
        }
    }

}
