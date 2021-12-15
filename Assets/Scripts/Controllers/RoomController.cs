using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Class controls if the room can be picked up by the player or not
    public class RoomController : MonoBehaviour
    {
        public static List<GameObject> GrabbableRooms = new List<GameObject>();
        private XRGrabInteractable _grabber;
        private bool _hasObjectL;
        private bool _hasObjectR;
        private bool _hasObjectC;

        void Awake()
        {
            _grabber = gameObject.GetComponent<XRGrabInteractable>();
            GameObject box = gameObject.transform.GetChild(0).gameObject;
            GameObject grandChildObjL = box.transform.GetChild(1).gameObject;
            GameObject grandChildObjR = box.transform.GetChild(2).gameObject;
            GameObject grandChildObjC = box.transform.GetChild(3).gameObject;
            GameObject left = grandChildObjL.transform.GetChild(0).gameObject;
            GameObject right = grandChildObjR.transform.GetChild(0).gameObject;
            GameObject ceiling = grandChildObjC.transform.GetChild(0).gameObject;
            XRSocketInteractor socketL = left.GetComponent<XRSocketInteractor>();
            XRSocketInteractor socketR = right.GetComponent<XRSocketInteractor>();
            XRSocketInteractor socketC = ceiling.GetComponent<XRSocketInteractor>();
            socketL.selectEntered.AddListener(EnteredL);
            socketL.selectExited.AddListener(ExitedL);
            socketR.selectEntered.AddListener(EnteredR);
            socketR.selectExited.AddListener(ExitedR);
            socketC.selectEntered.AddListener(EnteredC);
            socketC.selectExited.AddListener(ExitedC);
        }

        private void EnteredL(SelectEnterEventArgs args)
        {
            _hasObjectL = true;
            ToggleGrab();
            
            GameObject obj = args.interactable.gameObject;
            GrabbableRooms.Add(obj);
            GrabbableRooms.Remove(gameObject);
            
            Debug.Log(GrabbableRooms.Count);
        }
        
        private void ExitedL(SelectExitEventArgs args)
        {
            _hasObjectL = false;
            ToggleGrab();
            
            GameObject room = args.interactable.gameObject;
            GrabbableRooms.Remove(room);
            ProcessBaseRoom(gameObject);
            
            Debug.Log(GrabbableRooms.Count);
        }
        private void EnteredR(SelectEnterEventArgs args)
        {
            _hasObjectR = true;
            ToggleGrab();
            
            GameObject obj = args.interactable.gameObject;
            GrabbableRooms.Add(obj);
            GrabbableRooms.Remove(gameObject);
            
            Debug.Log(GrabbableRooms.Count);
        }
        
        private void ExitedR(SelectExitEventArgs args)
        {
            _hasObjectR = false;
            ToggleGrab();
            
            GameObject room = args.interactable.gameObject;
            GrabbableRooms.Remove(room);
            ProcessBaseRoom(gameObject);
            
            Debug.Log(GrabbableRooms.Count);
        }
        private void EnteredC(SelectEnterEventArgs args)
        {
            _hasObjectC = true;
            ToggleGrab();
            
            GameObject obj = args.interactable.gameObject;
            GrabbableRooms.Add(obj);
            GrabbableRooms.Remove(gameObject);
            
            Debug.Log(GrabbableRooms.Count);
        }
        
        private void ExitedC(SelectExitEventArgs args)
        {
            _hasObjectC = false;
            ToggleGrab();
            
            GameObject room = args.interactable.gameObject;
            GrabbableRooms.Remove(room);
            ProcessBaseRoom(gameObject);
            
            Debug.Log(GrabbableRooms.Count);
        }

        private void ToggleGrab()
        {
            if (_hasObjectL || _hasObjectR || _hasObjectC)
            {
                //Changing the layer to "Socket" only, so the player can't pick up the room
                _grabber.interactionLayerMask = (1 << 6);
                return;
            }
            //Changing the layer back to "Socket" and "Player"
            _grabber.interactionLayerMask = (1<<6) | (1<<7);
        }
        
        private void ProcessBaseRoom(GameObject obj)
         {
             if (_hasObjectL || _hasObjectR || _hasObjectC)
             {
                 return;
             }
             GrabbableRooms.Add(gameObject);
         }
        
        public static void ToggleGrabOffForGrabbableRooms()
         {
             foreach (var room in GrabbableRooms)
             {
                 room.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 6);
             }
         }
        
        public static void ToggleGrabOnForGrabbableRooms()
         {
             foreach (var room in GrabbableRooms)
             {
                 room.GetComponent<XRGrabInteractable>().interactionLayerMask = (1<<6) | (1<<7);
             }
         }
    }
}

