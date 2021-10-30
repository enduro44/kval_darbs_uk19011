using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerR : MonoBehaviour
    {
        private SocketController _controller;
        void Awake()
        {
            _controller = new SocketController();
            XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
            socket.selectEntered.AddListener(Entered);
            socket.selectExited.AddListener(Exited);
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            _controller.TurnOnSocketRight(obj);
            _controller.TurnOnSocketCeiling(obj);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            _controller.TurnOffSocketRight(obj);
            _controller.TurnOffSocketCeiling(obj);
        }
    }
}