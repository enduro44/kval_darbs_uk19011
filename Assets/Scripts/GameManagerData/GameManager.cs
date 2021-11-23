using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        public InstantiateSaveData instantiateSaveData;
        
        [Header("Constants")]
        private static readonly string FOLDER = PlayerData.GameID;

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
            BinaryFormatter formatter = new BinaryFormatter();
            //Saving Home controllers
            SaveControllerData(formatter);

            //Saving Rooms
            SaveRoomData(formatter);
            
            //Saving Furniture
            SaveFurnitureData(formatter);
        }

        private void SaveControllerData(BinaryFormatter formatter)
        {
            string path = Application.persistentDataPath + FOLDER +  HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + FOLDER + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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
        
        private void SaveRoomData(BinaryFormatter formatter)
        {
            string path = Application.persistentDataPath + FOLDER + ROOMS_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + FOLDER + ROOMS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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
        
        private void SaveFurnitureData(BinaryFormatter formatter)
        {
            string path = Application.persistentDataPath + FOLDER + FURNITURE_SUB + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + FOLDER + FURNITURE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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
                Debug.Log("Game not saved yet, path: " + Application.persistentDataPath + "/" + saveName);
                return;
            }
            
            BinaryFormatter formatter = new BinaryFormatter();
            SceneManager.LoadScene("Testing");
            
            //Loading Home controllers
            LoadControllers(formatter, saveName);

            //Loading Rooms
            LoadRooms(formatter, saveName);
        }
        
        private void LoadControllers(BinaryFormatter formatter, string saveName)
        {
            string controllersPath = Application.persistentDataPath + "/" + saveName + HOME_CONTROLLERS_SUB + SceneManager.GetActiveScene().buildIndex;
            string controllersCountPath = Application.persistentDataPath + "/" + saveName + HOME_CONTROLLERS_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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

                    instantiateSaveData.LoadSavedControllers(data);
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
            string roomsPath = Application.persistentDataPath + "/" + saveName + ROOMS_SUB +
                               SceneManager.GetActiveScene().buildIndex;
            string roomsCountPath = Application.persistentDataPath + "/" + saveName + ROOMS_COUNT_SUB +
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
                    
                    instantiateSaveData.LoadSavedRooms(data);
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
