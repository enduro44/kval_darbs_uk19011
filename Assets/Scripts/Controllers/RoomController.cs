using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase kontrolē vai istaba ir paceļama vai nē, kā arī tā satur statisku sarakstu ar visām istabām, kas ir paceļamas, lai
    //situācijā, kad spēles stadija mainās, sistēma var padarīt visas paceļamās istabas nepaceļamas un vice versa pēc attiecīgās
    //spēles stadijas nepieciešamības.
    public class RoomController : MonoBehaviour
    {
        public static List<GameObject> GrabbableRooms = new List<GameObject>();
        private XRGrabInteractable _grabber;
        private bool _hasObjectL;
        private bool _hasObjectR;
        private bool _hasObjectC;

        void Awake()
        {
            //Šī ir istabas komponente, kas kontrolē to vai istaba ir paceļama vai nē
            _grabber = gameObject.GetComponent<XRGrabInteractable>();
            
            GameObject box = gameObject.transform.GetChild(0).gameObject;
            GameObject grandChildObjL = box.transform.GetChild(1).gameObject;
            GameObject grandChildObjR = box.transform.GetChild(2).gameObject;
            GameObject grandChildObjC = box.transform.GetChild(3).gameObject;
            GameObject left = grandChildObjL.transform.GetChild(0).gameObject;
            GameObject right = grandChildObjR.transform.GetChild(0).gameObject;
            GameObject ceiling = grandChildObjC.transform.GetChild(0).gameObject;
            XRSocketInteractor socketL = left.GetComponent<XRSocketInteractor>();
            XRSocketInteractor socketR = right.GetComponent<XRSocketInteractor>();
            XRSocketInteractor socketC = ceiling.GetComponent<XRSocketInteractor>();
            
            //Visām kontaktligzdām ir pievienoti listeneri, lai kontrolētu kontakligzdu pieejamību un no tā izsecinātu
            //vai istaba ir paceļama vai nē
            socketL.selectEntered.AddListener(EnteredL);
            socketL.selectExited.AddListener(ExitedL);
            socketR.selectEntered.AddListener(EnteredR);
            socketR.selectExited.AddListener(ExitedR);
            socketC.selectEntered.AddListener(EnteredC);
            socketC.selectExited.AddListener(ExitedC);
        }

        //Brīdī, kad kādā no istabas kontaktligzdām tiek pievienota jauna istaba tā vairs nav paceļama, 
        //bet jaunā istaba ir paceļama, šo konfigurāciju apstrādā visas Entered metodes
        private void EnteredL(SelectEnterEventArgs args)
        {
            _hasObjectL = true;
            ToggleGrab();
            
            GameObject obj = args.interactable.gameObject;
            GrabbableRooms.Add(obj);
            GrabbableRooms.Remove(gameObject);
        }
        
        //Brīdī, kad bāzes istabai tiek noņemta istabas no tās kontakligzdas, noņemtā istaba tiek pievienota
        //paceļamo istabu sarakstam un bāzes istabu apstrādā ProcessBaseRoom() metode, kas nosaka vai
        //kādā citā kontaktligzdā vēl ir istabas, un,ja nav, tad pievieno bāzes istabu paceļamo istabu sarakstā.
        //Šādu konfigurāciju veic visas Exited metodes.
        private void ExitedL(SelectExitEventArgs args)
        {
            _hasObjectL = false;
            ToggleGrab();
            
            GameObject room = args.interactable.gameObject;
            GrabbableRooms.Remove(room);
            ProcessBaseRoom(gameObject);
        }
        private void EnteredR(SelectEnterEventArgs args)
        {
            _hasObjectR = true;
            ToggleGrab();
            
            GameObject obj = args.interactable.gameObject;
            GrabbableRooms.Add(obj);
            GrabbableRooms.Remove(gameObject);
        }
        
        private void ExitedR(SelectExitEventArgs args)
        {
            _hasObjectR = false;
            ToggleGrab();
            
            GameObject room = args.interactable.gameObject;
            GrabbableRooms.Remove(room);
            ProcessBaseRoom(gameObject);
        }
        private void EnteredC(SelectEnterEventArgs args)
        {
            _hasObjectC = true;
            ToggleGrab();
            
            GameObject obj = args.interactable.gameObject;
            GrabbableRooms.Add(obj);
            GrabbableRooms.Remove(gameObject);
        }
        
        private void ExitedC(SelectExitEventArgs args)
        {
            _hasObjectC = false;
            ToggleGrab();
            
            GameObject room = args.interactable.gameObject;
            GrabbableRooms.Remove(room);
            ProcessBaseRoom(gameObject);
        }

        private void ToggleGrab()
        {
            if (_hasObjectL || _hasObjectR || _hasObjectC)
            {
                //Situācijā, kad kādā no kontakligzdām ir pievienota istaba, bāzes istaba nevar būt paceļama, tāpēc
                //tiek mainīta interactionLayerMask, lai spēlētājs to nevar pacelt, bet, lai istaba joprojām var būt
                //savienota ar citu istabu
                _grabber.interactionLayerMask = (1 << 6);
                return;
            }
            //Ja kontakligzdās nav nevienas istabas, tad istabu drīkst pacelt, tāpēc tiek pielikta atpakaļ
            //spēlētāja interactionLayerMask
            _grabber.interactionLayerMask = (1<<6) | (1<<7);
        }
        
        //Metode brīdī, kad istabai tiek noņemta pievienotā istaba pārbauda vai kādā citā kontaktligzdā ir pievienota istaba
        //un, ja ir, tad nedara neko, bet, ja nav, tad pievieno istabu paceļamo istabu sarkastā
        private void ProcessBaseRoom(GameObject obj)
         {
             if (_hasObjectL || _hasObjectR || _hasObjectC)
             {
                 return;
             }
             GrabbableRooms.Add(gameObject);
         }
        
        //Šīs abas metodes tiek izmantotas spēles stadijas maiņā, lai neļautu spēlētājam pacelt istabas, kuras ir paceļamas
        //vai, lai atgrieztu spēlētājam iespēju pacelt paceļamās istabas
        public static void ToggleGrabOffForGrabbableRooms()
         {
             foreach (var room in GrabbableRooms)
             {
                 room.GetComponent<XRGrabInteractable>().interactionLayerMask = (1 << 6);
             }
         }
        
        public static void ToggleGrabOnForGrabbableRooms()
         {
             foreach (var room in GrabbableRooms)
             {
                 room.GetComponent<XRGrabInteractable>().interactionLayerMask = (1<<6) | (1<<7);
             }
         }
    }
}