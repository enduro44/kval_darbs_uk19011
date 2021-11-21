using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

namespace GameManagerData
{
    public class GameManager : MonoBehaviour
    {
        public PrefabData prefabData;
        [Header("Prefabs")]
        [SerializeField] private LargeRoom largeRoomPrefab;
        [SerializeField] private HomeControllerObject homeControllerPrefab;

        [Header("Constants")] 
        private const string SAVE_FOLDER = "/SavedGames";
        private const string FOLDER = "/My First Game";
        
        private const string ROOMS_SUB = "/rooms";
        private const string ROOMS_COUNT_SUB = "/rooms.count";

        private const string HOME_CONTROLLERS_SUB = "/controllers";
        private const string HOME_CONTROLLERS_COUNT_SUB = "/controllers.count";
        
        private const string FURNITURE_SUB = "/furniture";
        private const string FURNITURE_COUNT_SUB = "/furniture.count";
        private void Awake()
        {
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        void SaveGame()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER + FOLDER);
            //Saving Home controllers
            SaveControllerData(formatter);

            //Saving Rooms
            SaveRoomData(formatter);
            
            //Saving Furniture
            SaveFurnitureData(formatter);
        }

        private static void SaveControllerData(BinaryFormatter formatter)
        {
            string path = Application.persistentDataPath + SAVE_FOLDER + FOLDER +  HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

            FileStream countStream = new FileStream(countPath, FileMode.Create);
            formatter.Serialize(countStream, GameData.HomeControllers.Count);

            for (int i = 0; i < GameData.HomeControllers.Count; i++)
            {
                FileStream stream = new FileStream(path + i, FileMode.Create);
                HomeControllerData data = new HomeControllerData(GameData.HomeControllers[i]);

                formatter.Serialize(stream, data);
                stream.Close();
            }
        }
        
        private static void SaveRoomData(BinaryFormatter formatter)
        {
            string path = Application.persistentDataPath + SAVE_FOLDER + FOLDER + ROOMS_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + ROOMS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

            FileStream countStream = new FileStream(countPath, FileMode.Create);
            formatter.Serialize(countStream, GameData.Rooms.Count);

            for (int i = 0; i < GameData.Rooms.Count; i++)
            {
                FileStream stream = new FileStream(path + i, FileMode.Create);
                RoomData data = new RoomData(GameData.Rooms[i]);

                formatter.Serialize(stream, data);
                stream.Close();
            }
        }
        
        private static void SaveFurnitureData(BinaryFormatter formatter)
        {
            string path = Application.persistentDataPath + SAVE_FOLDER + FOLDER + FURNITURE_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + FURNITURE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

            FileStream countStream = new FileStream(countPath, FileMode.Create);
            formatter.Serialize(countStream, GameData.Furniture.Count);

            for (int i = 0; i < GameData.Furniture.Count; i++)
            {
                FileStream stream = new FileStream(path + i, FileMode.Create);
                FurnitureData data = new FurnitureData(GameData.Furniture[i]);

                formatter.Serialize(stream, data);
                stream.Close();
            }
        }



        void LoadGame()
        {
            if (!Directory.Exists(Application.persistentDataPath + SAVE_FOLDER + FOLDER))
            {
                Debug.Log("Game not saved yet");
                return;
            }
            
            BinaryFormatter formatter = new BinaryFormatter();
            
            //Loading Home controllers
            LoadControllers(formatter);

            //Loading Rooms
            LoadRooms(formatter);
        }
        
        private void LoadControllers(BinaryFormatter formatter)
        {
            string controllersPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string controllersCountPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

            int controllerCount = 0;
            if (File.Exists(controllersCountPath))
            {
                FileStream controllersCountStream = new FileStream(controllersCountPath, FileMode.Open);
                controllerCount = (int) formatter.Deserialize(controllersCountStream);
                controllersCountStream.Close();
            }
            else
            {
                Debug.LogError("Path not found " + controllersCountPath);
                return;
            }

            for (int i = 0; i < controllerCount; i++)
            {
                if (File.Exists(controllersPath + i))
                {
                    FileStream stream = new FileStream(controllersPath + i, FileMode.Open);
                    HomeControllerData data = formatter.Deserialize(stream) as HomeControllerData;

                    stream.Close();

                    Vector3 homePos = new Vector3(data.position[0], data.position[1], data.position[2]);
                    Vector3 homeRot = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
                    HomeControllerObject home = Instantiate(homeControllerPrefab, homePos, Quaternion.identity);
                    home.transform.eulerAngles = homeRot;
                }
                else
                {
                    Debug.LogError("Path not found " + controllersPath + i);
                    return;
                }
            }
        }

        private void LoadRooms(BinaryFormatter formatter)
        {
            string roomsPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + ROOMS_SUB +
                               SceneManager.GetActiveScene().buildIndex;
            string roomsCountPath = Application.persistentDataPath + SAVE_FOLDER + FOLDER + ROOMS_COUNT_SUB +
                                    SceneManager.GetActiveScene().buildIndex;

            int roomCount = 0;
            if (File.Exists(roomsCountPath))
            {
                FileStream countStream = new FileStream(roomsCountPath, FileMode.Open);
                roomCount = (int) formatter.Deserialize(countStream);
                countStream.Close();
            }
            else
            {
                Debug.LogError("Path not found " + roomsCountPath);
                return;
            }

            for (int i = 0; i < roomCount; i++)
            {
                if (File.Exists(roomsPath + i))
                {
                    FileStream stream = new FileStream(roomsPath + i, FileMode.Open);
                    RoomData data = formatter.Deserialize(stream) as RoomData;

                    stream.Close();
                    
                    Vector3 roomPos = new Vector3(data.position[0], data.position[1], data.position[2]);
                    Vector3 roomSize = new Vector3(data.size[0], data.size[1], data.size[2]);
                    Vector3 roomRot = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
                    
                    string type = data.type;
                    Room prefab = prefabData.GetRoomPrefab(type);
                    Room room = Instantiate(prefab, roomPos, Quaternion.identity);

                    Transform roomTransform = room.transform;
                    roomTransform.localScale = roomSize;
                    roomTransform.eulerAngles = roomRot;
                }
                else
                {
                    Debug.LogError("Path not found " + roomsPath + i);
                    return;
                }
            }
        }
    }
}
