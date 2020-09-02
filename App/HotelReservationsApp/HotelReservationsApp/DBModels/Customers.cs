using System;
using System.Collections.Generic;

namespace HotelReservationsApp.DBModels
{
    public partial class Customers
    {
        public Customers()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
