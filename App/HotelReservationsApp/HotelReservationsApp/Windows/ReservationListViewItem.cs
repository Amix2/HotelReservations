using HotelReservationsApp.DBModels;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    internal class ReservationListViewItem : ListViewItem
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