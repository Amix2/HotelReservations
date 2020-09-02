

using Microsoft.EntityFrameworkCore;

namespace HotelReservationsApp.Model.Validator
{
    public interface IEntityValidator<T> where T : class
    {
        bool Validate(T entity, DataConnection dataConnection, out Result result);
    }
}
