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
                PlayerController playerController = PlayerController.Instance();
                playerController.SetNewGamePlayerPos();
            }
        }
    }
}
