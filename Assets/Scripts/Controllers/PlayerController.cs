using System;
using UnityEngine;
using GameManagerData.data;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase pārvalda spēlētāja pozīciju spēles ainā, spēlētāja iespēju mijiedarboties ar spēles ainu un spēlētāja augstuma maiņas funkcionalitāti
    //Kā arī klase sagatavo datus par spēlētāja pozīciju ainā brīdī, kad spēle tiek saglabāta
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public GameObject playerObject;
        public GameObject xrRig;
        private bool _heightChangeAllowed = false;
        
        public GameObject locomotionSystem;
        public GameObject leftHand;
        public GameObject rightHand;
        
        public Vector3 playerPositionGameLoaded = new Vector3(0,0,0);
        public Vector3 playerPositionGameLoading = new Vector3(0,0,-500);

        private void Awake()
        {
            _instance = this;
        }

        public static PlayerController Instance() {
            return _instance;
        }
        
        //Metode sagatavo spēlētāju jaunas spēles ainā
        public void PreparePlayerNewGame()
        {
            playerObject.transform.position = playerPositionGameLoaded;
            EnablePlayerMovement();
            EnableRaysBothHands();
            _heightChangeAllowed = true;
        }

        //Metode sagatavo spēlētāju ielādētas spēles ainā
        public void PreparePlayerLoadGame(PlayerData data)
        {
            Vector3 playerPos = new Vector3(data.position[0], data.position[1], data.position[2]);
            Vector3 playerSize = new Vector3(data.size[0], data.size[1], data.size[2]);
            Vector3 playerRot = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
            Transform playerTransform = playerObject.transform;
            playerTransform.position = playerPos;
            playerTransform.localScale = playerSize;
            playerTransform.eulerAngles = playerRot;
            EnablePlayerMovement();
            EnableRaysBothHands();
            _heightChangeAllowed = true;
        }

        //Metode sagatavo spēlētāja datus brīdī, kad spēle tiek saglabāta
        public PlayerData SetPlayerData()
        {
            PlayerData playerData = new PlayerData();

            playerData.sceneType = SceneManager.GetActiveScene().name;
            
            Vector3 playerPos = xrRig.transform.position;
            Vector3 playerHeight = playerObject.transform.position;
            
            playerData.position = new float[]
            {
                playerPos.x, playerHeight.y, playerPos.z
            };
            
            Vector3 playerSize = xrRig.transform.lossyScale;

            playerData.size = new float[]
            {
                playerSize.x, playerSize.y, playerSize.z
            };
            
            Vector3 playerRot = xrRig.transform.eulerAngles;

            playerData.rotation = new float[]
            {
                playerRot.x, playerRot.y, playerRot.z
            };
            
            return playerData;
        }

        //Metodi izsauc labā kontroliera sekundārās pogas spiediens
        public void IncreasePlayerHeight(InputAction.CallbackContext context)
        {
            if (context.performed && _heightChangeAllowed)
            {
                Transform playerTransform = playerObject.transform;
                Vector3 playerPosition = playerObject.transform.position;
                float playerHeight = playerTransform.position.y;
                if (playerHeight <= 3)
                {
                    playerPosition.y = playerHeight + 0.2f;
                    playerTransform.position = playerPosition;
                }
            }
        }
        
        //Metodi izsauc labā kontroliera primārās pogas spiediens
        public void DecreasePlayerHeight(InputAction.CallbackContext context)
        {
            if (context.performed && _heightChangeAllowed)
            {
                Transform playerTransform = playerObject.transform;
                Vector3 playerPosition = playerObject.transform.position;
                float playerHeight = playerTransform.position.y;
                if (playerHeight >= -1)
                {
                    playerPosition.y = playerHeight - 0.2f;
                    playerTransform.position = playerPosition;
                }
            }
        }

        public void DisablePlayerMovement()
        {
            locomotionSystem.SetActive(false);
            _heightChangeAllowed = false;
        }

        public void EnablePlayerMovement()
        {
            locomotionSystem.SetActive(true);
            _heightChangeAllowed = true;
        }

        public void EnableRayRightHand()
        {
            rightHand.GetComponent<XRRayInteractor>().enabled = true;
        }
        
        public void DisableRayRightHand()
        {
            rightHand.GetComponent<XRRayInteractor>().enabled = false;
        }
        
        public void EnableRayLeftHand()
        {
            leftHand.GetComponent<XRRayInteractor>().enabled = true;
        }
        
        public void DisableRayLeftHand()
        {
            Debug.Log("turning leftoff");
            leftHand.GetComponent<XRRayInteractor>().enabled = false;
        }

        public void EnableRaysBothHands()
        {
            EnableRayLeftHand();
            EnableRayRightHand();
        }
        
        public void DisableRaysBothHands()
        {
            DisableRayLeftHand();
            DisableRayRightHand();
        }

        public void EnableMovementAndRays()
        {
            EnablePlayerMovement();
            EnableRaysBothHands();
        }
        
        public void DisableMovementAndRays()
        {
            DisablePlayerMovement();
            DisableRaysBothHands();
        }
        
        //Metode nodrošina, ka spēlētājs nevar iziet no spēles ainas spēlējamās zonas, ja spēlētājs mēģina to darīt, 
        //tad tā pozīcija tiek pārlikta atpakaļ uz ielādētas jaunas spēles pozīciju(ainas centrā).
        public void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Barrier"))
            {
                playerObject.transform.position = playerPositionGameLoaded;
                xrRig.transform.position = playerPositionGameLoaded;
            }
        }
    }
}