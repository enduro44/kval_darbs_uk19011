using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketController
    {
        public XRSocketInteractor Socket;

        private XRSocketInteractor GetSocket(GameObject obj, int index)
        {
            GameObject boxWall = obj.transform.GetChild(index).gameObject;
            return boxWall.GetComponent<XRSocketInteractor>();
        }
        
        public void TurnOnSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,1);

            socket.socketActive = true;
        }
        
        public void TurnOnSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,2);

            socket.socketActive = true;
        }
        
        public void TurnOnSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,3);

            socket.socketActive = true;
        }

        public void TurnOffSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,1);

            socket.socketActive = false;
        }
        
        public void TurnOffSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,2);

            socket.socketActive = false;
        }
        
        public void TurnOffSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,3);

            socket.socketActive = false;
        }
    }
}