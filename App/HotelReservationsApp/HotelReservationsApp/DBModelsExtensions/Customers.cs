using System;

namespace HotelReservationsApp.DBModels
{
    public partial class Customers
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Customers)) return false;
            Customers other = (Customers)obj;
            return other.Name == Name
                && other.Surname == Surname
                && other.Email == Email
                && other.Phone == Phone;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Surname, Email, Phone, Reservations);
        }
    }
}