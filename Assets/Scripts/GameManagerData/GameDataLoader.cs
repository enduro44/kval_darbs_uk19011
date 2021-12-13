using Controllers;
using UnityEngine;

namespace GameManagerData
{
    public class GameDataLoader : MonoBehaviour
    {
        void Awake()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadGameData();
            
            if (!GameManager.IsLoadGame())
            {
                Debug.Log("Placing player to 0 0 0");
                PlayerController playerController = PlayerController.Instance();
                playerController.SetNewGamePlayerPos();
            }
        }
    }
}
