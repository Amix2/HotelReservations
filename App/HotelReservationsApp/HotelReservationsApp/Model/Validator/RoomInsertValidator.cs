using HotelReservationsApp.DBModels;
using Microsoft.EntityFrameworkCore;
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
            var roomInDBList = dataConnection.GetEntitiesWithFilter<Rooms>(room => room.RoomNumber == entity.RoomNumber);
            if(roomInDBList.Count() > 0)
            {   // room already exists in db, dont add, just check fields
                var roomInDB = roomInDBList.First();
                if (roomInDB.Equals(entity))
                {
                    result = new Result(ResultType.ALREADY_IN_DATABASE_IDENTICAL);
                }
                else
                {
                    result = new Result(ResultType.ALREADY_IN_DATABASE_DIFFERENT);
                }
                return false;
            }
            result = new Result(ResultType.SUCCESS);
            return true;
        }
    }
}
