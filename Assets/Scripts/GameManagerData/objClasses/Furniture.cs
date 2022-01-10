using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Furniture : MonoBehaviour
    {
        //Klase nodrošina, ka katrs mēbeles objekts tiks pievienots tam atbilstošajā aktīvās spēles datu sarakstā, un tiks noņemts, 
        //kad tas tiek iznīcināts
        void Awake()
        {
            GameData.Furniture.Add(this);
        }

        private void OnDestroy()
        {
            GameData.Furniture.Remove(this);
        }
    }
}
