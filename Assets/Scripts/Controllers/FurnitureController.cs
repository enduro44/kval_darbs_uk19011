using GameManagerData;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class FurnitureController
    {
        public static void SetAllFurnitureNotMovable()
        {
            foreach (var furniture in GameData.Furniture)
            {
                furniture.gameObject.isStatic = true;
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
            }
        }
        
        public static void SetAllFurnitureMovable()
        {
            foreach (var furniture in GameData.Furniture)
            {
                furniture.gameObject.isStatic = false;
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
            }
        }
    }
}