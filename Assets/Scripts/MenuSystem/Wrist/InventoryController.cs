using System.Numerics;
using Controllers;
using GameManagerData.data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace MenuSystem.Wrist
{
    public class InventoryController : MonoBehaviour
    {
        private static InventoryController _instance;
        private XRSocketInteractor _socketI;
        private SocketController _controller;
        private PrefabData _prefabData;
        private static GameObject _objInInventory;
        private Vector3 _objInInventoryScale;
        
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
            Vector3 objScale = PrefabData.GetSizeVector3(typeOfObjectInSocket);
            obj.transform.localScale = objScale;
        }

        private void Exited(SelectExitEventArgs args)
        {
            args.interactable.transform.root.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask &= 1<<7;
            _objInInventory.transform.localScale = _objInInventoryScale;
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
            _objInInventoryScale = newObject.transform.localScale;
            newObject.SetActive(false);

            Vector3 objScale = PrefabData.GetSizeVector3(objectType);
            //newObject.transform.rotation = inventoryObject.transform.rotation;
            newObject.transform.position = inventoryObject.transform.position;
            newObject.transform.localScale = objScale;
            newObject.SetActive(true);
        }
        
        private void GetObject(SelectEnterEventArgs args)
        {
            XRBaseInteractable interactable = args.interactable;
            _objInInventory = interactable.gameObject;
        }
        
        public static void HideInventory()
        {
            if (_objInInventory != null)
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
