using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;

/*
 * Class takes data from file and creates saved game controllers in the scene
 */
namespace GameManagerData
{
    public class InstantiateLoadedData : MonoBehaviour
    {
        public PrefabData prefabData;
        
        public void LoadSavedController(HomeControllerData data)
        {
            prefabData = PrefabData.Instance();
            Vector3 homePos = new Vector3(data.position[0], data.position[1], data.position[2]);
            Vector3 homeRot = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
            HomeControllerObject prefab = prefabData.GetControllerPrefab();
            HomeControllerObject home = Instantiate(prefab, homePos, Quaternion.identity);
            home.transform.eulerAngles = homeRot;
            home.controllerID = data.controlledID;
        }
        
        public void LoadSavedRoom(RoomData data)
        {
            prefabData = PrefabData.Instance();
            Vector3 roomPos = new Vector3(data.position[0], data.position[1], data.position[2]);
            Vector3 roomSize = new Vector3(data.size[0], data.size[1], data.size[2]);
            Vector3 roomRot = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
                    
            string type = data.type;
            Room prefab = prefabData.GetRoomPrefab(type);
            Room room = Instantiate(prefab, roomPos, Quaternion.identity);
                    
            Transform roomTransform = room.transform;
            roomTransform.localScale = roomSize;
            roomTransform.eulerAngles = roomRot;

            room.controllerID = data.controllerID;
        }
    }
}
