using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerData
{
    public class GameManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private LargeRoom largeRoomPrefab;
        [SerializeField] private HomeControllerObject homeControllerPrefab;
        
        [Header("Constants")]
        private const string ROOMS_SUB = "/rooms";
        private const string ROOMS_COUNT_SUB = "/rooms.count";

        private const string HOME_CONTROLLERS_SUB = "/controllers";
        private const string HOME_CONTROLLERS_COUNT_SUB = "/controllers.count";
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
            
            //Saving Home controllers
            string controllersPath = Application.persistentDataPath + HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string controllersCountPath = Application.persistentDataPath + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
            
            FileStream controllersCountStream = new FileStream(controllersCountPath, FileMode.Create);
            formatter.Serialize(controllersCountStream, GameData.HomeControllers.Count);
            
            for (int i = 0; i < GameData.HomeControllers.Count; i++)
            {
                FileStream stream = new FileStream(controllersPath + i, FileMode.Create);
                HomeControllerData data = new HomeControllerData(GameData.HomeControllers[i]);

                formatter.Serialize(stream, data);
                stream.Close();
            }
            
            //Saving Rooms
            string roomsPath = Application.persistentDataPath + ROOMS_SUB + SceneManager.GetActiveScene().buildIndex;
            string roomsCountPath = Application.persistentDataPath + ROOMS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
            
            FileStream roomCountStream = new FileStream(roomsCountPath, FileMode.Create);
            formatter.Serialize(roomCountStream, GameData.Rooms.Count);
            
            for (int i = 0; i < GameData.Rooms.Count; i++)
            {
                FileStream stream = new FileStream(roomsPath + i, FileMode.Create);
                RoomData data = new RoomData(GameData.Rooms[i]);

                formatter.Serialize(stream, data);
                stream.Close();
            }
        }

        void LoadGame()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            //Loading Home controllers
            string controllersPath = Application.persistentDataPath + HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string controllersCountPath = Application.persistentDataPath + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

            int controllerCount = 0;
            if (File.Exists(controllersCountPath))
            {
                FileStream controllersCountStream = new FileStream(controllersCountPath, FileMode.Open);
                controllerCount = (int)formatter.Deserialize(controllersCountStream);
                controllersCountStream.Close();
            }
            else
            {
                Debug.LogError("Path not found " + controllersCountPath);
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
                }
            }

            //Loading Rooms
            string roomsPath = Application.persistentDataPath + ROOMS_SUB + SceneManager.GetActiveScene().buildIndex;
            string roomsCountPath = Application.persistentDataPath + ROOMS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

            int roomCount = 0;
            if (File.Exists(roomsCountPath))
            {
                FileStream countStream = new FileStream(roomsCountPath, FileMode.Open);
                roomCount = (int)formatter.Deserialize(countStream);
                countStream.Close();
            }
            else
            {
                Debug.LogError("Path not found " + roomsCountPath);
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
                    LargeRoom largeRoom = Instantiate(largeRoomPrefab, roomPos, Quaternion.identity);

                    Transform largeRoomTransform = largeRoom.transform;
                    largeRoomTransform.localScale = roomSize;
                    largeRoomTransform.eulerAngles = roomRot;
                }
                else
                {
                    Debug.LogError("Path not found " + roomsPath + i);
                }
            }
        }
    }
}
