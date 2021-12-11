
using System.Collections.Generic;
using GameManagerData.data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class EmptyActiveSocketController : MonoBehaviour
    {
        private static EmptyActiveSocketController _instance;
        public List<EmptyActiveSocketData> emptyActiveSocketData = new List<EmptyActiveSocketData>();
        private SocketController _controller = new SocketController();
        
        private void Awake()
        {
            _instance = this;
        }
        
        public static EmptyActiveSocketController Instance() {
            return _instance;
        }

        public void AddSocket(string controllerID, XRSocketInteractor socket)
        {
            foreach (var data in emptyActiveSocketData)
            {
                Debug.Log("Controller IDs:");
                Debug.Log(controllerID);
                Debug.Log(data.controllerID);
                if (controllerID == data.controllerID)
                {
                    data.emptyActiveSockets.Add(socket);
                    Debug.Log("Controller found, room added");
                    return;
                }
            }

            EmptyActiveSocketData newData = new EmptyActiveSocketData(controllerID);
            newData.AddSocket(socket);
            emptyActiveSocketData.Add(newData);
            Debug.Log("Controller not found. New data created");
        }
        
        public void RemoveSocket(string controllerID, XRSocketInteractor socket)
        {
            foreach (var data in emptyActiveSocketData)
            {
                if (controllerID == data.controllerID)
                {
                    data.RemoveSocket(socket);
                }
            }
        }

        public void TurnOnAllSockets()
        {
            foreach (var data in emptyActiveSocketData)
            {
                foreach (var socket in data.emptyActiveSockets)
                {
                    _controller.TurnOnSocket(socket);
                }
            }
        }
        
        public void TurnOffAllSockets()
        {
            foreach (var data in emptyActiveSocketData)
            {
                foreach (var socket in data.emptyActiveSockets)
                {
                    _controller.TurnOffSocket(socket);
                }
            }
        }

        public void TurnOnAllForSpecificHome(string controllerID)
        {
            foreach (var data in emptyActiveSocketData)
            {
                if (controllerID == data.controllerID)
                {
                    foreach (var socket in data.emptyActiveSockets)
                    {
                        _controller.TurnOnSocket(socket);
                    }
                }
            }
        }
        
        public void TurnOffAllForSpecificHome(string controllerID)
        {
            foreach (var data in emptyActiveSocketData)
            {
                if (controllerID == data.controllerID)
                {
                    foreach (var socket in data.emptyActiveSockets)
                    {
                        _controller.TurnOffSocket(socket);
                    }
                }
            }
        }
    }
}