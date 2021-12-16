using GameManagerData;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class FurnitureController
    {
        public static void SetAllFurnitureStatic()
        {
            foreach (var furniture in GameData.Furniture)
            {
                furniture.gameObject.isStatic = true;
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
            }
        }
        
        public static void SetAllFurnitureNonStatic()
        {
            foreach (var furniture in GameData.Furniture)
            {
                furniture.gameObject.isStatic = false;
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
            }
        }
    }
}