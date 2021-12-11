using System;
using System.Collections.Generic;
using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData
{
    public class LoadData
    {
        public string ControllerID;
        public HomeControllerData HomeControllerData;
        public List<RoomData> ControllersRooms = new List<RoomData>();

        public void AddControllerData(HomeControllerData data)
        {
            ControllerID = data.controlledID;
            HomeControllerData = data;
        }

        public void AddRoomData(RoomData data)
        {
            if (ControllerID != data.controllerID)
            {
                Debug.Log("Trying to add room to incorrect controller!");
                return;
            }
            Debug.Log(data.controllerID);
            ControllersRooms.Add(data);
        }
    }
}