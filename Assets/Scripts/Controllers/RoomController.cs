using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Class controls if the room can be picked up by the player or not
    public class RoomController : MonoBehaviour
    {
        private XRGrabInteractable _grabber;
        private bool _hasObjectL;
        private bool _hasObjectR;
        private bool _hasObjectC;

        void Awake()
        {
            _grabber = gameObject.GetComponent<XRGrabInteractable>();
            GameObject box = gameObject.transform.GetChild(0).gameObject;
            GameObject left = box.transform.GetChild(1).gameObject;
            GameObject right = box.transform.GetChild(2).gameObject;
            GameObject ceiling = box.transform.GetChild(3).gameObject;
            XRSocketInteractor socketL = left.GetComponent<XRSocketInteractor>();
            XRSocketInteractor socketR = right.GetComponent<XRSocketInteractor>();
            XRSocketInteractor socketC = ceiling.GetComponent<XRSocketInteractor>();
            socketL.selectEntered.AddListener(EnteredL);
            socketL.selectExited.AddListener(ExitedL);
            socketR.selectEntered.AddListener(EnteredR);
            socketR.selectExited.AddListener(ExitedR);
            socketC.selectEntered.AddListener(EnteredC);
            socketC.selectExited.AddListener(ExitedC);
        }

        private void EnteredL(SelectEnterEventArgs args)
        {
            _hasObjectL = true;
            ToggleGrab();
        }
        
        private void ExitedL(SelectExitEventArgs args)
        {
            _hasObjectL = false;
            ToggleGrab();
        }
        private void EnteredR(SelectEnterEventArgs args)
        {
            _hasObjectR = true;
            ToggleGrab();
        }
        
        private void ExitedR(SelectExitEventArgs args)
        {
            _hasObjectR = false;
            ToggleGrab();
        }
        private void EnteredC(SelectEnterEventArgs args)
        {
            _hasObjectC = true;
            ToggleGrab();
        }
        
        private void ExitedC(SelectExitEventArgs args)
        {
            _hasObjectC = false;
            ToggleGrab();
        }

        private void ToggleGrab()
        {
            if (_hasObjectL || _hasObjectR || _hasObjectC)
            {
                //Changing the layer to "Socket" only, so the player can't pick up the room
                _grabber.interactionLayerMask = (1 << 6);
                return;
            }
            //Changing the layer back to "Socket" and "Player"
            _grabber.interactionLayerMask = (1<<6) | (1<<7);
        }
    }
}
