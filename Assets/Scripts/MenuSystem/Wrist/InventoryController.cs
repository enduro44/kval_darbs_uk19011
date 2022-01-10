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
        //Klase nodrošina inventāra loga loģiku, jaunu objektu izveidošanu, ievietošanu tajā un izmēra koriģēšanu, lai
        //objekts ietilptu inventāra logā. Kā arī tā ļauj aizvērt, atvērt inventāra logu un dzēst objektu, kas ir palicis
        //inventārā
        private static InventoryController _instance;
        private XRSocketInteractor _socketI;
        private SocketController _controller;
        private PrefabData _prefabData;
        private static GameObject _objInInventory;
        private Vector3 _objInInventoryScale;
        
        public static GameObject inventoryObject;
        public Transform spawnPoint;
        private GameObject _inventoryTransform;
        void Awake()
        {
            _instance = this;
            _controller = new SocketController();
            _prefabData = PrefabData.Instance();
            _socketI = gameObject.GetComponent<XRSocketInteractor>();
            _socketI.selectEntered.AddListener(GetObject);
            _socketI.selectExited.AddListener(Exited);

            inventoryObject = gameObject;
            _inventoryTransform = gameObject.transform.GetChild(0).gameObject;
        }
        
        public static InventoryController Instance() {
            return _instance;
        }

        //Funkcija nodrošina spēlētāja izvēlētā objekta izveidi un ievietošanu inventāra logā
        public void InstantiateNewObject(string objectType, string objectCategory)
        {
            //Ja iepriekšējais objekts nav izņemts, tas tiek dzēsts
            if (_socketI.selectTarget != null)
            {
                Destroy(_objInInventory);
            }

            //Tiek iegūta objekta sagatave atkarībā no tā tipa un kategorijas
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
            
            //Tiek izveidots objekts
            GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            //Tiek saglabāts objekta oriģinālais izmērs 
            _objInInventoryScale = newObject.transform.localScale;
            //Objekts tiek padarīts neredzams
            newObject.SetActive(false);
            
            //Ja tips ir jumts, tad tam tiek piekoriģēts inventāra loga transform pozīcija
            if (objectType == "SmallRoof(Clone)" || objectType == "LargeRoof(Clone)" || objectType == "CornerRoof(Clone)")
            {
                _inventoryTransform.transform.localPosition = new Vector3(-0.12f, -0.29f, 0.3f);
            }
            else
            {
                _inventoryTransform.transform.localPosition = new Vector3(0, -0.29f, 0.3f);
            }

            //Objektam tiek mainīts izmērs, tas tiek ievietots inventārā un tiek padarīts redzams
            Vector3 objScale = PrefabData.GetSizeVector3(objectType);
            newObject.transform.position = inventoryObject.transform.position;
            newObject.transform.localScale = objScale;
            newObject.SetActive(true);
        }
        
        //Izejošam objektam tiek atgriezts tā oriģinālais izmērs un noņemta inventāra loga layer mask, lai to nevarētu ielikt
        //atpakaļ inventāra logā
        private void Exited(SelectExitEventArgs args)
        {
            args.interactable.transform.root.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask &= 1<<7;
            _objInInventory.transform.localScale = _objInInventoryScale;
            _objInInventory = null;
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
