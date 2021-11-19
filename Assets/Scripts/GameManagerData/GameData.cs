using System;
using System.Collections.Generic;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData
{
    [Serializable]
    public class GameData : MonoBehaviour
    {
        public static List<Room> Rooms = new List<Room>();
        public static List<Furniture> Furniture = new List<Furniture>();
        public static List<Doll> Dolls = new List<Doll>();
        public static List<HomeControllerObject> HomeControllers = new List<HomeControllerObject>();

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }
    
        public void RemoveRoom(Room room)
        {
            Rooms.Remove(room);
        }

        public void AddFurniture(Furniture furniture)
        {
            Furniture.Add(furniture);
        }
    
        public void RemoveFurniture(Furniture furniture)
        {
            Furniture.Remove(furniture);
        }
    
        public void AddDoll(Doll doll)
        {
            Dolls.Add(doll);
        }
    
        public void RemoveDoll(Doll doll)
        {
            Dolls.Remove(doll);
        }
        
        public void AddHomeController(HomeControllerObject obj)
        {
            HomeControllers.Add(obj);
        }
    
        public void RemoveHomeController(HomeControllerObject obj)
        {
            HomeControllers.Remove(obj);
        }
    }
}