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
        private Color _meshColor;
        private Color _meshColorAllowed;

        private GameObject _socketTransform;
        void Awake()
        {
            _controller = new SocketController();
            _socketL = gameObject.GetComponent<XRSocketInteractor>();
            _socketL.selectEntered.AddListener(Entered);
            _socketL.selectExited.AddListener(Exited);
            _socketL.hoverEntered.AddListener(HoverEntered);
            _socketL.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketL.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();
            _meshColor = _mesh.GetComponent<MeshRenderer>().material.color;
            _meshColorAllowed = new Color(0, 204, 102, 0.3f);

            _socketTransform = _socketL.transform.parent.gameObject.transform.GetChild(1).gameObject;
        }
    
        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            
            _controller.TurnOnSocketLeft(obj);
            _controller.TurnOnSocketCeiling(obj);
            
            string type = _controller.GetType(obj);
            if (type == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-12.19f, -0.466f, 0.0100003f);
            }
            
            if (type == "CornerRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-17.41f, -0.466f, -0.489f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, -90, 0);
            }

            if (type == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-7.01f, -0.466f, 0.001f);
            }
            
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
        
        private void HoverEntered(HoverEnterEventArgs args)
        {
            _mesh.GetComponent<Renderer>().material.color = _meshColorAllowed;
        }
        
        private void HoverExited(HoverExitEventArgs args)
        {
            _mesh.GetComponent<MeshRenderer>().material.color = _meshColor;
        }
    }
}
