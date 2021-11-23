using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        //TODO: I want to turn on only the sockets which had object in them. Then when everything is rendered
        //turn on rest of the ones which were on beforehand as well
        //TODO: NO, limit controllers + add ids, that are passed from room to room. Then create lists with rooms. Instantiate controller
        //then its rooms in order. Turn off free active sockets or do a check when placing a room for controller ID. If it doesnt match, turn off the wrong socket, the room should snap in to 
        //the right one on its own, then turn the wrong socket on again (should think which solution is better, easier,more efficient). 
        
        //The home controller block over another room might be solved by this, if not, then a collider box should be added that desnt allow adding rooms inside first room. However, Im not sure how this happened, should have worked...
        
        //todo: so, when house is picked up -> all socket should be inactive. Big no no to be active. Or need to create logic that gives sockets back ability to turn blue
        // public XRSocketInteractor socketL;
        // private bool _isActiveL;
        // private bool _hasTargetL;
        // public XRSocketInteractor socketR;
        // private bool _isActiveR;
        // private bool _hasTargetR; //ernests
        // public XRSocketInteractor socketC;
        // private bool _isActiveC;
        // private bool _hasTargetC;
        public int controllerID;
        
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
