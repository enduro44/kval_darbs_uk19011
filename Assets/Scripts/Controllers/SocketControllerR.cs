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
        private Color _meshColor;
        private Color _meshColorAllowed;
        private Color _meshColorDanger;
        private bool _canBePlaced = true;

        private GameObject _socketTransform;
        void Awake()
        {
            _controller = new SocketController();
            _socketR = gameObject.GetComponent<XRSocketInteractor>();
            _socketR.selectEntered.AddListener(Entered);
            _socketR.selectExited.AddListener(Exited);
            _socketR.hoverEntered.AddListener(HoverEntered);
            _socketR.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketR.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();
            _meshColor = _mesh.GetComponent<MeshRenderer>().material.color;
            _meshColorAllowed = new Color(0, 204, 102, 0.3f);
            _meshColorDanger = new Color(255, 0, 0, 0.3f);

            _socketTransform = _socketR.transform.parent.gameObject.transform.GetChild(1).gameObject;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;
            if (!_canBePlaced)
            {
                Destroy(_socketR.selectTarget.gameObject.transform.root.gameObject);
                Debug.Log("Can't place this here");
                return;
            }
            
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            
            _controller.TurnOnSocketRight(obj);
            _controller.TurnOnSocketCeiling(obj);

            string type = _controller.GetType(obj);
            if (type == "CornerRoom(Clone)" || type == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(12.19f, -0.466f, 0.0100003f);
            }

            if (type == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(7.01f, -0.466f, 0.001f);
            }
            
            _controller.ToogleConnectedTag(obj);
            _socketVisual.SetActive(false);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            _controller.TurnOffSocketRight(obj);
            _controller.TurnOffSocketCeiling(obj);
            _controller.ToogleConnectedTag(obj);
            _socketVisual.SetActive(true);
        }
        
        private void HoverEntered(HoverEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;
            if (rootObj.CompareTag("Connected") || !_canBePlaced)
            {
                _mesh.GetComponent<MeshRenderer>().material.color = _meshColorDanger;
                _canBePlaced = false;
            }
            else if((rootObj.CompareTag("Disconnected") || rootObj.CompareTag("Untagged")) && _canBePlaced)
            {
                _mesh.GetComponent<Renderer>().material.color = _meshColorAllowed;
            }
        }
        
        private void HoverExited(HoverExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;
                       
            if (rootObj.CompareTag("Disconnected"))
            {
                _canBePlaced = true;
                _mesh.GetComponent<Renderer>().material.color = _meshColor;
            }
        }
    }
}