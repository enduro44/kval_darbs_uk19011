using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Playable : MonoBehaviour
    {
        //Klase nodrošina, ka katrs spēlējamais objekts tiks pievienots tam atbilstošajā aktīvās spēles datu sarakstā, un tiks noņemts, 
        //kad tas tiek iznīcināts
        void Awake()
        {
            GameData.Playables.Add(this);
        }

        private void OnDestroy()
        {
            GameData.Playables.Remove(this);
        }
    }
}