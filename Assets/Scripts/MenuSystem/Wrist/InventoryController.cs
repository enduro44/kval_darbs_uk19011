using Controllers;
using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MenuSystem.Wrist
{
    public class InventoryController : MonoBehaviour
    {
        private static InventoryController _instance;
        private XRSocketInteractor _socketI;
        private SocketController _controller;
        private PrefabData _prefabData;
        private static GameObject _objInInventory;
        
        public static GameObject inventoryObject;
        public Transform spawnPoint;
        void Awake()
        {
            
            _instance = this;
            _controller = new SocketController();
            _prefabData = PrefabData.Instance();
            _socketI = gameObject.GetComponent<XRSocketInteractor>();
            _socketI.selectEntered.AddListener(Entered);
            _socketI.selectEntered.AddListener(GetObject);
            _socketI.selectExited.AddListener(Exited);
            
            inventoryObject = gameObject;
        }
        
        public static InventoryController Instance() {
            return _instance;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            string typeOfObjectInSocket = _controller.GetType(obj);

            Vector3 inventoryScale = inventoryObject.transform.lossyScale;
            Vector3 objInSocketScale = args.interactable.gameObject.transform.lossyScale;
            //TODO: jāizdomā change scale loģika
            
            if (typeOfObjectInSocket == "CornerRoom(Clone)" || typeOfObjectInSocket == "LargeRoom(Clone)" || typeOfObjectInSocket == "SmallRoom(Clone)" || _controller.IsRoof(obj))
            {
                Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
                obj.transform.localScale = scaleChange;
            }
        }

        private void Exited(SelectExitEventArgs args)
        {
            //args.interactable.transform.root.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask &= ~(1<<11);
            _objInInventory = null;
        }

        public void InstantiateNewObject(string objectType, string objectCategory)
        {
            if (_socketI.selectTarget != null)
            {
                Destroy(_objInInventory);
            }

            GameObject prefab;
            switch (objectCategory)
            {
                case "home":
                    prefab = _prefabData.GetPrefab(objectType);
                    break;
                case "furniture":
                    prefab = _prefabData.GetFurniturePrefab(objectType);
                    break;
                case "playable":
                    prefab = _prefabData.GetPlayablePrefab(objectType);
                    break;
                default:
                    Debug.Log("Category does not exist");
                    return;
            }
            
            GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            newObject.SetActive(false);
            newObject.transform.rotation = inventoryObject.transform.rotation;
            newObject.transform.position = inventoryObject.transform.position;
            newObject.SetActive(true);
        }
        
        private void GetObject(SelectEnterEventArgs args)
        {
            XRBaseInteractable interactable = args.interactable;
            _objInInventory = interactable.gameObject;
        }
        
        public static void HideInventory()
        {
            if (!_objInInventory)
            {
                Destroy(_objInInventory);
            }
            inventoryObject.SetActive(false);
        }
        
        public static void ShowInventory()
        {
            inventoryObject.SetActive(true);
        }
    }
}
