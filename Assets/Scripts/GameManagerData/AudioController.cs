using UnityEngine;

namespace GameManagerData
{
    public class AudioController : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
