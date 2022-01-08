using GameManagerData;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase, izmantojot GameData.Playables sarakstu spēj padarīt spēlējamos objektus paceļamas vai nepaceļamus spēlētājam
    public class PlayableController
    {
        public static void SetAllPlayablesMovable()
        {
            foreach (var playable in GameData.Playables)
            {
                playable.gameObject.isStatic = false;
                //Lai spēlētājs spēlējamo objektu varētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs var mijiedarboties
                playable.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
            }
        }

        public static void SetAllPlayablesNotMovable()
        {
            foreach (var playable in GameData.Playables)
            {
                playable.gameObject.isStatic = true;
                //Lai spēlētājs spēlējamo objektu nevarētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs nevar mijiedarboties
                playable.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
            }
        }
    }
}