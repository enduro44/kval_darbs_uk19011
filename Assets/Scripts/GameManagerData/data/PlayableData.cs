using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData.data
{
    public class PlayableData
    {
        public string type;
        public float[] position;
        public float[] size;
        public float[] rotation;

        public PlayableData(Playable playable)
        {
            type = playable.name;
            
            Transform transform = playable.transform;
            Vector3 furniturePos = transform.position;

            position = new float[]
            {
                furniturePos.x, furniturePos.y, furniturePos.z
            };
            
            Vector3 furnitureSize = transform.lossyScale;
            
            size = new float[]
            {
                furnitureSize.x, furnitureSize.y, furnitureSize.z
            };
            
            Vector3 furnitureRot = transform.eulerAngles;
            
            rotation = new float[]
            {
                furnitureRot.x, furnitureRot.y, furnitureRot.z
            };
        }
    }
}