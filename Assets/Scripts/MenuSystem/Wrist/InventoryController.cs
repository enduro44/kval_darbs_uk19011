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
        private static GameObject _objInInventory;
        
        public static GameObject inventoryObject;
        public Transform spawnPoint;
        private GameObject _newRoom;
        void Awake()
        {
            _instance = this;
            _controller = new SocketController();
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

            if (typeOfObjectInSocket == "CornerRoom(Clone)" || typeOfObjectInSocket == "LargeRoom(Clone)" || typeOfObjectInSocket == "SmallRoom(Clone)")
            {
                Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
                obj.transform.localScale = scaleChange;
            }
        }

        private void Exited(SelectExitEventArgs args)
        {
            _objInInventory = null;
        }

        public void InstantiateNewObject(string objectType, string objectCategory)
        {
            if (_socketI.selectTarget != null)
            {
                Destroy(_objInInventory);
            }
            PrefabData prefabData = PrefabData.Instance();
            
            GameObject prefab = new GameObject();
            switch (objectCategory)
            {
                case "home":
                    prefab = prefabData.GetPrefab(objectType);
                    break;
                case "furniture":
                    prefab = prefabData.GetFurniturePrefab(objectType);
                    break;
                case "playable":
                    prefab = prefabData.GetPlayablePrefab(objectType);
                    break;
                default:
                    Debug.Log("Category does not exist");
                    return;
            }
            
            GameObject newBase = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            newBase.SetActive(false);
            newBase.transform.position = inventoryObject.transform.position;
            newBase.transform.rotation = inventoryObject.transform.rotation;
            newBase.SetActive(true);
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
