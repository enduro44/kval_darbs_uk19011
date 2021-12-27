using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerL : MonoBehaviour
    {
        private SocketController _controller;
        private XRSocketInteractor _socketL;
        
        private GameObject _socketVisual;
        private MeshFilter _mesh;
        private bool _canBePlaced = true;
        private SocketAccessibilityController _socketAccessibilityController;
        private GameObject _socketTransform;
        
        void Awake()
        {
            _socketAccessibilityController  = new SocketAccessibilityController();
            _controller = new SocketController();
            _socketL = gameObject.GetComponent<XRSocketInteractor>();
            _socketL.selectEntered.AddListener(Entered);
            _socketL.selectExited.AddListener(Exited);
            _socketL.hoverEntered.AddListener(HoverEntered);
            _socketL.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketL.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();

            _socketTransform = _socketL.transform.parent.gameObject.transform.GetChild(1).gameObject;
        }
    
        private void Entered(SelectEnterEventArgs args)
        {
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.RemoveSocket(controllerID, _socketL);
            
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;
            
            if (!_canBePlaced)
            {
                Destroy(_socketL.selectTarget.gameObject.transform.root.gameObject);
                return;
            }

            TransformPosition(obj);
            
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;

            _controller.ToogleConnectedTag(obj);
            _socketVisual.SetActive(false);
            
            obj.GetComponent<Room>().controllerID = controllerID;
            
            _controller.TurnOnSocketLeft(obj);
            _controller.TurnOnSocketCeiling(obj);
        }

        private void TransformPosition(XRBaseInteractable obj)
        {
            string type = _controller.GetType(obj);
            if (type == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-12.19f, -0.466f, 0.0100003f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            if (type == "CornerRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-17.41f, -0.466f, -0.489f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, -90, 0);
            }

            if (type == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-7.01f, -0.466f, 0.001f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }

        private void Exited(SelectExitEventArgs args)
        {
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.AddSocket(controllerID, _socketL);
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketLeft(obj);
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
