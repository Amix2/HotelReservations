using System;

namespace HotelReservationsApp.DBModels
{
    public partial class Rooms
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Rooms)) return false;
            Rooms other = (Rooms)obj;
            return other.RoomNumber == RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoomNumber, FloorNumber, RoomType, RoomSize, Capacity, PriceForNight, Reservations);
        }
    }
}