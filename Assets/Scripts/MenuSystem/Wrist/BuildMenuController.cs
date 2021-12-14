using System;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Wrist
{
    public class BuildMenuController : MonoBehaviour
    {

        public const string TYPE = "home";
        public PrefabIcon[] AllBases;
        public PrefabIcon[] AllSmallRooms;
        public PrefabIcon[] AllLargeRooms;
        public PrefabIcon[] AllCornerRooms;
        public PrefabIcon[] AllRoofs;

        public ScrollViewController controller;

        public void PopulateBaseData()
        {
            controller.PopulateData(AllBases, TYPE);
        }
        public void PopulateSmallRoomData()
        {
            controller.PopulateData(AllSmallRooms, TYPE);
        }
        
        public void PopulateLargeRoomData()
        {
            controller.PopulateData(AllLargeRooms, TYPE);
        }

        public void PopulateCornerRoomData()
        {
            controller.PopulateData(AllCornerRooms, TYPE);
        }
        
        public void PopulateRoofData()
        {
            controller.PopulateData(AllRoofs, TYPE);
        }
    }
}