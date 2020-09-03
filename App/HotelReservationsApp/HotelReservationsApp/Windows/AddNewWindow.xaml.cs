using HotelReservationsApp.DBModels;
using HotelReservationsApp.Misc;
using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationsApp.Windows
{
    /// <summary>
    /// Interaction logic for AddNew.xaml
    /// </summary>
    public partial class AddNewWindow : Window
    {
        private readonly DataConnection dataConnection;
        private readonly RoomCreator roomCreator;
        private Integer selectedRoomKey = new Integer(-1);
        private readonly CustomerCreator customerCreator;
        private Integer selectedCustomerKey = new Integer(-1);
        private readonly ReservationCreator reservationCreator;
        private Integer selectedReservationKey = new Integer(-1);


        public AddNewWindow(DataConnection dataConnection)
        {
            InitializeComponent();
            this.dataConnection = dataConnection;

            roomCreator = new RoomCreator(RoomNumberInput, RoomCapacityInput, RoomFloorNumberInput, RoomPriceInput, RoomSizeInput, RoomTypeInput
                , log => UpdateLogLabel(log), selectedRoomKey);

            customerCreator = new CustomerCreator(CustNameInput, CustSurnameInput, CustEmailInput, CustPhoneInput, log => UpdateLogLabel(log), selectedCustomerKey);

            reservationCreator = new ReservationCreator(selectedRoomKey, selectedCustomerKey, ResStartDateInput, ResEndDateInput, ResVisitorsCountInput
                , log => UpdateLogLabel(log), selectedReservationKey);
        }

        public AddNewWindow(DataConnection dataConnection, Reservations reservation) : this(dataConnection)
        {
            roomCreator.InsertFields(reservation.Room);
            roomCreator.BlockInputTextBoxes();
            customerCreator.InsertFields(reservation.Customer);
            customerCreator.BlockInputTextBoxes();
            reservationCreator.InsertFields(reservation);
        }

        private void FetchRoomButton_Click(object sender, RoutedEventArgs e)
        {
            roomCreator.FetchRoom(dataConnection);
        }

        private void FetchCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            customerCreator.FetchCustomer(dataConnection);
        }

 

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewEntity(roomCreator);
        }


        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewEntity(customerCreator);
        }

        private void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewEntity(reservationCreator);
        }

        private void EditReservationButton_Click(object sender, RoutedEventArgs e)
        {
            reservationCreator.EditEntity(dataConnection);
        }

        private void AddNewEntity<T>(AbstractCreator<T> creator) where T : class
        {
            //try
            //{
                Result result = creator.AddNewEntity(dataConnection);
                UpdateLogLabel(result.type.ToFriendlyString() + (result.details != string.Empty ? " : " + result.details : ""));
            //}
            //catch (Exception exe)
            //{
            //    UpdateLogLabel(exe.Message);
            //    throw exe;
            //}
        }

        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private async void UpdateLogLabel(string text)
        {
            LogLabel.Content = text;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await Task.Delay(5_000, _cancellationTokenSource.Token);
                LogLabel.Content = string.Empty;
            }
            catch (TaskCanceledException) { }
        }

    }
}