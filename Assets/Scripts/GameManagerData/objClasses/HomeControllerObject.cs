using UnityEngine;

namespace GameManagerData.objClasses
{
    public class HomeControllerObject : MonoBehaviour
    {
        void Awake()
        {
            GameData.HomeControllers.Add(this);
        }
        
        private void OnDestroy()
        {
            GameData.HomeControllers.Remove(this);
        }
    }
}
