using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerC : MonoBehaviour
    {
        private SocketController _controller;
        void Awake()
        {
            _controller = new SocketController();
            XRSocketInteractor socketC = gameObject.GetComponent<XRSocketInteractor>();
            socketC.selectEntered.AddListener(Entered);
            socketC.selectExited.AddListener(Exited);
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            _controller.TurnOnSocketCeiling(obj);
        }
        
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            _controller.TurnOffSocketCeiling(obj);
        }
    }
}