using System;
using System.Collections.Generic;

namespace HotelReservationsApp.DBModels
{
    public partial class Reservations
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? VisitorsCount { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Rooms Room { get; set; }
    }
}
