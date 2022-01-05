using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerC : MonoBehaviour
    {
        private SocketController _controller;
        private XRSocketInteractor _socketC;
        
        private GameObject _socketVisual;
        private MeshFilter _mesh;
        private SocketAccessibilityController _socketAccessibilityController;
        private GameObject _socketTransform;
        
        private GameObject _root;
        
        void Awake()
        {
            _socketAccessibilityController  = new SocketAccessibilityController();
            _controller = new SocketController();
            _socketC = gameObject.GetComponent<XRSocketInteractor>();
            _socketC.selectEntered.AddListener(Entered);
            _socketC.selectExited.AddListener(Exited);
            _socketC.hoverEntered.AddListener(HoverEntered);
            _socketC.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketC.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();

            Transform transformC = _socketC.transform;
            _socketTransform = transformC.parent.gameObject.transform.GetChild(1).gameObject;
            _root = transformC.root.gameObject;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.RemoveSocket(controllerID, _socketC);
            
            XRBaseInteractable obj = args.interactable;

            string typeOfObjectInSocket = _controller.GetType(obj);
            string typeOfRootObject = _root.name;

            if (typeOfRootObject != typeOfObjectInSocket && !_controller.IsRoof(obj))
            {
                Destroy(_socketC.selectTarget.gameObject.transform.root.gameObject);
                return;
            }

            TransformPosition(typeOfObjectInSocket);

            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            
            obj.GetComponent<Room>().controllerID = controllerID;
            
            _socketVisual.SetActive(false);
            
            if (_controller.IsRoof(obj))
            {
                return;
            }
            
            _controller.TurnOnSocketCeiling(obj);
        }

        private void TransformPosition(string typeOfObjectInSocket)
        {
            if (typeOfObjectInSocket == "CornerRoom(Clone)" || typeOfObjectInSocket == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(0f, 1f, 0f);
            }

            if (typeOfObjectInSocket == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.515f, 0.032f, 0.486f);
            }

            if (typeOfObjectInSocket == "SmallRoof(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-1.103f, -0.006f, 1.094f);
            }
            
            if (typeOfObjectInSocket == "LargeRoof(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.545f, 0.41f, 0.58f);
            }
            
            if (typeOfObjectInSocket == "CornerRoof(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.28f, 0.39f, 0.186f);
            }
        }

        private void Exited(SelectExitEventArgs args)
        {
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.AddSocket(controllerID, _socketC);
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            obj.GetComponent<Room>().controllerID = "";
            
            _socketVisual.SetActive(true);
            
            if (_controller.IsRoof(obj))
            {
                return;
            }
            
            _controller.TurnOffSocketCeiling(obj);
        }

        private void HoverEntered(HoverEnterEventArgs args)
        {
            _socketAccessibilityController.ProcessEnterC(args, _controller, _root, _mesh);
        }
        
        private void HoverExited(HoverExitEventArgs args)
        {
            _socketAccessibilityController.ProcessExitC(_mesh);
        }
    }
}