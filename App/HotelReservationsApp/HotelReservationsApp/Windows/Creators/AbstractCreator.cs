using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    internal abstract class AbstractCreator<T> where T : class
    {
        protected IEnumerable<TextBox> textBoxesToBlock;

        public abstract void InsertFields(T entity);

        public abstract Result AddNewEntity(DataConnection dataConnection);

        protected T FetchEntity(DataConnection dataConnection, Expression<Func<T, bool>> filter, string failLog, string successLog, Action<string> Log)
        {
            T fetchedEntity = dataConnection.GetFirstEntytiWithFilter<T>(filter);
            if (fetchedEntity == null)
            {
                Log?.Invoke(failLog);
                UnblockInputTextBoxes();
                return null;
            }
            else
            {
                Log?.Invoke(successLog);
                InsertFields(fetchedEntity);
                BlockInputTextBoxes();
                return fetchedEntity;
            }
        }

        public void BlockInputTextBoxes()
        {
            foreach (var textBox in textBoxesToBlock)
            {
                textBox.IsEnabled = false;
            }
        }

        public void UnblockInputTextBoxes()
        {
            foreach (var textBox in textBoxesToBlock)
            {
                textBox.IsEnabled = true;
            }
        }
    }
}