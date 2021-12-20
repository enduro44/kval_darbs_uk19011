using Controllers;
using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        public string controllerID;
        
        void Awake()
        {
            Debug.Log("Object created");
            GameData.Rooms.Add(this);
        }

        private void OnDestroy()
        {
            Debug.Log("Object deleted");
            GameData.Rooms.Remove(this);
            RoomController.GrabbableRooms.Remove(gameObject);
        }
    }
}
