using HotelReservationsApp.DBModels;
using HotelReservationsApp.Misc;
using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    internal class ReservationCreator : AbstractCreator<Reservations>
    {
        private Integer selectedRoomKey = null;
        private Integer selectedCustomerKey = null;
        private TextBox ResStartDateInput;
        private TextBox ResEndDateInput;
        private TextBox ResVisitorsCountInput;
        private Action<string> Log;
        private Integer selectedReservationKey;

        public ReservationCreator(Integer selectedRoomKey, Integer selectedCustomerKey, TextBox resStartDateInput, TextBox resEndDateInput, TextBox resVisitorsCountInput, Action<string> log, Integer selectedReservationKey)
        {
            this.selectedRoomKey = selectedRoomKey;
            this.selectedCustomerKey = selectedCustomerKey;
            ResStartDateInput = resStartDateInput;
            ResEndDateInput = resEndDateInput;
            ResVisitorsCountInput = resVisitorsCountInput;
            Log = log;
            this.selectedReservationKey = selectedReservationKey;

            this.textBoxesToBlock = new List<TextBox>() {
                ResStartDateInput,
                ResEndDateInput,
                ResVisitorsCountInput
            };
        }

        public override Result AddNewEntity(DataConnection dataConnection)
        {
            int visitorsCount;
            DateTime startDate;
            DateTime endDate;
            {
                try
                {
                    visitorsCount = int.Parse(ResVisitorsCountInput.Text);
                }
                catch (Exception)
                {
                    Log?.Invoke("Fetch Failed - Visitors count is in incorrect format");
                    UnblockInputTextBoxes();
                    return new Result(ResultType.WRONG_PARAMETER, "Visitors count");
                }
                try
                {
                    startDate = DateTime.Parse(ResStartDateInput.Text);
                }
                catch (Exception)
                {
                    Log?.Invoke("Fetch Failed - Start date is in incorrect format");
                    UnblockInputTextBoxes();
                    return new Result(ResultType.WRONG_PARAMETER, "Start date");
                }
                try
                {
                    endDate = DateTime.Parse(ResEndDateInput.Text);
                }
                catch (Exception)
                {
                    Log?.Invoke("Fetch Failed - End date is in incorrect format");
                    UnblockInputTextBoxes();
                    return new Result(ResultType.WRONG_PARAMETER, "End date");
                }
            } // try catch parse all variables

            Reservations reservation = new Reservations() { RoomId = selectedRoomKey, CustomerId = selectedCustomerKey, VisitorsCount = visitorsCount, StartDate = startDate, EndDate = endDate };
            Result result = dataConnection.InsertEntity(reservation);
            selectedReservationKey.Value = reservation.Id;
            return result;
        }

        public override void InsertFields(Reservations entity)
        {
            throw new NotImplementedException();
        }
    }
}