using System.Collections.Generic;

namespace HotelReservationsApp.DBModels
{
    public partial class Rooms
    {
        public Rooms()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int RoomNumber { get; set; }
        public int? FloorNumber { get; set; }
        public string RoomType { get; set; }
        public int? RoomSize { get; set; }
        public int? Capacity { get; set; }
        public decimal? PriceForNight { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}