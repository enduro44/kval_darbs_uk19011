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
        private Color _meshColor;
        private Color _meshColorAllowed;
        private Color _meshColorDanger;
        
        private GameObject _socketTransform;
        private GameObject _root;
        void Awake()
        {
            _controller = new SocketController();
            _socketC = gameObject.GetComponent<XRSocketInteractor>();
            _socketC.selectEntered.AddListener(Entered);
            _socketC.selectExited.AddListener(Exited);
            _socketC.hoverEntered.AddListener(HoverEntered);
            _socketC.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketC.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();
            _meshColor = _mesh.GetComponent<MeshRenderer>().material.color;
            _meshColorAllowed = new Color(0, 204, 102, 0.3f);
            _meshColorDanger = new Color(255, 0, 0, 0.3f);
            
            _socketTransform = _socketC.transform.parent.gameObject.transform.GetChild(1).gameObject;

            _root = _socketC.transform.root.gameObject;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;

            string typeOfObjectInSocket = _controller.GetType(obj);
            string typeOfRootObject = _root.name;

            if (typeOfRootObject != typeOfObjectInSocket)
            {
                Destroy(_socketC.selectTarget.gameObject.transform.root.gameObject);
                Debug.Log("Can't place this here");
                return;
            }

            if (typeOfObjectInSocket == "CornerRoom(Clone)" || typeOfObjectInSocket == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(0f, 1f, 0f);
            }

            if (typeOfObjectInSocket == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.528f, 0.032f, 0.486f);
            }

            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            _controller.TurnOnSocketCeiling(obj);
            
            _socketVisual.SetActive(false);
        }

        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketCeiling(obj);
            _socketVisual.SetActive(true);
        }

        private void HoverEntered(HoverEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;

            string typeOfObjectInSocket = _controller.GetType(obj);
            string typeOfRootObject = _root.name;

            if (typeOfRootObject != typeOfObjectInSocket)
            {
                _mesh.GetComponent<Renderer>().material.color = _meshColorDanger;
            }
            else
            {
                _mesh.GetComponent<Renderer>().material.color = _meshColorAllowed;
            }
        }
        
        private void HoverExited(HoverExitEventArgs args)
        {
            _mesh.GetComponent<MeshRenderer>().material.color = _meshColor;
        }
    }
}