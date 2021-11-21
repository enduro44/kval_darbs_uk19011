using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketController
    {
        private GameObject _socketVisual;
        private GameObject _root;

        private XRSocketInteractor GetSocket(GameObject obj, int index)
        {
            GameObject boxWall = obj.transform.GetChild(index).gameObject;
            return boxWall.GetComponent<XRSocketInteractor>();
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

        private GameObject GetSocketVisual(XRSocketInteractor obj)
        {
            GameObject visual = obj.transform.GetChild(0).gameObject;
            return visual;
        }
        
        public void TurnOnSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(1).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            socket.socketActive = true;

            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(true);
        }
        
        public void TurnOnSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(2).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            socket.socketActive = true;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(true);
        }
        
        public void TurnOnSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(3).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            socket.socketActive = true;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(true);
        }

        public void TurnOffSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(1).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            socket.socketActive = false;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(false);
        }
        
        public void TurnOffSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(2).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            socket.socketActive = false;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(false);
        }
        
        public void TurnOffSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(3).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            socket.socketActive = false;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(false);
        }

        public string GetType(XRBaseInteractable obj)
        {
            GameObject objInSocket = obj.transform.root.gameObject;
            return objInSocket.name;
        }
    }
}