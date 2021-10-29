using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerC : MonoBehaviour
    {
        private XRSocketInteractor _socketC;
        private XRGrabInteractable obj1;
        private XRGrabInteractable obj2;
        // private XRSocketInteractor _socketL;
        // private XRSocketInteractor _socketR;
        
        void Awake()
        {
            //Ceiling 
            _socketC = gameObject.GetComponent<XRSocketInteractor>();
            _socketC.selectEntered.AddListener(Entered);
            _socketC.selectExited.AddListener(Exited);
            
            // //Parent game object
            // GameObject parentObj = transform.parent.gameObject;
            //
            // //Left wall
            // GameObject parentBoxWallLeft = parentObj.transform.GetChild(1).gameObject;
            // _socketL = parentBoxWallLeft.GetComponent<XRSocketInteractor>();
            //
            // _socketL.selectEntered.AddListener(OnEnterLeft);
            // _socketL.selectExited.AddListener(OnExitLeft);
            //
            // //Right wall
            // GameObject parentBoxWallRight = parentObj.transform.GetChild(2).gameObject;
            // _socketR = parentBoxWallRight.GetComponent<XRSocketInteractor>();
            //
            // _socketR.selectEntered.AddListener(OnEnterRight);
            // _socketR.selectExited.AddListener(OnExitRight);
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            SocketController socket = new SocketController();
            socket.ToggleSocketCeiling(obj);
            // if (_socketL.selectTarget)
            // {
            //     ToggleLeftSocket(obj);
            // }
            //
            // if (_socketR.selectTarget)
            // {
            //     ToggleRightSocket(obj);
            // }
        }
        
        private void Exited(SelectExitEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            SocketController sc = new SocketController();
            sc.ResetSocketCeiling(obj);
            // sc.ResetSocketLeft(obj);
            // sc.ResetSocketRight(obj);
        }
        
        
    //     private void OnEnterLeft(SelectEnterEventArgs args)
    //     {
    //         if (!_socketC.selectTarget) return;
    //         XRBaseInteractable obj = _socketC.selectTarget;
    //         SocketController targetSocket = new SocketController();
    //         targetSocket.ToggleSocketLeft(obj);
    //     }
    //     
    //     private void OnExitLeft(SelectExitEventArgs args)
    //     {
    //         if (!_socketC.selectTarget) return;
    //         XRBaseInteractable obj = _socketC.selectTarget;
    //         SocketController targetSocket = new SocketController();
    //         targetSocket.ToggleSocketLeft(obj);
    //     }
    //     
    //     private void ToggleLeftSocket(XRBaseInteractable obj)
    //     {
    //         SocketController socket = new SocketController();
    //         socket.ToggleSocketLeft(obj);
    //     }
    //     
    //     private void OnEnterRight(SelectEnterEventArgs args)
    //     {
    //         if (!_socketC.selectTarget) return;
    //         XRBaseInteractable obj = _socketC.selectTarget;
    //         SocketController targetSocket = new SocketController();
    //         targetSocket.ToggleSocketRight(obj);
    //     }
    //     
    //     private void OnExitRight(SelectExitEventArgs args)
    //     {
    //         if (!_socketC.selectTarget) return;
    //         XRBaseInteractable obj = _socketC.selectTarget;
    //         SocketController targetSocket = new SocketController();
    //         targetSocket.ToggleSocketRight(obj);
    //     }
    //
    //     private void ToggleRightSocket(XRBaseInteractable obj)
    //     {
    //         SocketController socket = new SocketController();
    //         socket.ToggleSocketRight(obj);
    //     }
    }
}