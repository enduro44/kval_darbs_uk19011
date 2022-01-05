using Controllers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        public string controllerID;
        public XRSocketInteractor socketL;
        public XRSocketInteractor socketR;
        public XRSocketInteractor socketC;

        void Awake()
        {
            GameData.Rooms.Add(this);
            
            if (gameObject.name == "SmallRoof(Clone)" || gameObject.name == "LargeRoof(Clone)" || gameObject.name == "CornerRoof(Clone)")
            {
                Debug.Log("Adding roof to grabbalbe rooms");
                RoomController.GrabbableRooms.Add(gameObject);
            }
        }

        private void OnDestroy()
        {
            EmptyActiveSocketController.RemoveSocket(controllerID, socketL);
            EmptyActiveSocketController.RemoveSocket(controllerID, socketR);
            EmptyActiveSocketController.RemoveSocket(controllerID, socketC);
            GameData.Rooms.Remove(this);
            RoomController.GrabbableRooms.Remove(gameObject);
        }
    }
}
