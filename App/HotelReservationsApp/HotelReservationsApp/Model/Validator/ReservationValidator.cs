using HotelReservationsApp.DBModels;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsApp.Model.Validator
{
    class ReservationInsertValidator : IEntityValidator<Reservations>
    {
        public bool Validate(Reservations entity, DataConnection dataConnection, out Result result)
        {
            List<string> errors = new List<string>();
            if (entity.RoomId == -1) errors.Add("Room Id not set");
            if (entity.CustomerId == -1) errors.Add("Customer Id not set");
            if (entity.VisitorsCount < 0) errors.Add("Visitors count is below 0");
            if(errors.Count > 0)
            {
                result = new Result(ResultType.WRONG_PARAMETER, errors.Join());
                return false;
            }
            
            result = new Result(ResultType.SUCCESS);
            return true;
        }
    }
}
