using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerL : MonoBehaviour
    {
        private SocketController _controller;
        private XRSocketInteractor _socketL;
        private GameObject _socketVisual;
        void Awake()
        {
            _controller = new SocketController();
            _socketL = gameObject.GetComponent<XRSocketInteractor>();
            _socketL.selectEntered.AddListener(Entered);
            _socketL.selectExited.AddListener(Exited);
            _socketVisual = _socketL.transform.GetChild(0).gameObject;
        }
    
        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            _controller.TurnOnSocketLeft(obj);
            _controller.TurnOnSocketCeiling(obj);
            _socketVisual.SetActive(false);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketLeft(obj);
            _controller.TurnOffSocketCeiling(obj);
            _socketVisual.SetActive(true);
        }
    }
}
