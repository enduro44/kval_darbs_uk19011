using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerL : MonoBehaviour
    {
        void Awake()
        {
            XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
            socket.selectEntered.AddListener(Entered);
            socket.selectExited.AddListener(Exited);
        }
    
        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            SocketController socket = new SocketController();
            socket.ToggleSocketLeft(obj);
            socket.ToggleSocketCeiling(obj);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            SocketController socket = new SocketController();
            socket.ResetSocketLeft(obj);
            socket.ResetSocketCeiling(obj);
        }
    
        private void ToggleSocket(XRBaseInteractable obj)
        {
            SocketController socket = new SocketController();
            socket.ToggleSocketLeft(obj);
        }
    }
}
