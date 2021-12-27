using GameManagerData;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class PlayableController
    {
        public static void SetAllPlayablesMovable()
        {
            foreach (var furniture in GameData.Playables)
            {
                furniture.gameObject.isStatic = true;
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
            }
        }

        public static void SetAllPlayablesNotMovable()
        {
            foreach (var furniture in GameData.Playables)
            {
                furniture.gameObject.isStatic = false;
                furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
            }
        }
    }
}