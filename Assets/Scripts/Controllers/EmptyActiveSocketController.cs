using System.Collections.Generic;
using GameManagerData.data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class EmptyActiveSocketController : MonoBehaviour
    {
        public static List<EmptyActiveSocketData> EmptyActiveSockets = new List<EmptyActiveSocketData>();
        private static SocketController _controller = new SocketController();

        public static void AddData(EmptyActiveSocketData data)
        {
            EmptyActiveSockets.Add(data);
        }
        
        public static void RemoveData(EmptyActiveSocketData data)
        {
            EmptyActiveSockets.Remove(data);
        }

        public static void AddSocket(string controllerID, XRSocketInteractor socket)
        {
            foreach (var data in EmptyActiveSockets)
            {
                if (controllerID == data.controllerID)
                {
                    data.AddSocket(socket);
                    return;
                }
            }
        }
        
        public static void RemoveSocket(string controllerID, XRSocketInteractor socket)
        {
            foreach (var data in EmptyActiveSockets)
            {
                if (controllerID == data.controllerID)
                {
                    data.RemoveSocket(socket);
                }
            }
        }

        public static void TurnOnAllSockets()
        {
            foreach (var data in EmptyActiveSockets)
            {
                if (data.isControllerEmpty)
                {
                    _controller.TurnOnControllerSocket(data.controllerSocket);
                    break;
                }
                foreach (var socket in data.emptyActiveSockets)
                {
                    _controller.TurnOnSocket(socket);
                }
            }
        }
        
        public static void TurnOffAllSockets()
        {
            foreach (var data in EmptyActiveSockets)
            {
                if (data.isControllerEmpty)
                {
                    _controller.TurnOffControllerSocket(data.controllerSocket);
                    
                    break;
                }
                foreach (var socket in data.emptyActiveSockets)
                {
                    _controller.TurnOffSocket(socket);
                }
            }
        }

        public static void TurnOffAllForSpecificHome(string controllerID)
        {
            foreach (var data in EmptyActiveSockets)
            {
                if (data.isControllerEmpty)
                {
                    _controller.TurnOffControllerSocket(data.controllerSocket);
                    break;
                }
                
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