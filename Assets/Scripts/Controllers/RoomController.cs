using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class RoomController : MonoBehaviour
    {
        public XRGrabInteractable grabber;
        private XRSocketInteractor socketL;
        private XRSocketInteractor socketR;
        private XRSocketInteractor socketC;
        private bool hasObjectL;
        private bool hasObjectR;
        private bool hasObjectC;

        void Awake()
        {
            grabber = gameObject.GetComponent<XRGrabInteractable>();
            GameObject box = gameObject.transform.GetChild(0).gameObject;
            GameObject left = box.transform.GetChild(1).gameObject;
            GameObject right = box.transform.GetChild(2).gameObject;
            GameObject ceiling = box.transform.GetChild(3).gameObject;
            socketL = left.GetComponent<XRSocketInteractor>();
            socketR = right.GetComponent<XRSocketInteractor>();
            socketC = ceiling.GetComponent<XRSocketInteractor>();
            socketL.selectEntered.AddListener(EnteredL);
            socketL.selectExited.AddListener(ExitedL);
            socketR.selectEntered.AddListener(EnteredR);
            socketR.selectExited.AddListener(ExitedR);
            socketC.selectEntered.AddListener(EnteredC);
            socketC.selectExited.AddListener(ExitedC);
        }

        private void EnteredL(SelectEnterEventArgs args)
        {
            hasObjectL = true;
            ToggleGrab();
        }
        
        private void ExitedL(SelectExitEventArgs args)
        {
            hasObjectL = false;
            ToggleGrab();
        }
        private void EnteredR(SelectEnterEventArgs args)
        {
            hasObjectR = true;
            ToggleGrab();
        }
        
        private void ExitedR(SelectExitEventArgs args)
        {
            hasObjectR = false;
            ToggleGrab();
        }
        private void EnteredC(SelectEnterEventArgs args)
        {
            hasObjectC = true;
            ToggleGrab();
        }
        
        private void ExitedC(SelectExitEventArgs args)
        {
            hasObjectC = false;
            ToggleGrab();
        }

        private void ToggleGrab()
        {
            if (hasObjectL || hasObjectR || hasObjectC)
            {
                //grabber.selectEntered.AddListener(Stop);
                gameObject.layer = 3;
                gameObject.layer = ~gameObject.layer;
            }
            gameObject.layer = 0;
        }

        private void Stop(SelectEnterEventArgs args)
        {
            // XRBaseInteractable obj = args.interactable;
            // GameObject box = obj.transform.GetChild(0).gameObject;
            // GameObject left = box.transform.GetChild(1).gameObject;
            // GameObject right = box.transform.GetChild(2).gameObject;
            // GameObject ceiling = box.transform.GetChild(3).gameObject;
            // XRSocketInteractor socketLChild = left.GetComponent<XRSocketInteractor>();
            // XRSocketInteractor socketRChild = right.GetComponent<XRSocketInteractor>();
            // XRSocketInteractor socketCChild = ceiling.GetComponent<XRSocketInteractor>();
            // bool socketLChild_isActive = this.socketL.socketActive;
            // bool socketRChild_isActive = this.socketL.socketActive;
            // bool socketCChild_isActive = this.socketL.socketActive;
            // grabber.interactionManager.CancelInteractableSelection(obj);
            // socketLChild.socketActive = socketLChild_isActive;
            // socketRChild.socketActive = socketRChild_isActive;
            // socketCChild.socketActive = socketCChild_isActive;
        }
    }
}

