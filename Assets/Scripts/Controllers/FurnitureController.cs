using GameManagerData;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase, izmantojot GameData.Furniture sarakstu spēj padarīt mēbeles paceļamas vai nepaceļamas spēlētājam
    public class FurnitureController
    {
        public static void SetAllFurnitureNotMovable()
        {
            foreach (var furniture in GameData.Furniture)
            {
                furniture.gameObject.isStatic = true;
                //Lai spēlētājs mēbeles objektu nevarētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs nevar mijiedarboties
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
            }
        }
        
        public static void SetAllFurnitureMovable()
        {
            foreach (var furniture in GameData.Furniture)
            {
                furniture.gameObject.isStatic = false;
                //Lai spēlētājs mēbeles objektu varētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs var mijiedarboties
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
            }
        }
    }
}