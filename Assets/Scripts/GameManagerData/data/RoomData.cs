using System;
using GameManagerData.objClasses;
using UnityEngine;

namespace GameManagerData.data
{
    [Serializable]
    public class RoomData
    {
        //Klase satur istabu saglabājamo datu struktūru un konstruktoru
        public string type;
        public string controllerID;
        public float[] position;
        public float[] size;
        public float[] rotation;

        public RoomData(Room room)
        {
            type = room.name;

            controllerID = room.controllerID;
            
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
