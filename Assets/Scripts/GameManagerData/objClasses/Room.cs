using Controllers;
using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        public string controllerID;
        
        void Awake()
        {
            GameData.Rooms.Add(this);
        }

        private void OnDestroy()
        {
            GameData.Rooms.Remove(this);
            RoomController.GrabbableRooms.Remove(gameObject);
        }
    }
}
