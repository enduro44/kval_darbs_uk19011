using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Controllers;
using GameManagerData.data;
using GameManagerData.objClasses;
using MenuSystem.Main;
using MenuSystem.Wrist;
using UnityEngine;
using Application = UnityEngine.Application;

namespace GameManagerData
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public SceneController sceneController;
        
        [Header("Load data")]
        private string _saveNameData;
        private static bool _loadGame = false;
        private PlayerData _playerLoadData = new PlayerData();
        private List<HomeLoadData> _homeLoadData = new List<HomeLoadData>();
        private List<RoomData> _freeroamRoomData = new List<RoomData>();
        private FurnitureLoadData _furnitureLoadData = new FurnitureLoadData();
        private PlayableLoadData _playableLoadData = new PlayableLoadData();
        public InstantiateLoadedData instantiateLoadedData;

        [Header("Constants")] 
        private const string PLAYER = "/player";
        
        private const string ROOMS_SUB = "/rooms";
        private const string ROOMS_COUNT_SUB = "/rooms.count";

        private const string HOME_CONTROLLERS_SUB = "/controllers";
        private const string HOME_CONTROLLERS_COUNT_SUB = "/controllers.count";
        
        private const string FURNITURE_SUB = "/furniture";
        private const string FURNITURE_COUNT_SUB = "/furniture.count";
        
        private const string PLAYABLE_SUB = "/playable";
        private const string PLAYABLE_COUNT_SUB = "/playable.count";

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public static GameManager Instance() {
            return _instance;
        }

        public void StartNewGame()
        {
            ResetGameData();
            PlayerData.GameID = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
            Directory.CreateDirectory(Application.persistentDataPath + "/" + PlayerData.GameID);
            LoadNewScene("Testing");
            
        }

        public bool SaveGame()
        {
            string folder = PlayerData.GameID;

            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                //Saving Player 
                SavePlayer(formatter, folder);

                //Saving Home controllers
                SaveControllerData(formatter, folder);

                //Saving Rooms
                SaveRoomData(formatter, folder);

                //Saving Furniture
                SaveFurnitureData(formatter, folder);

                //Saving Playables
                SavePlayableData(formatter, folder);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
            return true;
        }

        private void SavePlayer(BinaryFormatter formatter, string folder)
        {
            string path = Application.persistentDataPath + "/" + folder +  PLAYER;
            
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerController playerController = PlayerController.Instance();
            PlayerData gameData = playerController.SetPlayerData();

            formatter.Serialize(stream, gameData);
            stream.Close();
        }

        private void SaveControllerData(BinaryFormatter formatter, string folder)
        {
            string path = Application.persistentDataPath + "/" + folder +  HOME_CONTROLLERS_SUB;
            string countPath = Application.persistentDataPath + "/" + folder + HOME_CONTROLLERS_COUNT_SUB;

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
            string path = Application.persistentDataPath + "/" + folder + ROOMS_SUB;
            string countPath = Application.persistentDataPath + "/" + folder + ROOMS_COUNT_SUB;

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
            string path = Application.persistentDataPath + "/" + folder + FURNITURE_SUB;
            string countPath = Application.persistentDataPath + "/" + folder + FURNITURE_COUNT_SUB;

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
        
        private void SavePlayableData(BinaryFormatter formatter, string folder)
        {
            string path = Application.persistentDataPath + "/" + folder + PLAYABLE_SUB;
            string countPath = Application.persistentDataPath + "/" + folder + PLAYABLE_COUNT_SUB;

            FileStream countStream = new FileStream(countPath, FileMode.Create);
            formatter.Serialize(countStream, GameData.Playables.Count);

            for (int i = 0; i < GameData.Playables.Count; i++)
            {
                FileStream stream = new FileStream(path + i, FileMode.Create);
                PlayableData data = new PlayableData(GameData.Playables[i]);

                formatter.Serialize(stream, data);
                stream.Close();
            }
            // WristMenu wristMenu = WristMenu.Instance();
            // wristMenu.GameSavedWithPopup();
            
        }

        public void LoadGame(string saveName)
        {
            ResetGameData();
            _saveNameData = saveName;
            MainMenu mainMenu = MainMenu.Instance();
            
            if (!Directory.Exists(Application.persistentDataPath + "/" + saveName))
            {
                Debug.Log("Game does not exist, path: " + Application.persistentDataPath + "/" + saveName);
                mainMenu.ShowGameCouldNotBeLoadedError();
                return;
            }

            //Case where game was created, but nothing was saved
            string[] files = Directory.GetFiles(Application.persistentDataPath + "/" + saveName);
             if (files.Length == 0)
             {
                 _saveNameData = saveName;
                 LoadNewScene("Testing");
                 return;
             }

            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                LoadGameData();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                mainMenu.ShowGameCouldNotBeLoadedError();
                return;
            }
            LoadNewScene(_playerLoadData.sceneType);
            _loadGame = true;
        }

        public void LoadNewScene(string sceneName)
        {
            sceneController.StartSceneLoad(sceneName);
        }

        public void LoadGameData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            //Laoding Player
            LoadPlayer(formatter, _saveNameData);
            
            //Loading Home controllers
            LoadControllers(formatter, _saveNameData);

            //Loading Rooms
            LoadRooms(formatter, _saveNameData);

            //Loading Furniture
            LoadFurniture(formatter, _saveNameData);

            //Loading Playables
            LoadPlayables(formatter, _saveNameData);
        }

        private void LoadPlayer(BinaryFormatter formatter, string saveName)
        {
            
            string path = Application.persistentDataPath + "/" + saveName +  PLAYER;
            
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                _playerLoadData = formatter.Deserialize(stream) as PlayerData;

                stream.Close();
            }
            else
            {
                throw new Exception("Path not found " + path);
            }
        }
        
        private void LoadControllers(BinaryFormatter formatter, string saveName)
        {
            string controllersPath = Application.persistentDataPath + "/" + saveName + HOME_CONTROLLERS_SUB;
            string controllersCountPath = Application.persistentDataPath + "/" + saveName + HOME_CONTROLLERS_COUNT_SUB;

            int controllerCount = 0;
            if (File.Exists(controllersCountPath))
            {
                FileStream controllersCountStream = new FileStream(controllersCountPath, FileMode.Open);
                controllerCount = (int) formatter.Deserialize(controllersCountStream);
                controllersCountStream.Close();
            }
            else
            {
                throw new Exception("Path not found " + controllersCountPath);
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

                    HomeLoadData newControllerStruct = new HomeLoadData();
                    newControllerStruct.AddControllerData(data);
                    _homeLoadData.Add(newControllerStruct);
                }
                else
                {
                    throw new Exception("Path not found " + controllersPath + i);
                }
            }
        }

        private void LoadRooms(BinaryFormatter formatter, string saveName)
        {
            string roomsPath = Application.persistentDataPath + "/" + saveName + ROOMS_SUB;
            string roomsCountPath = Application.persistentDataPath + "/" + saveName + ROOMS_COUNT_SUB;

            int roomCount = 0;
            if (File.Exists(roomsCountPath))
            {
                FileStream countStream = new FileStream(roomsCountPath, FileMode.Open);
                roomCount = (int) formatter.Deserialize(countStream);
                countStream.Close();
            }
            else
            {
                throw new Exception("Path not found " + roomsCountPath);
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
                    
                    foreach (var controller in _homeLoadData)
                    {
                        if (controller.ControllerID == data.controllerID)
                        {
                            controller.AddRoomData(data);
                            data = null;
                            break;
                        }
                    }

                    if (data != null)
                    {
                        _freeroamRoomData.Add(data);
                    }
                }
                else
                {
                    throw new Exception("Path not found " + roomsPath + i);
                }
            }
        }
        
        private void LoadFurniture(BinaryFormatter formatter, string saveName)
        {
            string path = Application.persistentDataPath + "/" + saveName + FURNITURE_SUB;
            string countPath = Application.persistentDataPath + "/" + saveName + FURNITURE_COUNT_SUB;

            int furnitureCount = 0;
            if (File.Exists(countPath))
            {
                FileStream countStream = new FileStream(countPath, FileMode.Open);
                furnitureCount = (int) formatter.Deserialize(countStream);
                countStream.Close();
            }
            else
            {
                throw new Exception("Path not found " + countPath);
            }

            for (int i = 0; i < furnitureCount; i++)
            {
                if (File.Exists(path + i))
                {
                    FileStream stream = new FileStream(path + i, FileMode.Open);
                    FurnitureData data = formatter.Deserialize(stream) as FurnitureData;

                    stream.Close();

                    if (data == null)
                    {
                        return;
                    }
                    
                    _furnitureLoadData.AddFurnitureData(data);
                }
                else
                {
                    throw new Exception("Path not found " + path + i);
                }
            }
        }
        
        private void LoadPlayables(BinaryFormatter formatter, string saveName)
        {
            string path = Application.persistentDataPath + "/" + saveName + PLAYABLE_SUB;
            string countPath = Application.persistentDataPath + "/" + saveName + PLAYABLE_COUNT_SUB;

            int playableCount = 0;
            if (File.Exists(countPath))
            {
                FileStream countStream = new FileStream(countPath, FileMode.Open);
                playableCount = (int) formatter.Deserialize(countStream);
                countStream.Close();
            }
            else
            {
                throw new Exception("Path not found " + countPath);
            }

            for (int i = 0; i < playableCount; i++)
            {
                if (File.Exists(path + i))
                {
                    FileStream stream = new FileStream(path + i, FileMode.Open);
                    PlayableData data = formatter.Deserialize(stream) as PlayableData;

                    stream.Close();

                    if (data == null)
                    {
                        return;
                    }
                    
                    _playableLoadData.AddPlayableData(data);
                }
                else
                {
                    throw new Exception("Path not found " + path + i);
                }
            }
        }

        public void InstantiateLoadedData()
        {
            if (!_loadGame)
            {
                return;
            }
            StartCoroutine(ProcessLoadedData());
        }

        IEnumerator ProcessLoadedData()
        {
            //Creating each controller and its rooms first
            foreach (var controller in _homeLoadData)
            {
                GameObject home = instantiateLoadedData.LoadSavedController(controller.HomeControllerData);
                foreach (var room in controller.ControllersRooms)
                {
                    instantiateLoadedData.LoadSavedRoom(room);
                }
                yield return new WaitForSeconds(0.5f);
                EmptyActiveSocketController.TurnOffAllForSpecificHome(home.GetComponent<HomeControllerObject>().controllerID);
            }
            
            //Adding free roaming rooms
            foreach (var room in _freeroamRoomData)
            {
                instantiateLoadedData.LoadSavedRoom(room);
            }

            //Adding furniture
            foreach (var furniture in _furnitureLoadData.Furniture)
            {
                instantiateLoadedData.LoadSavedFurniture(furniture);
            }
            
            //Adding playables
            foreach (var playable in _playableLoadData.Playables)
            {
                instantiateLoadedData.LoadSavedPlayable(playable);
            }
            
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            
            yield return new WaitForSeconds(2f);
            PlayerController playerController = PlayerController.Instance();
            playerController.PreparePlayerLoadGame(_playerLoadData);
            
            //Setting back the variable to false
            _loadGame = false;
        }

        public static bool IsLoadGame()
        {
            return _loadGame;
        }

        public string[] GetSavedGames()
        {
            string path = Application.persistentDataPath;
            string[] savedGames = Directory.GetDirectories(path)
                .Select(Path.GetFileName)
                .ToArray();
            return savedGames;
        }

        public void DeleteGame(string saveName)
        {
            Directory.Delete(Application.persistentDataPath + "/" + saveName, true);
        }

        public void ResetGameData()
        {
            EmptyActiveSocketController.EmptyActiveSockets.Clear();
            RoomController.GrabbableRooms.Clear();
            GameData.HomeControllers.Clear();
            GameData.Rooms.Clear();
            GameData.Furniture.Clear();
            GameData.Playables.Clear();
            
            _homeLoadData.Clear();
            _freeroamRoomData.Clear();
            _furnitureLoadData.Furniture.Clear();
            _playableLoadData.Playables.Clear();
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
