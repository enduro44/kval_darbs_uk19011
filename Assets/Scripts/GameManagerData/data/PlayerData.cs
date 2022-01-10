using System;
using UnityEngine;

namespace GameManagerData.data
{
   [Serializable]
   public class PlayerData 
   {
      //Klase satur spēlētāja saglabājamo datu struktūru
      [SerializeField] public static string GameID { get; set; }
      public string sceneType;
        
      public float[] position;
      public float[] size;
      public float[] rotation;
   }
}
