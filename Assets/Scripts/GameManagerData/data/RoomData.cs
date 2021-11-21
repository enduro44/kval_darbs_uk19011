using System;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData.data
{
    [Serializable]
    public class RoomData
    {
        public string type;
        public float[] position;
        public float[] size;
        public float[] rotation;

        public RoomData(Room room)
        {
            type = room.name;
            
            Transform transform = room.transform;
            Vector3 roomPos = transform.position;

            position = new float[]
            {
                roomPos.x, roomPos.y, roomPos.z
            };

            Vector3 roomSize = transform.lossyScale;
            
            size = new float[]
            {
                roomSize.x, roomSize.y, roomSize.z
            };
            
            Vector3 roomRot = transform.eulerAngles;
            
            rotation = new float[]
            {
                roomRot.x, roomRot.y, roomRot.z
            };
        }
    }
}
