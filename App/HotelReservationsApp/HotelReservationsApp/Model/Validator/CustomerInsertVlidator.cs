using HotelReservationsApp.DBModels;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationsApp.Model.Validator
{
    internal class CustomerInsertVlidator : IEntityValidator<Customers>
    {
        public bool Validate(Customers entity, DataConnection dataConnection, out Result result)
        {
            List<string> errors = new List<string>();
            if (!SpecificChecks.ValidateEmail(entity.Email))
            {
                errors.Add("email is incorrect");
            }
            if (!SpecificChecks.ValidatePhone(entity.Phone))
            {
                errors.Add("phone number is incorrect");
            }
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