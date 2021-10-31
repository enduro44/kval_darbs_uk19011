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
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            _controller.TurnOnSocketRight(obj);
            _controller.TurnOnSocketCeiling(obj);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketRight(obj);
            _controller.TurnOffSocketCeiling(obj);
        }
    }
}