using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    abstract class AbstractCreator<T> where T : class
    {
        protected IEnumerable<TextBox> textBoxesToBlock;

        public abstract void InsertFields(T entity);
        public abstract Result AddNewEntity(DataConnection dataConnection);

        protected T FetchEntity(DataConnection dataConnection, Expression<Func<T, bool>> filter, string failLog, string successLog, Action<string> Log)
        {
            var fetchedEntity = dataConnection.GetEntitiesWithFilter<T>(filter);
            if (fetchedEntity.Count() == 0)
            {
                Log?.Invoke(failLog);
                UnblockInputTextBoxes();
                return null;
            }
            else
            {
                Log?.Invoke(successLog);
                T entity = fetchedEntity.First();
                InsertFields(entity);
                BlockInputTextBoxes();
                return entity;
            }
        }

        protected void BlockInputTextBoxes()
        {
            foreach(var textBox in textBoxesToBlock)
            {
                textBox.IsEnabled = false;
            }
        }

        protected void UnblockInputTextBoxes()
        {
            foreach (var textBox in textBoxesToBlock)
            {
                textBox.IsEnabled = true;
            }
        }
    }
}
