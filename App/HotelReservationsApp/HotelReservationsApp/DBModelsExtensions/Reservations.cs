using System;

namespace HotelReservationsApp.DBModels
{
    public partial class Reservations
    {
        public override bool Equals(object obj)
        {
            return obj is Reservations reservations &&
                   Id == reservations.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerId, RoomId, StartDate, EndDate, VisitorsCount, Customer, Room);
        }

        public override string ToString()
        {
            return string.Format("{3} - {4}: Room: {0}; {1} {2}", RoomId, Customer.Name, Customer.Surname, StartDate.ToString("d"), EndDate.ToString("d"));
        }

        public string GetFullDescription()
        {
            return string.Format("Room number:\t {0}\nRoom type:\t {1}\nCustomer:\t {2} {3}\nContact:\t {4}, {5}\nTime:\t {6} - {7}\nVisitors count:\t {8}"
                , RoomId, Room.RoomType, Customer.Name, Customer.Surname, Customer.Email, Customer.Phone, StartDate.ToString("d"), EndDate.ToString("d"), VisitorsCount);
        }
    }
}