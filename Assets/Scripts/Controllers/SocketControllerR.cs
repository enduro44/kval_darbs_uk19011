using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerR : MonoBehaviour
    {
        void Start()
        {
            XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
            socket.selectEntered.AddListener(Entered);
            socket.selectExited.AddListener(Exited);
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            ToggleSocket(obj);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            ToggleSocket(obj);
        }
    
        private void ToggleSocket(XRBaseInteractable obj)
        {
            SocketController socket = new SocketController();
            socket.ToggleSocketLeft(obj);
        }
    }
}