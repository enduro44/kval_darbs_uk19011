using GameManagerData.data;
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
        private GameObject _socketVisual;
        private EmptyActiveSocketData _emptyActiveSocketData;

        private SocketAccessibilityController _socketAccessibilityController;
        private static GameObject _root;
        
        private void Awake()
        {
            _root = gameObject.transform.root.gameObject;
            
            _socketAccessibilityController  = new SocketAccessibilityController();
            _socketController = new SocketController();
            _homeSocket = gameObject.GetComponent<XRSocketInteractor>();
            _homeSocket.selectEntered.AddListener(Entered);
            _homeSocket.selectExited.AddListener(Exited);
            _homeSocket.hoverEntered.AddListener(HoverEntered);
            _homeSocket.hoverExited.AddListener(HoverExited);
            
            _socketVisualOnEmpty = _homeSocket.transform.GetChild(0).gameObject;
            _socketVisual = _socketVisualOnEmpty.transform.GetChild(0).gameObject;

            //Each new controller gets a structure to save all active empty sockets that it has attached to it
            _emptyActiveSocketData = new EmptyActiveSocketData(_root.GetComponent<HomeControllerObject>().controllerID, _homeSocket);
            EmptyActiveSocketController.AddData(_emptyActiveSocketData);
            RoomController.GrabbableRooms.Add(_root);
        }
        
        private void OnDestroy()
        {
            EmptyActiveSocketController.RemoveData(_emptyActiveSocketData);
        }

        private void Entered(SelectEnterEventArgs args)
        {
            if (args.interactable.gameObject.transform.root.gameObject.name == "HomeController(Clone)")
            {
                Debug.Log("Destroying");
                Destroy(_homeSocket.selectTarget.gameObject.transform.root.gameObject);
                return;
            }
            
            _emptyActiveSocketData.isControllerEmpty = false;
            SetControllerNotGrabbable();
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            
            obj.GetComponent<Room>().controllerID = _root.GetComponent<HomeControllerObject>().controllerID;
            
            ToggleSockets(obj);
            _socketController.ToogleConnectedTag(obj);
            _socketVisualOnEmpty.SetActive(false);

            GameObject room = args.interactable.gameObject;
            RoomController.GrabbableRooms.Add(room);
            RoomController.GrabbableRooms.Remove(_root);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            if (args.interactable.gameObject.transform.root.gameObject.name == "HomeController(Clone)")
            {
                return;
            }
            
            _emptyActiveSocketData.isControllerEmpty = true;
            SetControllerGrabbable();
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
            obj.transform.localScale = scaleChange;

            obj.GetComponent<Room>().controllerID = "";
            
            ResetSockets(obj);
            _socketController.ToogleConnectedTag(obj);
            _socketVisualOnEmpty.SetActive(true);

            GameObject room = args.interactable.gameObject;
            RoomController.GrabbableRooms.Remove(room);
            RoomController.GrabbableRooms.Add(_root);
        }
        
        private void HoverEntered(HoverEnterEventArgs args)
        {
            _socketAccessibilityController.ProcessEnterH(args, _socketVisual.GetComponent<MeshFilter>());
        }
        
        private void HoverExited(HoverExitEventArgs args)
        {
            _socketAccessibilityController.ProcessExitH(_socketVisual.GetComponent<MeshFilter>());
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

        public static void SetControllerGrabbable()
        {
            if (_root == null)
            {
                return;
            }
            _root.transform.root.GetComponent<XRGrabInteractable>().interactionLayerMask = (1<<7) | (1<<8);
        }
        
        public static void SetControllerNotGrabbable()
        {
            _root.transform.root.GetComponent<XRGrabInteractable>().interactionLayerMask = 1<<3;
        }
    }
}
