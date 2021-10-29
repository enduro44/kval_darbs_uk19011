using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerR : MonoBehaviour
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
            socket.ToggleSocketRight(obj);
            socket.ToggleSocketCeiling(obj);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            SocketController socket = new SocketController();
            socket.ResetSocketRight(obj);
            socket.ResetSocketCeiling(obj);
        }
    
        private void ToggleSocket(XRBaseInteractable obj)
        {
            SocketController socket = new SocketController();
            socket.ToggleSocketRight(obj);
        }
    }
}