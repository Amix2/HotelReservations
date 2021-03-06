﻿using HotelReservationsApp.DBModels;
using HotelReservationsApp.Misc;
using HotelReservationsApp.Model;
using HotelReservationsApp.Model.Validator;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HotelReservationsApp.Windows
{
    internal class RoomCreator : AbstractCreator<Rooms>
    {
        private TextBox RoomNumberInput;
        private TextBox RoomCapacityInput;
        private TextBox RoomFloorNumberInput;
        private TextBox RoomPriceInput;
        private TextBox RoomSizeInput;
        private TextBox RoomTypeInput;
        private Action<string> Log;
        private Integer selectedRoomKey;

        public RoomCreator(TextBox roomNumberInput, TextBox roomCapacityInput, TextBox roomFloorNumberInput, TextBox roomPriceInput, TextBox roomSizeInput, TextBox roomTypeInput
            , Action<string> log, Integer selectedRoomKey)
        {
            RoomNumberInput = roomNumberInput;
            RoomCapacityInput = roomCapacityInput;
            RoomFloorNumberInput = roomFloorNumberInput;
            RoomPriceInput = roomPriceInput;
            RoomSizeInput = roomSizeInput;
            RoomTypeInput = roomTypeInput;
            Log = log;
            this.selectedRoomKey = selectedRoomKey;

            this.textBoxesToBlock = new List<TextBox>() {
                RoomCapacityInput,
                RoomFloorNumberInput,
                RoomPriceInput,
                RoomSizeInput,
                RoomTypeInput
            };
        }

        public void FetchRoom(DataConnection dataConnection)
        {
            int inputRoomNumber;
            try
            {
                inputRoomNumber = int.Parse(RoomNumberInput.Text);
            }
            catch (Exception)
            {
                Log?.Invoke("Fetch Failed - Room with provided number couldnt be found");
                UnblockInputTextBoxes();
                return;
            }

            const string FailLog = "Fetch Failed - Room with provided number couldnt be found";
            const string SuccessLog = "Fetch Successful";
            Rooms fetchedRoom = base.FetchEntity(dataConnection, room => room.RoomNumber == inputRoomNumber, FailLog, SuccessLog, Log);
            if (fetchedRoom != null) selectedRoomKey.Value = fetchedRoom.RoomNumber;
        }

        public override void InsertFields(Rooms room)
        {
            RoomNumberInput.Text = room.RoomNumber.ToString();
            RoomCapacityInput.Text = room.Capacity.HasValue ? room.Capacity.Value.ToString() : "";
            RoomFloorNumberInput.Text = room.FloorNumber.HasValue ? room.FloorNumber.Value.ToString() : "";
            RoomPriceInput.Text = room.PriceForNight.HasValue ? room.PriceForNight.Value.ToString() : "";
            RoomSizeInput.Text = room.RoomSize.HasValue ? room.RoomSize.Value.ToString() : "";
            RoomTypeInput.Text = room.RoomType;
            selectedRoomKey.Value = room.RoomNumber;
        }

        public override Result AddNewEntity(DataConnection dataConnection)
        {
            int roomNumber = int.Parse(RoomNumberInput.Text);
            int floorNumber = int.Parse(RoomFloorNumberInput.Text);
            int size = int.Parse(RoomSizeInput.Text);
            string type = RoomTypeInput.Text;
            int price = int.Parse(RoomPriceInput.Text);
            int capacity = int.Parse(RoomCapacityInput.Text);
            Rooms room = new Rooms() { RoomNumber = roomNumber, Capacity = capacity, FloorNumber = floorNumber, PriceForNight = price, RoomSize = size, RoomType = type };
            Result result = dataConnection.InsertEntity(room);
            selectedRoomKey.Value = room.RoomNumber;
            return result;
        }
    }
}