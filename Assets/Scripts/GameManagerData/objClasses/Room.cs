using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        void Awake()
        {
            GameData.Rooms.Add(this);
        }

        private void OnDestroy()
        {
            GameData.Rooms.Remove(this);
        }
    }
}
