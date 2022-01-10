using UnityEngine;

namespace GameManagerData
{
    public class AudioController : MonoBehaviour
    {
        //Klase nodrošina mūzikas objekta neiznīcināšanu, mainot spēles ainas
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
