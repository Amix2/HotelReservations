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


            if(!ParseFields(out int visitorsCount, out DateTime startDate, out DateTime endDate, out Result result))
            {
                return result;
            }
            // try catch parse all variables
            int thisKey = selectedReservationKey != -1 ? selectedReservationKey.Value : 0;
            Reservations reservation = new Reservations() { Id = thisKey, RoomId = selectedRoomKey, CustomerId = selectedCustomerKey, VisitorsCount = visitorsCount, StartDate = startDate, EndDate = endDate };
            result = dataConnection.InsertEntity(reservation);
            selectedReservationKey.Value = reservation.Id;
            return result;
        }

       

        internal Result EditEntity(DataConnection dataConnection)
        {
            if (!ParseFields(out int visitorsCount, out DateTime startDate, out DateTime endDate, out Result result))
            {
                return result;
            }
            // try catch parse all variables
            int thisKey = selectedReservationKey != -1 ? selectedReservationKey.Value : 0;
            Reservations reservation = new Reservations() { Id = thisKey, RoomId = selectedRoomKey, CustomerId = selectedCustomerKey, VisitorsCount = visitorsCount, StartDate = startDate, EndDate = endDate };
            result = dataConnection.EditEntity(reservation);
            selectedReservationKey.Value = reservation.Id;
            return result;
        }

        private bool ParseFields(out int visitorsCount, out DateTime startDate, out DateTime endDate, out Result result)
        {
            visitorsCount = -1;
            startDate = default;
            endDate = default;
            try
            {
                visitorsCount = int.Parse(ResVisitorsCountInput.Text);
            }
            catch (Exception)
            {
                Log?.Invoke("Fetch Failed - Visitors count is in incorrect format");
                UnblockInputTextBoxes();
                result = new Result(ResultType.WRONG_PARAMETER, "Visitors count");
                return false;
            }
            try
            {
                startDate = DateTime.Parse(ResStartDateInput.Text);
            }
            catch (Exception)
            {
                Log?.Invoke("Fetch Failed - Start date is in incorrect format");
                UnblockInputTextBoxes();
                result = new Result(ResultType.WRONG_PARAMETER, "Start date");
                return false;
            }
            try
            {
                endDate = DateTime.Parse(ResEndDateInput.Text);
            }
            catch (Exception)
            {
                Log?.Invoke("Fetch Failed - End date is in incorrect format");
                UnblockInputTextBoxes();
                result = new Result(ResultType.WRONG_PARAMETER, "End date");
                return false;
            }
            result = new Result(ResultType.NONE);
            return true;
        }

        public override void InsertFields(Reservations entity)
        {
            ResStartDateInput.Text = entity.StartDate.ToString("d");
            ResEndDateInput.Text = entity.EndDate.ToString("d");
            ResVisitorsCountInput.Text = entity.VisitorsCount.ToString();
            selectedReservationKey.Value = entity.Id;
        }
    }
}