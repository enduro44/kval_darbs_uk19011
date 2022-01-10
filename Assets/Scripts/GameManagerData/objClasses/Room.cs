using Controllers;
using UnityEngine;

namespace GameManagerData.objClasses
{
    public class Room : MonoBehaviour
    {
        //Klase nodrošina, ka katrs istabas objekts tiks pievienots tam atbilstošajā aktīvās spēles datu sarakstā, un tiks noņemts, 
        //kad tas tiek iznīcināts Kā arī, ka katrai istabai ir vieta, kur piešķirt mājas kontroliera identifikatoru, kuram
        //tā pieder
        public string controllerID;

        void Awake()
        {
            GameData.Rooms.Add(this);
        }

        private void OnDestroy()
        {
            GameData.Rooms.Remove(this);
            RoomController.GrabbableRooms.Remove(gameObject);
        }
    }
}
