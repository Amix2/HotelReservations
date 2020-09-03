using HotelReservationsApp.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsApp.Model.Validator
{
    class RoomInsertValidator : IEntityValidator<Rooms>
    {
        public bool Validate(Rooms entity, DataConnection dataConnection, out Result result)
        {
            List<string> errors = new List<string>();
            if (entity.RoomSize < 0) errors.Add("Room Size is less than 0");
            if (entity.PriceForNight < 0) errors.Add("Price is less than 0");
            if (entity.Capacity < 0) errors.Add("Capacity is below 0");
            if (errors.Count > 0)
            {
                result = new Result(ResultType.WRONG_PARAMETER, errors.Join());
                return false;
            }
            result = new Result(ResultType.SUCCESS);
            return true;
        }
    }
}
