﻿using HotelReservationsApp.DBModels;
using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using HotelReservationsApp.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelReservationsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataConnection dataConnection;

        private string RoomCustomerCountTextUpdate => string.Format("Rooms: {0}; Customers: {1}", dataConnection.GetTableCount<Rooms>(), dataConnection.GetTableCount<Customers>());

        public MainWindow()
        {
            InitializeComponent();
            dataConnection = new DataConnection();
            dataConnection.AddInsertValidator(new RoomInsertValidator());
            dataConnection.AddInsertValidator(new CustomerInsertVlidator());


            dataConnection.OnDBChange += (dbContext, type) =>
            {
                if(type == typeof(Customers) || type == typeof(Rooms))
                {
                    RoomCustomerCountText.Content = RoomCustomerCountTextUpdate;
                }
            };
            RoomCustomerCountText.Content = RoomCustomerCountTextUpdate;

        }


        private void MenuItem_Add(object sender, RoutedEventArgs e)
        {
            AddNewWindow addNewWindow = new AddNewWindow(dataConnection);
            addNewWindow.Show();
        }

        private void MenuItem_ImportCSV(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ReservationsListView_ItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("qqqqqqqqqqqqqqqqq");
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                //Do your stuff
            }
        }
    }
}