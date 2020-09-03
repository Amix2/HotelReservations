using HotelReservationsApp.DBModels;
using HotelReservationsApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    class ReservationsList
    {
        ListView ListView;
        Label InfoLabel;
        DataConnection dataConnection;

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
            var reservations = dataConnection.GetTable<Reservations>().Include(reservation => reservation.Room).Include(reservation => reservation.Customer);
            foreach (Reservations reservation in reservations)
            {
                ListView.Items.Add(new ReservationListViewItem(reservation));
            }
        }

        public void ItemSelected(Reservations reservation)
        {
            if(reservation != null)
            {
                InfoLabel.Content = reservation.GetFullDescription();
            } else
            {
                InfoLabel.Content = string.Empty;
            }
        }

    }
}
