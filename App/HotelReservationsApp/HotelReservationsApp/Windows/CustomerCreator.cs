﻿using HotelReservationsApp.DBModels;
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

        public CustomerCreator(TextBox custNameInput, TextBox custSurnameInput, TextBox custEmailInput, TextBox custPhoneInput, Action<string> log)
        {
            CustNameInput = custNameInput;
            CustSurnameInput = custSurnameInput;
            CustEmailInput = custEmailInput;
            CustPhoneInput = custPhoneInput;
            Log = log;

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
            base.FetchEntity(dataConnection, customer => customer.Name == name && customer.Surname == surname, FailLog, SuccessLog, Log);

        }

        public override void InsertFields(Customers customer)
        {
            CustNameInput.Text = customer.Name;
            CustSurnameInput.Text = customer.Surname;
            CustEmailInput.Text = customer.Email;
            CustPhoneInput.Text = customer.Phone;
        }

        public override Result AddNewEntity(DataConnection dataConnection)
        {
            Customers customer = new Customers() { Name = CustNameInput.Text, Surname = CustSurnameInput.Text, Phone = CustPhoneInput.Text, Email = CustEmailInput.Text };
            customer.Reservations.Clear();
            Result result = dataConnection.InsertEntity(customer);
            return result;
        }
    }
}
