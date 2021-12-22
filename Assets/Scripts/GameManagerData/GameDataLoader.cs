using Controllers;
using UnityEngine;

namespace GameManagerData
{
    public class GameDataLoader : MonoBehaviour
    {
        void Awake()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.InstantiateLoadedData();
            
            if (!GameManager.IsLoadGame())
            {
                Debug.Log("New game position");
                PlayerController playerController = PlayerController.Instance();
                playerController.SetNewGamePlayerPos();
            }
        }
    }
}
