using UnityEngine;
using GameManagerData.data;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public GameObject playerObject;
        public GameObject xrRig;
        public Vector3 playerPositionGameLoaded = new Vector3(0,0,0);
        public Vector3 playerPositionGameLoading = new Vector3(0,0,-500);

        private void Awake()
        {
            _instance = this;
        }

        public static PlayerController Instance() {
            return _instance;
        }
        
        public void SetNewGamePlayerPos()
        {
            playerObject.transform.position = playerPositionGameLoaded;
        }

        public void SetPlayerPos(PlayerGameData gameData)
        {
            Debug.Log("Load game position");
            Vector3 playerPos = new Vector3(gameData.position[0], gameData.position[1], gameData.position[2]);
            Vector3 playerSize = new Vector3(gameData.size[0], gameData.size[1], gameData.size[2]);
            Vector3 playerRot = new Vector3(gameData.rotation[0], gameData.rotation[1], gameData.rotation[2]);
            Transform playerTransform = playerObject.transform;
            playerTransform.position = playerPos;
            playerTransform.localScale = playerSize;
            playerTransform.eulerAngles = playerRot;
        }

        public PlayerGameData SetPlayerData()
        {
            PlayerGameData playerGameData = new PlayerGameData();

            playerGameData.sceneType = SceneManager.GetActiveScene().name;
            
            Vector3 playerPos = xrRig.transform.position;
            Vector3 playerHeight = playerObject.transform.position;
            
            playerGameData.position = new float[]
            {
                playerPos.x, playerHeight.y, playerPos.z
            };
            
            Vector3 playerSize = xrRig.transform.lossyScale;

            playerGameData.size = new float[]
            {
                playerSize.x, playerSize.y, playerSize.z
            };
            
            Vector3 playerRot = xrRig.transform.eulerAngles;

            playerGameData.rotation = new float[]
            {
                playerRot.x, playerRot.y, playerRot.z
            };
            
            return playerGameData;
        }

        public void IncreasePlayerHeight(InputAction.CallbackContext context)
        {
            if (context.performed)
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
        
        public void DecreasePlayerHeight(InputAction.CallbackContext context)
        {
            if (context.performed)
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
    }
}