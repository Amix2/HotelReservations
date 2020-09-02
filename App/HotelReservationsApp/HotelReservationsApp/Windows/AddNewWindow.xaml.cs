using HotelReservationsApp.DBModels;
using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelReservationsApp.Windows
{
    /// <summary>
    /// Interaction logic for AddNew.xaml
    /// </summary>
    public partial class AddNewWindow : Window
    {

        private readonly DataConnection dataConnection;
        private readonly RoomCreator roomCreator;
        private readonly CustomerCreator customerCreator;

        public AddNewWindow(DataConnection dataConnection)
        {
            InitializeComponent();
            this.dataConnection = dataConnection;
            roomCreator = new RoomCreator(RoomNumberInput, RoomCapacityInput, RoomFloorNumberInput, RoomPriceInput, RoomSizeInput, RoomTypeInput
                , log => UpdateLogLabel(log));
            customerCreator = new CustomerCreator(CustNameInput, CustSurnameInput, CustEmailInput, CustPhoneInput, log => UpdateLogLabel(log));
        }

        private void FetchRoomButton_Click(object sender, RoutedEventArgs e)
        {
            roomCreator.FetchRoom(dataConnection);
        }

        private void FetchCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            customerCreator.FetchCustomer(dataConnection);
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

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Result result = roomCreator.AddNewEntity(dataConnection);
                UpdateLogLabel(result.type.ToFriendlyString());
            }
            catch (Exception exe)
            {
                UpdateLogLabel(exe.Message);
                //throw exe;
            }
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                Result result = customerCreator.AddNewEntity(dataConnection);
                UpdateLogLabel(result.type.ToFriendlyString());
            //}
            //catch (Exception exe)
            //{
            //    UpdateLogLabel(exe.Message);
            //    //throw exe;
            //}
        }

    }
}
