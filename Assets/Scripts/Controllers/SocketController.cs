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
        
        public void ToggleSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,1);

            socket.socketActive = true;
        }
        
        public void ToggleSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,2);

            socket.socketActive = true;
        }
        
        public void ToggleSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,3);

            socket.socketActive = true;
        }

        public void ResetSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,1);

            socket.socketActive = false;
        }
        
        public void ResetSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,2);

            socket.socketActive = false;
        }
        
        public void ResetSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            XRSocketInteractor socket = GetSocket(childObj,3);

            socket.socketActive = false;
        }
    }
}