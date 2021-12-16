using UnityEngine;
using GameManagerData.data;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public GameObject playerObject;
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

        public void SetPlayerPos(PlayerTransformData data)
        {
            Vector3 playerPos = new Vector3(data.position[0], data.position[1], data.position[2]);
            Vector3 playerSize = new Vector3(data.size[0], data.size[1], data.size[2]);
            Vector3 playerRot = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
            Transform playerTransform = playerObject.transform;
            playerTransform.position = playerPos;
            playerTransform.localScale = playerSize;
            playerTransform.eulerAngles = playerRot;
        }

        public PlayerTransformData SetPlayerData()
        {
            PlayerTransformData playerData = new PlayerTransformData();
            
            Vector3 playerPos = playerObject.transform.position;
            
            playerData.position = new float[]
            {
                playerPos.x, playerPos.y, playerPos.z
            };
            
            Vector3 playerSize = playerObject.transform.lossyScale;

            playerData.size = new float[]
            {
                playerSize.x, playerSize.y, playerSize.z
            };
            
            Vector3 playerRot = playerObject.transform.eulerAngles;

            playerData.rotation = new float[]
            {
                playerRot.x, playerRot.y, playerRot.z
            };

            return playerData;
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