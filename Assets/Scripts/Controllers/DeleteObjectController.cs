using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase reģistrē objektus, ko lietotājs ir paņēmis rokās un ļauj tos dzēst ar pogas spiedienu
    public class DeleteObjectController : MonoBehaviour
    {
        private GameObject _gameObjectRightHand;
        private GameObject _gameObjectLeftHand;
        public static bool isPlayGameMode = true;

        public void SetGameObjectRightHand(SelectEnterEventArgs args)
        {
            _gameObjectRightHand = args.interactable.gameObject.transform.root.gameObject;
        }

        public void UnsetGameObjectRightHand(SelectExitEventArgs args)
        {
            _gameObjectRightHand = null;
        }

        public void SetGameObjectLeftHand(SelectEnterEventArgs args)
        {
            _gameObjectLeftHand = args.interactable.gameObject.transform.root.gameObject;
        }
        
        public void UnsetGameObjectLeftHand(SelectExitEventArgs args)
        {
            _gameObjectLeftHand = null;
        }

        //Metodi izsauc kreisā kontroliera sekundārās pogas spiediens
        public void DeleteObjectsInHands(InputAction.CallbackContext context)
        {
            if (isPlayGameMode)
            {
                return;
            }
            
            if (_gameObjectLeftHand != null)
            {
                _gameObjectLeftHand.GetComponent<XRGrabInteractable>().colliders.Clear();
                Destroy(_gameObjectLeftHand);
            }
            
            if (_gameObjectRightHand != null)
            {
                _gameObjectRightHand.GetComponent<XRGrabInteractable>().colliders.Clear();
                Destroy(_gameObjectRightHand);
            }
        }
    }
}
