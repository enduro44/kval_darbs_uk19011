using Controllers;
using UnityEngine;

namespace GameManagerData
{
    public class GameDataLoader : MonoBehaviour
    {
        //Klase nodrošina, ka tad, kad ielādējot saglabātu spēli, pēc ainas ielādes un aktivizēšanās, turpinās
        //ielādēto datu ielāde. Ja spēle ir jauna, dati netiek ielādēti, tiek tikai mainīta spēlētāja pozīcija.
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
