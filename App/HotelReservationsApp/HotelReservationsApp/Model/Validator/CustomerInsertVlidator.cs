﻿using HotelReservationsApp.DBModels;
using Microsoft.EntityFrameworkCore.Internal;
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
            List<string> errors = new List<string>();
            if(!SpecificChecks.ValidateEmail(entity.Email))
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

            var entityInDBList = dataConnection.GetEntitiesWithFilter<Customers>(customer => customer.Name == entity.Name && customer.Surname == entity.Surname);
            if (entityInDBList.Count() > 0)
            {   // already exists in db, dont add, just check fields
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
