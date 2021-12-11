using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

namespace GameManagerData.data
{
    [Serializable]
    public class EmptyActiveSocketData
    {
        public string controllerID;
        public List<XRSocketInteractor> emptyActiveSockets;

        public EmptyActiveSocketData(string controllerID)
        {
            this.controllerID = controllerID;
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