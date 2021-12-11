using System;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData.data
{
    [Serializable]
    public class FurnitureData
    {
        public string type;
        public float[] position;
        public float[] size;
        public float[] rotation;

        public FurnitureData(Furniture furniture)
        {
            Transform transform = furniture.transform;
            Vector3 furniturePos = transform.position;

            position = new float[]
            {
                furniturePos.x, furniturePos.y, furniturePos.z
            };

            //TODO: Decide if size is needed?
            // Vector3 furnitureSize = transform.lossyScale;
            //
            // size = new float[]
            // {
            //     furnitureSize.x, furnitureSize.y, furnitureSize.z
            // };
            
            Vector3 furnitureRot = transform.eulerAngles;
            
            rotation = new float[]
            {
                furnitureRot.x, furnitureRot.y, furnitureRot.z
            };
        }
    }
}