﻿using GameManagerData;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class PlayableController
    {
    public static void SetAllPlayablesStatic()
    {
        foreach (var furniture in GameData.Playables)
        {
            furniture.gameObject.isStatic = true;
            furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = 1 << 10;
        }
    }

    public static void SetAllPlayablesNonStatic()
    {
        foreach (var furniture in GameData.Playables)
        {
            furniture.gameObject.isStatic = false;
            furniture.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 7);
        }
    }
    }
}