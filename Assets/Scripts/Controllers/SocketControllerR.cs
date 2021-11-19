using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerR : MonoBehaviour
    {
        private SocketController _controller;
        private XRSocketInteractor _socketR;
        private GameObject _socketVisual;
        void Awake()
        {
            _controller = new SocketController();
            _socketR = gameObject.GetComponent<XRSocketInteractor>();
            _socketR.selectEntered.AddListener(Entered);
            _socketR.selectExited.AddListener(Exited);
            _socketVisual = _socketR.transform.GetChild(0).gameObject;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            _controller.TurnOnSocketRight(obj);
            _controller.TurnOnSocketCeiling(obj);
            _socketVisual.SetActive(false);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketRight(obj);
            _controller.TurnOffSocketCeiling(obj);
            _socketVisual.SetActive(true);
        }
    }
}