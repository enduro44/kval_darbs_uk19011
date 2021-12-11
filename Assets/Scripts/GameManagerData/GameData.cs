using System;
using System.Collections.Generic;
using System.ComponentModel;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData
{
    [Serializable]
    public class GameData : MonoBehaviour
    {
        public static List<Room> Rooms = new List<Room>();
        public static List<Furniture> Furniture = new List<Furniture>();
        public static List<Playable> Playables = new List<Playable>();
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
    
        public void AddDoll(Playable playable)
        {
            Playables.Add(playable);
        }
    
        public void RemoveDoll(Playable playable)
        {
            Playables.Remove(playable);
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