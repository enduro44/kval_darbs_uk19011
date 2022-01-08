using Controllers;
using UnityEngine;

namespace GameManagerData
{
    public class GameDataLoader : MonoBehaviour
    {
        void Awake()
        {
            
            GameManager gameManager = GameManager.Instance();
            PlayerController playerController = PlayerController.Instance();
            
            if (!GameManager.IsLoadGame())
            {
                playerController.PreparePlayerNewGame();
                return;
            }
            
            playerController.DisableMovementAndRays();
            gameManager.InstantiateLoadedData();
        }
    }
}
