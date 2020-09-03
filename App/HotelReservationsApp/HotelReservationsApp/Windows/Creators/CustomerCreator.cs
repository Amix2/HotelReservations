using HotelReservationsApp.DBModels;
using HotelReservationsApp.Misc;
using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace HotelReservationsApp.Windows
{
    class CustomerCreator : AbstractCreator<Customers>
    {
        TextBox CustNameInput;
        TextBox CustSurnameInput;
        TextBox CustEmailInput;
        TextBox CustPhoneInput;
        private Action<string> Log;
        private Integer selectedCustomerKey;

        public CustomerCreator(TextBox custNameInput, TextBox custSurnameInput, TextBox custEmailInput, TextBox custPhoneInput, Action<string> log, Integer selectedCustomerKey)
        {
            CustNameInput = custNameInput;
            CustSurnameInput = custSurnameInput;
            CustEmailInput = custEmailInput;
            CustPhoneInput = custPhoneInput;
            Log = log;
            this.selectedCustomerKey = selectedCustomerKey;

            this.textBoxesToBlock = new List<TextBox>() {
                CustEmailInput,
                CustPhoneInput
            };
        }

        public void FetchCustomer(DataConnection dataConnection)
        {
            string name = CustNameInput.Text;
            string surname = CustSurnameInput.Text;

            const string FailLog = "Fetch Failed - Customer with provided name and surname couldnt be found";
            const string SuccessLog = "Fetch Successful";
            Customers fetchedCustomer = base.FetchEntity(dataConnection, customer => customer.Name == name && customer.Surname == surname, FailLog, SuccessLog, Log);
            if(fetchedCustomer != null) selectedCustomerKey.Value = fetchedCustomer.Id;
        }

        public override void InsertFields(Customers customer)
        {
            CustNameInput.Text = customer.Name;
            CustSurnameInput.Text = customer.Surname;
            CustEmailInput.Text = customer.Email;
            CustPhoneInput.Text = customer.Phone;
            selectedCustomerKey.Value = customer.Id;
        }

        public override Result AddNewEntity(DataConnection dataConnection)
        {
            int thisKey = selectedCustomerKey != -1 ? selectedCustomerKey.Value : 0;
            Customers customer = new Customers() { Id = thisKey, Name = CustNameInput.Text, Surname = CustSurnameInput.Text, Phone = CustPhoneInput.Text, Email = CustEmailInput.Text };
            Result result = dataConnection.InsertEntity(customer);
            selectedCustomerKey.Value = customer.Id;
            return result;
        }
    }
}
