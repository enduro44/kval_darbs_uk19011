using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class DeleteObjectController : MonoBehaviour
    {
        private GameObject gameObjectRightHand;
        private GameObject gameObjectLeftHand;

        public void SetGameObjectRightHand(SelectEnterEventArgs args)
        {
            gameObjectRightHand = args.interactable.gameObject.transform.root.gameObject;
        }

        public void UnsetGameObjectRightHand(SelectExitEventArgs args)
        {
            gameObjectRightHand = null;
        }

        public void SetGameObjectLeftHand(SelectEnterEventArgs args)
        {
            gameObjectLeftHand = args.interactable.gameObject.transform.root.gameObject;
        }
        
        public void UnsetGameObjectLeftHand(SelectExitEventArgs args)
        {
            gameObjectLeftHand = null;
        }

        public void DeleteObjectsInHands(InputAction.CallbackContext context)
        {
            if (gameObjectLeftHand != null)
            {
                gameObjectLeftHand.GetComponent<XRGrabInteractable>().colliders.Clear();
                Destroy(gameObjectLeftHand);
            }
            if (gameObjectRightHand != null)
            {
                gameObjectRightHand.GetComponent<XRGrabInteractable>().colliders.Clear();
                Destroy(gameObjectRightHand);
            }
        }
    }
}
