using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        //TODO: I want to turn on only the sockets which had object in them. Then when everything is rendered
        //turn on rest of the ones which were on beforehand as well
        
        public XRSocketInteractor socketL;
        private bool _isActiveL;
        private bool _hasTargetL;
        public XRSocketInteractor socketR;
        private bool _isActiveR;
        private bool _hasTargetR;
        public XRSocketInteractor socketC;
        private bool _isActiveC;
        private bool _hasTargetC;
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
