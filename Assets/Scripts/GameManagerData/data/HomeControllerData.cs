using System;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData.data
{
    [Serializable]
    public class HomeControllerData
    {
        public string controlledID;
        public float[] position;
        public float[] rotation;

        public HomeControllerData(HomeControllerObject homeController)
        {
            controlledID = homeController.controllerID;
            
            Transform transform = homeController.transform; 
            
            Vector3 homePos = transform.position;

            position = new float[]
            {
                homePos.x, homePos.y, homePos.z
            };

            Vector3 homeRot = transform.eulerAngles;
            
            rotation = new float[]
            {
                homeRot.x, homeRot.y, homeRot.z
            };
        }
    }
}