using UnityEngine;

namespace GameManagerData
{
    public class GameStarter : MonoBehaviour
    {
        //Klases nozīme ir eksistēm pirmajā spēles ainā, kuru spēlētājs neredz, lai nodrošinātu, ka neiznīcināmajām
        //klasēm visas spēles laikā ir tikai viena instance
        void Start()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadNewScene("MainMenu");
        }
    }
}
