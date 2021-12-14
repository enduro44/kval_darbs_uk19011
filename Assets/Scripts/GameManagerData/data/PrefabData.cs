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
        public const string BED = "Bed(Clone)";
        public GameObject bedPrefab;
        public const string CABINET = "Cabinet(Clone)";
        public GameObject cabinetPrefab;
        public const string CARPET = "Carpet(Clone)";
        public GameObject carpetPrefab;
        public const string CHAIR = "Chair(Clone)";
        public GameObject chairPrefab;
        public const string CUSHION = "Cushion(Clone)";
        public GameObject cushionPrefab;
        public const string EGG_STOOL = "EggStool(Clone)";
        public GameObject eggStoolPrefab;
        public const string EGG_TABLE = "EggTable(Clone)";
        public GameObject eggTablePrefab;
        public const string FRIDGE = "Fridge(Clone)";
        public GameObject fridgePrefab;
        public const string OVEN = "Oven(Clone)";
        public GameObject ovenPrefab;
        public const string SIDE_TABLE = "SideTable(Clone)";
        public GameObject sideTablePrefab;
        public const string SOFA = "Sofa(Clone)";
        public GameObject sofaPrefab;
        public const string TABLE = "Table(Clone)";
        public GameObject tablePrefab;
        public const string TV = "TV(Clone)";
        public GameObject tvPrefab;
        public const string VASE = "Vase(Clone)";
        public GameObject vasePrefab;

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

        public GameObject GetFurniturePrefab(string type)
        {
            switch (type)
            {
                case BED:
                    return bedPrefab;
                case CABINET:
                    return cabinetPrefab;
                case CARPET:
                    return carpetPrefab;
                case CHAIR:
                    return chairPrefab;
                case CUSHION:
                    return cushionPrefab;
                case EGG_STOOL:
                    return eggStoolPrefab;
                case EGG_TABLE:
                    return eggTablePrefab;
                case FRIDGE:
                    return fridgePrefab;
                case OVEN:
                    return ovenPrefab;
                case SIDE_TABLE:
                    return sideTablePrefab;
                case SOFA:
                    return sofaPrefab;
                case TABLE:
                    return tablePrefab;
                case TV:
                    return tvPrefab;
                case VASE:
                    return vasePrefab;
                default:
                    Debug.Log("Type doesn't match, default furniture provided");
                    return bedPrefab;
            }
        }

        public GameObject GetPlayablePrefab(string type)
        {
            switch (type)
            {
                default:
                    Debug.Log("Type doesn't match, default furniture provided");
                    return bedPrefab;
            }
        }
    }
}