using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Furniture : MonoBehaviour
    {
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
