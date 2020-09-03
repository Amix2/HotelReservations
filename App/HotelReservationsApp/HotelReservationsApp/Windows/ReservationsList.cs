using HotelReservationsApp.DBModels;
using HotelReservationsApp.Model;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    internal class ReservationsList
    {
        private ListView ListView;
        private Label InfoLabel;
        private DataConnection dataConnection;

        public ReservationsList(ListView listView, Label infoLabel, DataConnection dataConnection)
        {
            ListView = listView;
            InfoLabel = infoLabel;
            this.dataConnection = dataConnection;

            FillListVIew(dataConnection);
            dataConnection.OnDBChange += (DbContext, t) =>
            {
                if (t == typeof(Reservations))
                {
                    FillListVIew(dataConnection);
                }
            };
        }

        private void FillListVIew(DataConnection dataConnection)
        {
            ListView.Items.Clear();
            var reservations = dataConnection.AllReservations();
            foreach (Reservations reservation in reservations)
            {
                ListView.Items.Add(new ReservationListViewItem(reservation));
            }
        }

        public void ItemSelected(Reservations reservation)
        {
            if (reservation != null)
            {
                InfoLabel.Content = reservation.GetFullDescription();
            }
            else
            {
                InfoLabel.Content = string.Empty;
            }
        }
    }
}