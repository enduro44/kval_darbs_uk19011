using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static GameObject Player;
        public static Vector3 PlayerPositionGameLoaded = new Vector3(0,0,0);
        public static Vector3 PlayerPositionGameLoading = new Vector3(0,0,-500);

        private void Awake()
        {
            Player = gameObject;
        }

        public static void SetPlayerPos()
        {
            Player.transform.position = PlayerPositionGameLoaded;
        }
        
    }
}