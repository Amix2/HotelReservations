using HotelReservationsApp.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationsApp.Model
{
    internal static class DataConnectionExtension
    {
        public static List<Reservations> AllReservations(this DataConnection _)
        {
            using (HotelReservationsContext context = new HotelReservationsContext())
            {
                return context.Set<Reservations>().Include(reservation => reservation.Room).Include(reservation => reservation.Customer).ToList();
            }
        }
    }
}