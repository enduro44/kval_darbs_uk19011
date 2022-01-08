using System;
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
        
        [Header("Roofs")]
        public const string LARGE_ROOF = "LargeRoof(Clone)";
        [SerializeField] private GameObject largeRoofPrefab;
        public const string SMALL_ROOF = "SmallRoof(Clone)";
        [SerializeField] private GameObject smallRoofPrefab;
        public const string CORNER_ROOF = "CornerRoof(Clone)";
        [SerializeField] private GameObject cornerRoofPrefab;
        
        [Header("Furniture")]
        public const string BED = "Bed(Clone)";
        public GameObject bedPrefab;
        public const string BED_BLUE = "BedBlue(Clone)";
        public GameObject bedBluePrefab;
        public const string BED_BROWN = "BedBrown(Clone)";
        public GameObject bedBrownPrefab;
        public const string BED_GREEN = "BedGreen(Clone)";
        public GameObject bedGreenPrefab;
        public const string BED_PINK = "BedPink(Clone)";
        public GameObject bedPinkPrefab;
        public const string BED_WHITE = "BedWhite(Clone)";
        public GameObject bedWhitePrefab;
        
        public const string BIN_GREEN = "BinGreen(Clone)";
        public GameObject binGreenPrefab;
        public const string BIN_WHITE = "BinWhite(Clone)";
        public GameObject binWhitePrefab;
        public const string BIN_YELLOW = "BinYellow(Clone)";
        public GameObject binYellowPrefab;
        
        //needs to be reworked
        public const string CABINET = "Cabinet(Clone)";
        public GameObject cabinetPrefab;
        
        public const string CARPET = "Carpet(Clone)";
        public GameObject carpetPrefab;
        public const string CARPET_BEIGE = "CarpetBeige(Clone)";
        public GameObject carpetBeigePrefab;
        public const string CARPET_BROWN = "CarpetBrown(Clone)";
        public GameObject carpetBrownPrefab;
        public const string CARPET_DARK = "CarpetDark(Clone)";
        public GameObject carpetDarkPrefab;
        public const string CARPET_PINK = "CarpetPink(Clone)";
        public GameObject carpetPinkPrefab;
        
        public const string CHAIR = "Chair(Clone)";
        public GameObject chairPrefab;
        public const string CHAIR_DARK = "ChairDark(Clone)";
        public GameObject chairDarkPrefab;
        public const string CHAIR_GREEN = "ChairGreen(Clone)";
        public GameObject chairGreenPrefab;
        public const string CHAIR_LIGHT = "ChairLight(Clone)";
        public GameObject chairLightPrefab;
        public const string CHAIR_WHITE = "ChairWhite(Clone)";
        public GameObject chairWhitePrefab;
        
        public const string CHAIR_SMALL = "ChairSmall(Clone)";
        public GameObject chairSmallPrefab;
        public const string CHAIR_SMALL_DARK = "ChairSmallDark(Clone)";
        public GameObject chairSmallDarkPrefab;
        public const string CHAIR_SMALL_GREEN = "ChairSmallGreen(Clone)";
        public GameObject chairSmallGreenPrefab;
        public const string CHAIR_SMALL_LIGHT = "ChairSmallLight(Clone)";
        public GameObject chairSmallLightPrefab;
        public const string CHAIR_SMALL_RED = "ChairSmallRed(Clone)";
        public GameObject chairSmallRedPrefab;
        public const string CHAIR_SMALL_WHITE = "ChairSmallWhite(Clone)";
        public GameObject chairSmallWhitePrefab;
        
        public const string COUCH = "Couch(Clone)";
        public GameObject couchPrefab;
        public const string COUCH_BEIGE = "CouchBeige(Clone)";
        public GameObject couchBeigePrefab;
        public const string COUCH_GREEN = "CouchGreen(Clone)";
        public GameObject couchGreenPrefab;
        
        public const string CUSHION = "Cushion(Clone)";
        public GameObject cushionPrefab;
        
        public const string EGG_STOOL = "EggStool(Clone)";
        public GameObject eggStoolPrefab;
        public const string EGG_STOOL_DARK = "EggStoolDarkWood(Clone)";
        public GameObject eggStoolDarkPrefab;
        public const string EGG_STOOL_LIGHT = "EggStoolLightWood(Clone)"; 
        public GameObject eggStoolLightPrefab;
        
        public const string EGG_TABLE = "EggTable(Clone)";
        public GameObject eggTablePrefab;
        public const string EGG_TABLE_DARK = "EggTableDarkWood(Clone)";
        public GameObject eggTableDarkPrefab;
        public const string EGG_TABLE_LIGHT = "EggTableLightWood(Clone)";
        public GameObject eggTableLightPrefab;
        
        public const string MICROWAVE = "Microwave(Clone)";
        public GameObject microwavePrefab;
        public const string FRIDGE = "Fridge(Clone)";
        public GameObject fridgePrefab;
        public const string OVEN = "Oven(Clone)";
        public GameObject ovenPrefab;
        public const string OVEN_2 = "Oven2(Clone)";
        public GameObject oven2Prefab;
        
        public const string SIDE_TABLE = "SideTable(Clone)";
        public GameObject sideTablePrefab;
        public const string SIDE_TABLE_GREEN = "SideTableGreen(Clone)";
        public GameObject sideTableGreenPrefab;
        
        
        public const string SINK = "Sink(Clone)";
        public GameObject sinkPrefab;
        
        public const string SMALL_TABLE = "SmallTable(Clone)";
        public GameObject smallTablePrefab;
        public const string SMALL_TABLE_GREEN = "SmallTableGreen(Clone)";
        public GameObject smallTableGreenPrefab;
        public const string SMALL_TABLE_RED = "SmallTableRed(Clone)";
        public GameObject smallTableRedPrefab;
        public const string SMALL_TABLE_WHITE = "SmallTableWhite(Clone)";
        public GameObject smallTableWhitePrefab;
        
        public const string SOFA = "Sofa(Clone)";
        public GameObject sofaPrefab;
        public const string SOFA_BEIGE = "SofaBeige(Clone)";
        public GameObject sofaBeigePrefab;
        public const string SOFA_GREEN = "SofaGreen(Clone)";
        public GameObject sofaGreenPrefab;
        
        public const string STAND_1 = "Stand1(Clone)";
        public GameObject stand1Prefab;
        public const string STAND_2 = "Stand2(Clone)";
        public GameObject stand2Prefab;
        public const string STAND_3 = "Stand3(Clone)";
        public GameObject stand3Prefab;
        
        public const string TABLE = "Table(Clone)";
        public GameObject tablePrefab;
        public const string TABLE_GREEN = "TableGreen(Clone)";
        public GameObject tableGreenPrefab;
        public const string TABLE_RED = "TableRed(Clone)";
        public GameObject tableRedPrefab;
        public const string TABLE_WHITE = "TableWhite(Clone)";
        public GameObject tableWhitePrefab;
        
        public const string TV = "TV(Clone)";
        public GameObject tvPrefab;
        
        public const string VASE = "Vase(Clone)";
        public GameObject vasePrefab;
        public const string VASE_PINK = "VasePink(Clone)";
        public GameObject vasePinkPrefab;
        
        [Header("Playables")]
        public const string ANIMAL_CAT = "AnimalCat(Clone)";
        public GameObject animalCatPrefab;
        
        public const string CAR_BLUE = "CarBlue(Clone)";
        public GameObject carBluePrefab;
        public const string CAR_HOTDOG_TRACK = "CarHotdogTrack(Clone)";
        public GameObject carHotdogTrackPrefab;
        
        
        public const string DOLL_BARBIE = "DollBarbie(Clone)";
        public GameObject dollBarbiePrefab;
        public const string DOLL_BOY = "DollBoy(Clone)";
        public GameObject dollBoyPrefab;
        public const string DOLL_FEMALE = "DollFemale(Clone)";
        public GameObject dollFemalePrefab;
        public const string DOLL_MALE = "DollMale(Clone)";
        public GameObject dollMalePrefab;
        public const string DOLL_POLICE = "DollPolice(Clone)";
        public GameObject dollPolicePrefab;
        
        public const string MONSTER_BAT = "MonsterBat(Clone)";
        public GameObject monsterBatPrefab;


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
                case LARGE_ROOF:
                    return largeRoofPrefab;
                case SMALL_ROOF:
                    return smallRoofPrefab;
                case CORNER_ROOF:
                    return cornerRoofPrefab;
                case CONTROLLER:
                    return homeControllerPrefab;
                default:
                    Debug.Log("Type doesn't match, default room provided: "+ type);
                    return largeRoomPrefab;
            }
        }

        public GameObject GetFurniturePrefab(string type)
        {
            switch (type)
            {
                case BED:
                    return bedPrefab;
                case BED_BLUE:
                    return bedBluePrefab;
                case BED_BROWN:
                    return bedBrownPrefab;
                case BED_GREEN:
                    return bedGreenPrefab;
                case BED_PINK:
                    return bedPinkPrefab;
                case BED_WHITE:
                    return bedWhitePrefab;
                case BIN_GREEN:
                    return binGreenPrefab;
                case BIN_WHITE:
                    return binWhitePrefab;
                case BIN_YELLOW:
                    return binYellowPrefab;
                case CABINET:
                    return cabinetPrefab;
                case CARPET:
                    return carpetPrefab;
                case CARPET_BEIGE:
                    return carpetBeigePrefab;
                case CARPET_BROWN:
                    return carpetBrownPrefab;
                case CARPET_DARK:
                    return carpetDarkPrefab;
                case CARPET_PINK:
                    return carpetPinkPrefab;
                case CHAIR:
                    return chairPrefab;
                case CHAIR_DARK:
                    return chairDarkPrefab;
                case CHAIR_GREEN:
                    return chairGreenPrefab;
                case CHAIR_LIGHT:
                    return chairLightPrefab;
                case CHAIR_WHITE:
                    return chairWhitePrefab;
                case CHAIR_SMALL:
                    return chairSmallPrefab;
                case CHAIR_SMALL_DARK:
                    return chairSmallDarkPrefab;
                case CHAIR_SMALL_GREEN:
                    return chairSmallGreenPrefab;
                case CHAIR_SMALL_LIGHT:
                    return chairSmallLightPrefab;
                case CHAIR_SMALL_RED:
                    return chairSmallRedPrefab;
                case CHAIR_SMALL_WHITE:
                    return chairSmallWhitePrefab;
                case COUCH:
                    return couchPrefab;
                case COUCH_BEIGE:
                    return couchBeigePrefab;
                case COUCH_GREEN:
                    return couchGreenPrefab;
                case CUSHION:
                    return cushionPrefab;
                case EGG_STOOL:
                    return eggStoolPrefab;
                case EGG_STOOL_DARK:
                    return eggStoolDarkPrefab;
                case EGG_STOOL_LIGHT:
                    return eggStoolLightPrefab;
                case EGG_TABLE:
                    return eggTablePrefab;
                case EGG_TABLE_DARK:
                    return eggTableDarkPrefab;
                case EGG_TABLE_LIGHT:
                    return eggTableLightPrefab;
                case FRIDGE:
                    return fridgePrefab;
                case MICROWAVE:
                    return microwavePrefab;
                case OVEN:
                    return ovenPrefab;
                case OVEN_2:
                    return oven2Prefab;
                case SIDE_TABLE:
                    return sideTablePrefab;
                case SIDE_TABLE_GREEN:
                    return sideTableGreenPrefab;
                case SINK:
                    return sinkPrefab;
                case SMALL_TABLE:
                    return smallTablePrefab;
                case SMALL_TABLE_GREEN:
                    return smallTableGreenPrefab;
                case SMALL_TABLE_RED:
                    return smallTableRedPrefab;
                case SMALL_TABLE_WHITE:
                    return smallTableWhitePrefab;
                case SOFA:
                    return sofaPrefab;
                case SOFA_BEIGE:
                    return sofaBeigePrefab;
                case SOFA_GREEN:
                    return sofaGreenPrefab;
                case STAND_1:
                    return stand1Prefab;
                case STAND_2:
                    return stand2Prefab;
                case STAND_3:
                    return stand3Prefab;
                case TABLE:
                    return tablePrefab;
                case TABLE_GREEN:
                    return tableGreenPrefab;
                case TABLE_RED:
                    return tableRedPrefab;
                case TABLE_WHITE:
                    return tableWhitePrefab;
                case TV:
                    return tvPrefab;
                case VASE:
                    return vasePrefab;
                case VASE_PINK:
                    return vasePinkPrefab;
                default:
                    Debug.Log("Type doesn't match, default furniture provided: " + type);
                    return bedPrefab;
            }
        }

        public GameObject GetPlayablePrefab(string type)
        {
            switch (type)
            {
                case ANIMAL_CAT:
                    return animalCatPrefab;
                case CAR_BLUE:
                    return carBluePrefab;
                case CAR_HOTDOG_TRACK:
                    return carHotdogTrackPrefab;
                case DOLL_BARBIE:
                    return dollBarbiePrefab;
                case DOLL_BOY:
                    return dollBoyPrefab;
                case DOLL_FEMALE:
                    return dollFemalePrefab;
                case DOLL_MALE:
                    return dollMalePrefab;
                case DOLL_POLICE:
                    return dollPolicePrefab;
                case MONSTER_BAT:
                    return monsterBatPrefab;
                default:
                    Debug.Log("Type doesn't match, default playable provided: " + type);
                    return animalCatPrefab;
            }
        }

        public static Vector3 GetSizeVector3(string type)
        {
            switch (type)
            {
                case LARGE_ROOM:
                    return new Vector3(0.1f,0.1f,0.1f);
                case SMALL_ROOM:
                    return new Vector3(0.1f,0.1f,0.1f);
                case CORNER_ROOM:
                    return new Vector3(0.1f,0.1f,0.1f);
                case LARGE_ROOF:
                    return new Vector3(0.1f,0.1f,0.1f);
                case SMALL_ROOF:
                    return new Vector3(0.1f,0.1f,0.1f);
                case CORNER_ROOF:
                    return new Vector3(0.1f,0.1f,0.1f);
                case CONTROLLER:
                    return new Vector3(0.1f,0.1f,0.1f);
                case BED:
                    return new Vector3(0.05f,0.05f,0.05f);
                case BED_BLUE:
                    return new Vector3(0.05f,0.05f,0.05f);
                case BED_BROWN:
                    return new Vector3(0.05f,0.05f,0.05f);
                case BED_GREEN:
                    return new Vector3(0.05f,0.05f,0.05f);
                case BED_PINK:
                    return new Vector3(0.05f,0.05f,0.05f);
                case BED_WHITE:
                    return new Vector3(0.05f,0.05f,0.05f);
                case BIN_GREEN:
                    return new Vector3(0.6f,0.6f,0.4f);
                case BIN_WHITE:
                    return new Vector3(0.6f,0.6f,0.4f);
                case BIN_YELLOW:
                    return new Vector3(0.6f,0.6f,0.4f);
                case CARPET:
                    return new Vector3(1,1,1);
                case CARPET_BEIGE:
                    return new Vector3(1,1,1);
                case CARPET_BROWN:
                    return new Vector3(1,1,1);
                case CARPET_DARK:
                    return new Vector3(1,1,1);
                case CARPET_PINK:
                    return new Vector3(1,1,1);
                case CHAIR:
                    return new Vector3(0.04666358f,0.1399907f,0.06999537f);
                case CHAIR_DARK:
                    return new Vector3(0.04666358f,0.1399907f,0.06999537f);
                case CHAIR_GREEN:
                    return new Vector3(0.04666358f,0.1399907f,0.06999537f);
                case CHAIR_LIGHT:
                    return new Vector3(0.04666358f,0.1399907f,0.06999537f);
                case CHAIR_WHITE:
                    return new Vector3(0.04666358f,0.1399907f,0.06999537f);
                case CHAIR_SMALL:
                    return new Vector3(0.008012706f,0.004807624f,0.008012706f);
                case CHAIR_SMALL_DARK:
                    return new Vector3(0.008012706f,0.004807624f,0.008012706f);
                case CHAIR_SMALL_GREEN:
                    return new Vector3(0.008012706f,0.004807624f,0.008012706f);
                case CHAIR_SMALL_LIGHT:
                    return new Vector3(0.008012706f,0.004807624f,0.008012706f);
                case CHAIR_SMALL_RED:
                    return new Vector3(0.008012706f,0.004807624f,0.008012706f);
                case CHAIR_SMALL_WHITE:
                    return new Vector3(0.008012706f,0.004807624f,0.008012706f);
                case COUCH:
                    return new Vector3(0.151962f,0.07641409f,0.07598101f);
                case COUCH_BEIGE:
                    return new Vector3(0.151962f,0.07641409f,0.07598101f);
                case COUCH_GREEN:
                    return new Vector3(0.151962f,0.07641409f,0.07598101f);
                case CUSHION:
                    return new Vector3(0.5f,0.5f,0.5f);
                case EGG_STOOL:
                    return new Vector3(0.248465f,0.248465f,0.248465f);
                case EGG_STOOL_DARK:
                    return new Vector3(0.248465f,0.248465f,0.248465f);
                case EGG_STOOL_LIGHT:
                    return new Vector3(0.248465f,0.248465f,0.248465f);
                case EGG_TABLE:
                    return new Vector3(0.139776f,0.139776f,0.139776f);
                case EGG_TABLE_DARK:
                    return new Vector3(0.139776f,0.139776f,0.139776f);
                case EGG_TABLE_LIGHT:
                    return new Vector3(0.139776f,0.139776f,0.139776f);
                case FRIDGE:
                    return new Vector3(0.0050625f,0.0050625f,0.0050625f);
                case MICROWAVE:
                    return new Vector3(0.02f,0.02f,0.02f);
                case OVEN:
                    return new Vector3(0.008418f,0.0101016f,0.008418f);
                case OVEN_2:
                    return new Vector3(0.008418f,0.0101016f,0.008418f);
                case SIDE_TABLE:
                    return new Vector3(0.1148225f,0.1607515f,0.1148225f);
                case SIDE_TABLE_GREEN:
                    return new Vector3(0.1148225f,0.1607515f,0.1148225f);
                case SINK:
                    return new Vector3(0.008418f,0.0101016f,0.008418f);
                case SMALL_TABLE:
                    return new Vector3(0.005185424f,0.003889068f,0.005185424f);
                case SMALL_TABLE_GREEN:
                    return new Vector3(0.005185424f,0.003889068f,0.005185424f);
                case SMALL_TABLE_RED:
                    return new Vector3(0.005185424f,0.003889068f,0.005185424f);
                case SMALL_TABLE_WHITE:
                    return new Vector3(0.005185424f,0.003889068f,0.005185424f);
                case SOFA:
                    return new Vector3(0.07599001f,0.07599001f,0.07599001f);
                case SOFA_BEIGE:
                    return new Vector3(0.07599001f,0.07599001f,0.07599001f);
                case SOFA_GREEN:
                    return new Vector3(0.07599001f,0.07599001f,0.07599001f);
                case STAND_1:
                    return new Vector3(0.008418f,0.0101016f,0.008418f);
                case STAND_2:
                    return new Vector3(0.008418f,0.0101016f,0.008418f);
                case STAND_3:
                    return new Vector3(0.008418f,0.0101016f,0.008418f);
                case TABLE:
                    return new Vector3(0.01402f,0.0052575f,0.0052575f);
                case TABLE_GREEN:
                    return new Vector3(0.01402f,0.0052575f,0.0052575f);
                case TABLE_RED:
                    return new Vector3(0.01402f,0.0052575f,0.0052575f);
                case TABLE_WHITE:
                    return new Vector3(0.01402f,0.0052575f,0.0052575f);
                case TV:
                    return new Vector3(0.092015f,0.092015f,0.092015f);
                case VASE:
                    return new Vector3(0.105418f,0.105418f,0.105418f);
                case VASE_PINK:
                    return new Vector3(0.105418f,0.105418f,0.105418f);
                case ANIMAL_CAT:
                    return new Vector3(1,1,1);
                case CAR_BLUE:
                    return new Vector3(0.47822f,0.47822f,0.47822f);
                case CAR_HOTDOG_TRACK:
                    return new Vector3(0.1053681f,0.1053681f,0.1053681f);
                case DOLL_BARBIE:
                    return new Vector3(0.03571492f,0.04879732f,0.03571492f);
                case DOLL_BOY:
                    return new Vector3(0.09904563f,0.07464772f,0.04421187f);
                case DOLL_FEMALE:
                    return new Vector3(0.07396753f,0.09497089f,0.07396753f);
                case DOLL_MALE:
                    return new Vector3(0.1108268f,0.07557795f,0.1108268f);
                case DOLL_POLICE:
                    return new Vector3(0.1108268f,0.07557795f,0.1108268f);
                case MONSTER_BAT:
                    return new Vector3(0.113242f,0.113242f,0.113242f);
                default:
                    Debug.Log("Type doesn't match, default Vector3 provided: "+ type);
                    return new Vector3(1,1,1);
            }
        }
    }
}