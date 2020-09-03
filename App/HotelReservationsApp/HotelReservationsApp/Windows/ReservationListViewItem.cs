using HotelReservationsApp.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    class ReservationListViewItem : ListViewItem
    {
        public Reservations Reservation { get; private set; }

        public ReservationListViewItem(Reservations reservation) : base()
        {
            this.Reservation = reservation;
            AddText(reservation.ToString());
        }

        public override string ToString()
        {
            return Reservation.ToString();
        }
    }
}
