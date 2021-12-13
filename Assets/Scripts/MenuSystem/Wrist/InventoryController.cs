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
        private GameObject _objInInventory;
        
        public GameObject inventoryObject;
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
            //TODO: Might not need exited listener at all? Depends on other objects I will have
            // XRBaseInteractable obj = args.interactable;
            // Vector3 scaleChange = new Vector3(1, 1, 1); 
            // obj.transform.localScale = scaleChange;
        }

        public void InstantiateNewObject(string type)
        {
            if (_socketI.selectTarget != null)
            {
                Destroy(_objInInventory);
            }
            PrefabData prefabData = PrefabData.Instance();
            GameObject prefab = prefabData.GetPrefab(type);
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
    }
}
