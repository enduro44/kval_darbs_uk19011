using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class HomeController : MonoBehaviour
    {
        private SocketController _socketController;
        private XRSocketInteractor _homeSocket;
        private GameObject _socketVisualOnEmpty;
        private GameObject _socketVisualNotEmpty;

        void Awake()
        {
            _socketController = new SocketController();
            _homeSocket = gameObject.GetComponent<XRSocketInteractor>();
            _homeSocket.selectEntered.AddListener(Entered);
            _homeSocket.selectExited.AddListener(Exited);
            
            _socketVisualOnEmpty = _homeSocket.transform.GetChild(0).gameObject;
            _socketVisualNotEmpty = _homeSocket.transform.GetChild(1).gameObject;
        }
        
        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            
            obj.GetComponent<Room>().controllerID = gameObject.transform.root.gameObject.GetComponent<HomeControllerObject>().controllerID;
            
            ToggleSockets(obj);
            _socketController.ToogleConnectedTag(obj);
            _socketVisualOnEmpty.SetActive(false);
            _socketVisualNotEmpty.SetActive(true);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
            obj.transform.localScale = scaleChange;

            obj.GetComponent<Room>().controllerID = "";
            
            ResetSockets(obj);
            _socketController.ToogleConnectedTag(obj);
            _socketVisualOnEmpty.SetActive(true);
            _socketVisualNotEmpty.SetActive(false);
        }
        
        private void ToggleSockets(XRBaseInteractable obj)
        {
            _socketController.TurnOnSocketLeft(obj);
            _socketController.TurnOnSocketRight(obj);
            _socketController.TurnOnSocketCeiling(obj);
        }
        
        private void ResetSockets(XRBaseInteractable obj)
        {
            _socketController.TurnOffSocketLeft(obj);
            _socketController.TurnOffSocketRight(obj);
            _socketController.TurnOffSocketCeiling(obj);
        }
    }
}
