using System;
using System.Collections.Generic;

namespace HotelReservationsApp.DBModels
{
    public partial class Rooms
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Rooms)) return false;
            Rooms other = (Rooms)obj;
            return other.RoomNumber == RoomNumber
                && other.RoomSize == RoomSize
                && other.RoomType == RoomType
                && other.PriceForNight == PriceForNight
                && other.FloorNumber == FloorNumber
                && other.Capacity == Capacity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoomNumber, FloorNumber, RoomType, RoomSize, Capacity, PriceForNight, Reservations);
        }
    }
}
