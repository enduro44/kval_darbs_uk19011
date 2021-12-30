using GameManagerData;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase, izmantojot GameData.Playables sarakstu spēj padarīt spēlējamos objektus paceļamas vai nepaceļamus spēlētājam
    public class PlayableController
    {
        public static void SetAllPlayablesMovable()
        {
            foreach (var furniture in GameData.Playables)
            {
                furniture.gameObject.isStatic = true;
                //Lai spēlētājs spēlējamo objektu nevarētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs nevar mijiedarboties
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
            }
        }

        public static void SetAllPlayablesNotMovable()
        {
            foreach (var furniture in GameData.Playables)
            {
                furniture.gameObject.isStatic = false;
                //Lai spēlētājs spēlējamo objektu varētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs var mijiedarboties
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
            }
        }
    }
}