using System;
using Controllers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GameManagerData.objClasses
{
    public class HomeControllerObject : MonoBehaviour
    {
        public string controllerID;
        private bool _isGuidUnique;
        void Awake()
        {
            controllerID = GuidGenerator();
            GameData.HomeControllers.Add(this);
        }
        
        private void OnDestroy()
        {
            GameData.HomeControllers.Remove(this);
            RoomController.GrabbableRooms.Remove(gameObject);
           
        }

        private string GuidGenerator()
        {
            string id = "";
            _isGuidUnique = false;
            while (!_isGuidUnique)
            {
                id = Guid.NewGuid().ToString();
                ValidateGuid(id);
            }
            return id;
        }

        private void ValidateGuid(string id)
        {
            foreach (var t in GameData.HomeControllers)
            {
                if (id == t.controllerID)
                {
                    return;
                }
            }
            _isGuidUnique = true;
        }
    }
}
