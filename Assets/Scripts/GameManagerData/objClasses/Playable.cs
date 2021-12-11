using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Playable : MonoBehaviour
    {
        void Awake()
        {
            GameData.Playables.Add(this);
        }

        private void OnDestroy()
        {
            GameData.Playables.Remove(this);
        }
    }
}