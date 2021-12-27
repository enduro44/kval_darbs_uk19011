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
        public static List<Playable> Playables = new List<Playable>();
        public static List<HomeControllerObject> HomeControllers = new List<HomeControllerObject>();
    }
}