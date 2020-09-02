using HotelReservationsApp.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsApp.Model.Validator
{
    class CustomerInsertVlidator : IEntityValidator<Customers>
    {

        public bool Validate(Customers entity, DataConnection dataConnection, out Result result)
        {
            var entityInDBList = dataConnection.GetEntitiesWithFilter<Customers>(customer => customer.Name == entity.Name && customer.Surname == entity.Surname);
            if (entityInDBList.Count() > 0)
            {   // room already exists in db, dont add, just check fields
                var entityInDB = entityInDBList.First();
                if (entityInDB.Equals(entity))
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
