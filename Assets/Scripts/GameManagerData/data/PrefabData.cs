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
        [SerializeField] private GameObject homeControllerPrefab;
        
        [Header("Rooms")]
        public const string LARGE_ROOM = "LargeRoom(Clone)";
        [SerializeField] private GameObject largeRoomPrefab;
        public const string SMALL_ROOM = "SmallRoom(Clone)";
        [SerializeField] private GameObject smallRoomPrefab;
        public const string CORNER_ROOM = "CornerRoom(Clone)";
        [SerializeField] private GameObject cornerRoomPrefab;
        
        [Header("Furniture")]
        public GameObject eggStoolPrefab;

        void Awake() {
            _instance = this;
        }
        
        public static PrefabData Instance() {
            return _instance;
        }
        
        public GameObject GetPrefab(string type)
        {
            switch (type)
            {
                case LARGE_ROOM:
                    return largeRoomPrefab;
                case SMALL_ROOM:
                    return smallRoomPrefab;
                case CORNER_ROOM:
                    return cornerRoomPrefab;
                case CONTROLLER:
                    return homeControllerPrefab;
                default:
                    Debug.Log("Type doesn't match, default room provided");
                    return largeRoomPrefab;
            }
        }
    }
}