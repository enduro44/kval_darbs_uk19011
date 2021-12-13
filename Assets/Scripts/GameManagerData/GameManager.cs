using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Controllers;
using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

namespace GameManagerData
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public SceneController sceneController;
        
        [Header("Load data")]
        private string _saveNameData;
        private bool _loadGame = false;
        private List<LoadData> _loadData = new List<LoadData>();
        public InstantiateLoadedData instantiateLoadedData;

        [Header("Constants")] 
        private const string ROOMS_SUB = "/rooms";
        private const string ROOMS_COUNT_SUB = "/rooms.count";

        private const string HOME_CONTROLLERS_SUB = "/controllers";
        private const string HOME_CONTROLLERS_COUNT_SUB = "/controllers.count";
        
        private const string FURNITURE_SUB = "/furniture";
        private const string FURNITURE_COUNT_SUB = "/furniture.count";

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public static GameManager Instance() {
            return _instance;
        }

        public void SaveGame()
        {
            string folder = PlayerData.GameID;
            BinaryFormatter formatter = new BinaryFormatter();
            
            //Saving Home controllers
            SaveControllerData(formatter, folder);

            //Saving Rooms
            SaveRoomData(formatter, folder);
            
            //Saving Furniture
            SaveFurnitureData(formatter, folder);
        }

        private void SaveControllerData(BinaryFormatter formatter, string folder)
        {
            string path = Application.persistentDataPath + "/" + folder +  HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + "/" + folder + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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
        
        private void SaveRoomData(BinaryFormatter formatter, string folder)
        {
            string path = Application.persistentDataPath + "/" + folder + ROOMS_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + "/" + folder + ROOMS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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
        
        private void SaveFurnitureData(BinaryFormatter formatter, string folder)
        {
            string path = Application.persistentDataPath + "/" + folder + FURNITURE_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + "/" + folder + FURNITURE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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

        public void LoadGame(string saveName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + saveName))
            {
                Debug.Log("Game does not exist, path: " + Application.persistentDataPath + "/" + saveName);
                return;
            }
            
            sceneController.StartSceneLoad("Testing");
            _saveNameData = saveName;
            _loadGame = true;
        }

        public void LoadGameData()
        {
            if (!_loadGame)
            {
                return;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            
            //Loading Home controllers
            LoadControllers(formatter, _saveNameData);

            //Loading Rooms
            LoadRooms(formatter, _saveNameData);

            //Creating loaded data in the scene
            InstantiateLoadedData();

            //Setting back the variable to false
            _loadGame = false;
        }
        
        private void LoadControllers(BinaryFormatter formatter, string saveName)
        {
            string controllersPath = Application.persistentDataPath + "/" + saveName + HOME_CONTROLLERS_SUB + SceneManager.GetSceneByName("Testing").buildIndex;
            string controllersCountPath = Application.persistentDataPath + "/" + saveName + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetSceneByName("Testing").buildIndex;

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
                    
                    if (data == null)
                    {
                        return;
                    }

                    LoadData newControllerStruct = new LoadData();
                    newControllerStruct.AddControllerData(data);
                    _loadData.Add(newControllerStruct);
                }
                else
                {
                    Debug.LogError("Path not found " + controllersPath + i);
                    return;
                }
            }
        }

        private void LoadRooms(BinaryFormatter formatter, string saveName)
        {
            string roomsPath = Application.persistentDataPath + "/" + saveName + ROOMS_SUB + SceneManager.GetSceneByName("Testing").buildIndex;
            string roomsCountPath = Application.persistentDataPath + "/" + saveName + ROOMS_COUNT_SUB + SceneManager.GetSceneByName("Testing").buildIndex;

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

                    if (data == null)
                    {
                        return;
                    }
                    
                    foreach (var controller in _loadData)
                    {
                        if (controller.ControllerID == data.controllerID)
                        {
                            controller.AddRoomData(data);
                            data = null;
                            break;
                        }
                    }
                    
                    HandleParentlessRoom(data);
                }
                else
                {
                    Debug.LogError("Path not found " + roomsPath + i);
                    return;
                }
            }
        }

        private void HandleParentlessRoom(RoomData data)
        {
            //If room has not been attached to a specific controller we can load it already
            if (data != null)
            {
                instantiateLoadedData.LoadSavedRoom(data);
            }
        }

        private void InstantiateLoadedData()
        {
            StartCoroutine(LoadControllerAndRooms());

            //EmptyActiveSocketController.TurnOnAllSockets();
        }

        IEnumerator LoadControllerAndRooms()
        {
            //Creating each controller and its rooms first
            foreach (var controller in _loadData)
            {
                HomeControllerObject home = instantiateLoadedData.LoadSavedController(controller.HomeControllerData);
                foreach (var room in controller.ControllersRooms)
                {
                    instantiateLoadedData.LoadSavedRoom(room);
                }
                yield return new WaitForSeconds(0.5f);
                EmptyActiveSocketController.TurnOffAllForSpecificHome(home.controllerID);
            }
            yield return new WaitForSeconds(2f);
            sceneController.SetSceneReady();
        }
    }
}
