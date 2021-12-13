using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

namespace GameManagerData.data
{
    [Serializable]
    public class EmptyActiveSocketData
    {
        public string controllerID;
        public XRSocketInteractor controllerSocket;
        public bool isControllerEmpty = true;
        public List<XRSocketInteractor> emptyActiveSockets;

        public EmptyActiveSocketData(string controllerID, XRSocketInteractor controllerSocket)
        {
            this.controllerID = controllerID;
            this.controllerSocket = controllerSocket;
            emptyActiveSockets = new List<XRSocketInteractor>();
        }
        
        public void AddSocket(XRSocketInteractor socket)
        {
            emptyActiveSockets.Add(socket);
        }
        
        public void RemoveSocket(XRSocketInteractor socket)
        {
            emptyActiveSockets.Remove(socket);
        }
    }
}