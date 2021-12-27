using System.Collections.Generic;
using GameManagerData.data;
using UnityEngine;

namespace GameManagerData
{
    public class HomeLoadData
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
                return;
            }
            ControllersRooms.Add(data);
        }
    }
}