using System;
using UnityEngine;

namespace GameManagerData.data
{
    [Serializable]
    public class PlayerGameData
    {
        public string sceneType;
        
        public float[] position;
        public float[] size;
        public float[] rotation;
    }
}