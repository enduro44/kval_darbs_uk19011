using Controllers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
            //RoomController.GrabbableRooms.Remove(gameObject);
        }
    }
}
