using System;
using UnityEngine;

namespace GameManagerData.objClasses
{
    public class HomeControllerObject : MonoBehaviour
    {
        public int ID = 0;
        void Awake()
        {
            GameData.HomeControllers.Add(this);
            ID++;
        }
        
        private void OnDestroy()
        {
            GameData.HomeControllers.Remove(this);
        }
    }
}
