using GameManagerData;
using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketController
    {
        private GameObject _socketVisual;
        private EmptyActiveSocketController _emptyActiveSocketController = EmptyActiveSocketController.Instance();

        private XRSocketInteractor GetSocket(GameObject obj, int index)
        {
            GameObject boxWall = obj.transform.GetChild(index).gameObject;
            return boxWall.GetComponent<XRSocketInteractor>();
        }
        
        private GameObject GetSocketVisual(XRSocketInteractor obj)
        {
            GameObject visual = obj.transform.GetChild(0).gameObject;
            return visual;
        }
        
        public string GetType(XRBaseInteractable obj)
        {
            GameObject objInSocket = GetRoot(obj);
            return objInSocket.name;
        }

        public GameObject GetRoot(XRBaseInteractable obj)
        {
            return obj.transform.root.gameObject;
        }
        
        public void ToogleConnectedTag(XRBaseInteractable obj)
        {
            GameObject rootObj = obj.transform.root.gameObject;
            if (rootObj.CompareTag("Connected"))
            {
                rootObj.tag = "Disconnected";
            }
            else
            {
                rootObj.tag = "Connected";
            }
        }

        public void TurnOnSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(1).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);
            
            AddSocketToList(obj, socket);
            TurnOnSocket(socket);
        }
        
        public void TurnOnSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(2).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            AddSocketToList(obj, socket);
            TurnOnSocket(socket);
        }
        
        public void TurnOnSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(3).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            AddSocketToList(obj, socket);
            TurnOnSocket(socket);
        }

        public void TurnOffSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(1).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            RemoveSocketFromList(obj, socket);
            TurnOffSocket(socket);
        }
        
        public void TurnOffSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(2).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            RemoveSocketFromList(obj, socket);
            TurnOffSocket(socket);
        }
        
        public void TurnOffSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(3).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            RemoveSocketFromList(obj, socket);
            TurnOffSocket(socket);
        }

        public void TurnOnSocket(XRSocketInteractor socket)
        {
            socket.socketActive = true;

            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(true);
        }
        
        public void TurnOffSocket(XRSocketInteractor socket)
        {
            socket.socketActive = false;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(false);
        }

        public void AddSocketToList(XRBaseInteractable obj, XRSocketInteractor socket)
        {
            GameObject root = GetRoot(obj);
            Room room = root.GetComponent<Room>();
            _emptyActiveSocketController.AddSocket(room.controllerID, socket);
        }

        public void RemoveSocketFromList(XRBaseInteractable obj, XRSocketInteractor socket)
        {
            GameObject root = GetRoot(obj);
            Room room = root.GetComponent<Room>();
            _emptyActiveSocketController.RemoveSocket(room.controllerID, socket);
        }
    }
}