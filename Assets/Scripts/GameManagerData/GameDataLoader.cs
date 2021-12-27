using Controllers;
using UnityEngine;

namespace GameManagerData
{
    public class GameDataLoader : MonoBehaviour
    {
        void Awake()
        {
            PlayerController playerController = PlayerController.Instance();
            playerController.DisableMovementAndRays();
            
            GameManager gameManager = GameManager.Instance();
            gameManager.InstantiateLoadedData();

            if (!GameManager.IsLoadGame())
            {
                playerController.PreparePlayerNewGame();
            }
        }
    }
}
