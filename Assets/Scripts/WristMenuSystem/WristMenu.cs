using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace WristMenuSystem
{
    public class WristMenu : MonoBehaviour
    {
        public GameObject wristUI;
        public GameObject inventoryObject;
        public XRSocketInteractor inventorySocket;
        public Transform spawnPoint;
        private GameObject _objInInventory;
        private GameObject _newRoom;
    
        [Header("Prefabs")]
        [Header("Controller")]
        public GameObject homeControllerPrefab;
        [Header("Rooms")]
        public GameObject largeRoomPrefab;
        public GameObject smallRoomPrefab;
        public GameObject cornerRoomPrefab;
        [Header("Furniture")]
        public GameObject eggStoolPrefab;
    

        public bool activeWristUI = true;

        void Start()
        {
            DisplayWristUI();
            inventorySocket = inventoryObject.GetComponent<XRSocketInteractor>();
            inventorySocket.selectEntered.AddListener(GetObject);
        }

        public void MenuPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DisplayWristUI();
            }
        }

        private void DisplayWristUI()
        {
            if (activeWristUI)
            {
                wristUI.SetActive(false);
                inventorySocket.gameObject.SetActive(false);
                activeWristUI = false;
                return;
            }
            wristUI.SetActive(true);
            inventorySocket.gameObject.SetActive(true);
            activeWristUI = true;
        }

        private void GetObject(SelectEnterEventArgs args)
        {
            XRBaseInteractable interactable = args.interactable;
            _objInInventory = interactable.gameObject;
        }

        public void PlayMode()
        {
        
        }

        public void BuildMode()
        {
        
        }
    
        public void Furnish()
        {
        
        }
    
        public void AddRoom(string type)
        {
            if (inventorySocket.selectTarget != null)
            {
                Destroy(_objInInventory);
            }

            var position = spawnPoint.position;
            _newRoom = type switch
            {
                "SmallRoom" => Instantiate(smallRoomPrefab, position, Quaternion.identity),
                "LargeRoom" => Instantiate(largeRoomPrefab, position, Quaternion.identity),
                "CornerRoom" => Instantiate(cornerRoomPrefab, position, Quaternion.identity),
                _ => _newRoom
            };

            Vector3 scaleChange = new Vector3(0.0001f, 0.0001f, 0.0001f);
            _newRoom.transform.localScale = scaleChange;
        
            _newRoom.transform.position = inventoryObject.transform.position;
            _newRoom.transform.rotation = inventoryObject.transform.rotation;
        }
    
        public void AddBase()
        {
            if (inventorySocket.selectTarget != null)
            {
                Destroy(_objInInventory);
            }
    
            GameObject newBase = Instantiate(homeControllerPrefab, spawnPoint.position, Quaternion.identity);
            newBase.SetActive(false);
            newBase.transform.position = inventoryObject.transform.position;
            newBase.transform.rotation = inventoryObject.transform.rotation;
            newBase.SetActive(true);
        }
    
        public void AddItems()
        {
            if (inventorySocket.selectTarget != null)
            {
                Destroy(_objInInventory);
            }
            GameObject newItem = Instantiate(eggStoolPrefab, transform.position, Quaternion.identity);
            newItem.transform.position = inventoryObject.transform.position;
            newItem.transform.rotation = inventoryObject.transform.rotation;
        }

        // public void SaveGame()
        // {
        //     _gameManager.SaveJsonData(_gameData);
        // }
    }
}
