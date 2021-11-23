using System;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData.data
{
    [Serializable]
    public class PrefabData : MonoBehaviour
    {
        private static PrefabData _instance;
        [Header("Controller")]
        public const string CONTROLLER = "HomeController(Clone)";
        [SerializeField] private HomeControllerObject homeControllerPrefab;
        [Header("Rooms")]
        public const string LARGE_ROOM = "LargeRoom(Clone)";
        [SerializeField] private Room largeRoomPrefab;
        public const string SMALL_ROOM = "SmallRoom(Clone)";
        [SerializeField] private Room smallRoomPrefab;
        public const string CORNER_ROOM = "CornerRoom(Clone)";
        [SerializeField] private Room cornerRoomPrefab;
        [Header("Furniture")]
        public GameObject eggStoolPrefab;

        void Awake() {
            _instance = this;
        }
        
        public static PrefabData Instance() {
            return _instance;
        }
        
        public HomeControllerObject GetControllerPrefab()
        {
            return homeControllerPrefab;
        }
        public Room GetRoomPrefab(string type)
        {
            Debug.Log("Here");
            switch (type)
            {
                case LARGE_ROOM:
                    Debug.Log("Large");
                    return largeRoomPrefab;
                case SMALL_ROOM:
                    Debug.Log("Small");
                    return smallRoomPrefab;
                case CORNER_ROOM:
                    Debug.Log("Corner");
                    return cornerRoomPrefab;
                default:
                    Debug.Log("Type doesn't match, default room provided");
                    return largeRoomPrefab;
            }
        }
    }
}