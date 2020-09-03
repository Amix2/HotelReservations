using HotelReservationsApp.DBModels;
using HotelReservationsApp.Model.Validator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DBContext = HotelReservationsApp.DBModels.HotelReservationsContext;

namespace HotelReservationsApp.Model
{
    public class DataConnection
    {
        public Action<HotelReservationsContext, Type> OnDBChange;

        private readonly Dictionary<Type, object> insertValidators;

        public Dictionary<Type, object> InsertValidators => insertValidators;

        public DataConnection()
        {
            insertValidators = new Dictionary<Type, object>();
        }

        public Result InsertEntity<T>(T item, bool ignoreValidation = false) where T : class
        {
            using (DBContext context = new HotelReservationsContext())
            {
                if (ContainsEntity(item, context))
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
                context.Add(item);
                context.SaveChanges();
                OnDBChange?.Invoke(context, typeof(T));
            }
            return new Result(ResultType.SUCCESS);
        }

        internal DbSet<T> GetTable<T>() where T : class
        {
            using (DBContext context = new HotelReservationsContext())
            {
                return context.Set<T>();
            }
        }

        private Result Validate<T>(T entity) where T : class
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

        public void RemoveEntity<T>(T entity)
        {
            using (DBContext context = new HotelReservationsContext())
            {
                context.Remove(entity);
                context.SaveChanges();
                OnDBChange?.Invoke(context, typeof(T));
            }
        }

        public Result EditEntity<T>(T entity) where T : class
        {
            using (DBContext context = new HotelReservationsContext())
            {
                if (!ContainsEntity(entity, context))
                {
                    return new Result(ResultType.NOT_IN_DATABASE);
                }

                Result result = Validate(entity);
                if (result.type != ResultType.SUCCESS) return result;
                context.Update(entity);
                context.SaveChanges();
                OnDBChange?.Invoke(context, typeof(T));
            }

            return new Result(ResultType.SUCCESS);
        }

        public int GetTableCount<T>() where T : class
        {
            using (DBContext context = new HotelReservationsContext())
            {
                var table = context.Set<T>();
                if (table == null)
                    return -1;
                return table.Count();
            }
        }

        public IQueryable<T> GetEntitiesWithFilter<T>(Expression<Func<T, bool>> filter) where T : class
        {
            using (DBContext context = new HotelReservationsContext())
            {
                var table = context.Set<T>();
                return table.Where<T>(filter);
            }
        }

        public bool ContainsEntity<T>(T entity, DBContext context) where T : class
        {
            return context.Set<T>().Any(x => x.Equals(entity));
        }

        public void AddInsertValidator<T>(IEntityValidator<T> validator) where T : class
        {
            if (insertValidators.ContainsKey(typeof(T))) throw new Exception("Insert Validator for given type already exists");
            insertValidators.Add(typeof(T), validator);
        }
    }
}