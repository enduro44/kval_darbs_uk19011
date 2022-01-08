using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerR : MonoBehaviour
    {
        private SocketController _controller;
        private XRSocketInteractor _socketR;
        
        private GameObject _socketVisual;
        private MeshFilter _mesh;
        private bool _canBePlaced = true;
        private SocketAccessibilityController _socketAccessibilityController;
        private GameObject _socketTransform;
        
        void Awake()
        {
            _socketAccessibilityController  = new SocketAccessibilityController();
            _controller = new SocketController();
            _socketR = gameObject.GetComponent<XRSocketInteractor>();
            _socketR.selectEntered.AddListener(Entered);
            _socketR.selectExited.AddListener(Exited);
            _socketR.hoverEntered.AddListener(HoverEntered);
            _socketR.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketR.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();

            _socketTransform = _socketR.transform.parent.gameObject.transform.GetChild(1).gameObject;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            if (!_canBePlaced)
            {
                Destroy(_socketR.selectTarget.gameObject.transform.root.gameObject);
                return;
            }
            
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.RemoveSocket(controllerID, _socketR);
            
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;
            
            TransformPosition(obj);
            
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;

            _controller.ToogleConnectedTag(obj);
            _socketVisual.SetActive(false);
            
            obj.GetComponent<Room>().controllerID = controllerID;
            
            _controller.TurnOnSocketRight(obj);
            _controller.TurnOnSocketCeiling(obj);
        }

        private void TransformPosition(XRBaseInteractable obj)
        {
            string type = _controller.GetType(obj);
            if (type == "CornerRoom(Clone)" || type == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(12.19f, -0.466f, 0.0100003f);
            }

            if (type == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(7.01f, -0.466f, 0.001f);
            }
        }

        private void Exited(SelectExitEventArgs args)
        {
            if (!_canBePlaced)
            {
                return;
            }
            
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.AddSocket(controllerID, _socketR);
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketRight(obj);
            _controller.TurnOffSocketCeiling(obj);
            _controller.ToogleConnectedTag(obj);
            _socketVisual.SetActive(true);
            obj.GetComponent<Room>().controllerID = "";
        }
        
        private void HoverEntered(HoverEnterEventArgs args)
        {
            _canBePlaced = _socketAccessibilityController.ProcessEnterLR(args, _mesh, _canBePlaced);
        }

        private void HoverExited(HoverExitEventArgs args)
        {
            _canBePlaced = _socketAccessibilityController.ProcessExitLR(_mesh);
        }
    }
}