using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HotelReservationsApp.DBModels;
using HotelReservationsApp.Model.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelReservationsApp.Model
{
    public class DataConnection
    {
        private readonly HotelReservationsContext hotelReservationsContext;
        
        public Action<HotelReservationsContext, Type> OnDBChange;

        private readonly Dictionary<Type, object> tables;
        private readonly Dictionary<Type, object> insertValidators;

        public Dictionary<Type, object> InsertValidators => insertValidators;

        public DataConnection()
        {
            hotelReservationsContext = new HotelReservationsContext();
            insertValidators = new Dictionary<Type, object>();

            tables = new Dictionary<Type, object>();
            tables.Add(typeof(Rooms), hotelReservationsContext.Rooms);
            tables.Add(typeof(Customers), hotelReservationsContext.Customers);
            tables.Add(typeof(Reservations), hotelReservationsContext.Reservations);
        }

        public Result InsertEntity<T>(T item, bool ignoreValidation = false) where T : class
        {
            if(ContainsEntity(item))
            {
                return new Result(ResultType.ALREADY_IN_DATABASE);
            }
            // validate item if needed
            if (!ignoreValidation)
            {
                Result result = Validate(item);
                if (result.type != ResultType.SUCCESS) return result;
            }

            // add item
            DbSet<T> table = GetTable<T>();
            table.Add(item);
            hotelReservationsContext.SaveChanges();
            OnDBChange?.Invoke(hotelReservationsContext, typeof(T));
            return new Result(ResultType.SUCCESS);
        }

        private Result Validate<T>(T entity) where T: class
        {
            Result result;
            if (insertValidators.TryGetValue(typeof(T), out object validatorObject))
            {
                IEntityValidator<T> validator = (IEntityValidator<T>)validatorObject;
                if (!validator.Validate(entity, this, out result))
                {
                    return result;
                }
            }
            else
            {
                throw new Exception(string.Format("Insert Validator for type {0} is not set, use AddInsertValidator()", typeof(T).ToString()));
            }
            return new Result(ResultType.SUCCESS);
        }

        public IEnumerable<Result> InsertEntity<T>(IEnumerable<T> items, bool ignoreValidation = false) where T : class
        {
            List<T> validatedItems = new List<T>();
            List<Result> results = new List<Result>();

            // validate items if needed
            if (!ignoreValidation)
            {
                if (insertValidators.TryGetValue(typeof(T), out object validatorObject))
                {
                    IEntityValidator<T> validator = (IEntityValidator<T>)validatorObject;
                    foreach(T item in items)
                    {
                        if (validator.Validate(item, this, out Result result))
                        {
                            validatedItems.Add(item);
                            results.Add(result);
                        }
                    }
                }
            } else
            {
                validatedItems = items.ToList();
            }

            if (validatedItems.Count == 0) return results;

            // add item
            DbSet<T> table = GetTable<T>();
            table.AddRange(validatedItems);
            hotelReservationsContext.SaveChanges();
            OnDBChange?.Invoke(hotelReservationsContext, typeof(T));
            return results;
        }

        public void RemoveEntity<T>(T entity)
        {
            hotelReservationsContext.Remove(entity);
            hotelReservationsContext.SaveChanges();
            OnDBChange?.Invoke(hotelReservationsContext, typeof(T));
        }

        public Result EditEntity<T>(T entity) where T:class
        {
            if (!ContainsEntity(entity))
            {
                return new Result(ResultType.NOT_IN_DATABASE);
            }

            Result result = Validate(entity);
            if (result.type != ResultType.SUCCESS) return result;

            hotelReservationsContext.SaveChanges();
            OnDBChange?.Invoke(hotelReservationsContext, typeof(T));

            return new Result(ResultType.SUCCESS);
        }

        public int GetTableCount<T>() where T : class
        {
            var table = GetTable<T>();
            if(table == null)
                return -1;
            return table.Count();
        }

        public DbSet<T> GetTable<T>() where T : class
        {
            if (tables.TryGetValue(typeof(T), out object table))
            {
                return (DbSet<T>)table;
            }
            return null;
        }

        public IQueryable<T> GetEntitiesWithFilter<T>(Expression<Func<T, bool>> filter) where T : class
        {
            var table = GetTable<T>();
            return table.Where<T>(filter);
        }

        public bool ContainsEntity<T>(T entity) where T : class
        {
            return GetTable<T>().Any(x => x.Equals(entity));
        }

        public void AddInsertValidator<T>(IEntityValidator<T> validator) where T : class
        {
            if (insertValidators.ContainsKey(typeof(T))) throw new Exception("Insert Validator for given type already exists");
            insertValidators.Add(typeof(T), validator);
        }

        protected void OnDBChanged<T>()
        {
            OnDBChange?.Invoke(hotelReservationsContext, typeof(T));
        }
    }
}


