using UnityEngine;

namespace GameManagerData
{
    public class GameStarter : MonoBehaviour
    {
        void Start()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadNewScene("MainMenu");
        }
    }
}
