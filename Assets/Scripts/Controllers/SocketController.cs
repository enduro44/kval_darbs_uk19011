using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase pārvalda tai padotās kontakligzdas, izslēdz vai ieslēdz tās, apstrādā savienojuma etiķetes, kā arī pievieno vai noņem aktīvo brīvo kontakligzdu sarakstam
    public class SocketController
    {
        private GameObject _socketVisual;

        private XRSocketInteractor GetSocket(GameObject obj, int index)
        {
            GameObject boxWall = obj.transform.GetChild(index).gameObject;
            return boxWall.GetComponent<XRSocketInteractor>();
        }
        
        private GameObject GetSocketVisual(XRSocketInteractor obj)
        {
            GameObject visual = obj.transform.GetChild(0).gameObject;
            return visual;
        }
        
        public string GetType(XRBaseInteractable obj)
        {
            GameObject objInSocket = GetRoot(obj);
            return objInSocket.name;
        }

        public GameObject GetRoot(XRBaseInteractable obj)
        {
            return obj.transform.root.gameObject;
        }
        
        //Ieslēdzot kontaktligzdu ir svarīgi noteikt vai tā ir istaba, kas ir jumts, jo jumtiem nav jāieslēdz kontaktligzdas
        public bool IsRoof(XRBaseInteractable obj)
        {
            string typeOfObjectInSocket = GetType(obj);
            if (typeOfObjectInSocket == "SmallRoof(Clone)" || typeOfObjectInSocket == "LargeRoof(Clone)" || typeOfObjectInSocket == "CornerRoof(Clone)")
            {
                return true;
            }

            return false;
        }
        
        //Funkcija piešķir savienojuma etiķeti istabām
        public void ToogleConnectedTag(XRBaseInteractable obj)
        {
            GameObject rootObj = obj.transform.root.gameObject;
            if (rootObj.CompareTag("Connected"))
            {
                rootObj.tag = "Disconnected";
            }
            else
            {
                rootObj.tag = "Connected";
            }
        }

        public void TurnOnSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(1).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);
            
            AddSocketToList(obj, socket);
            TurnOnSocket(socket);
        }
        
        public void TurnOnSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(2).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            AddSocketToList(obj, socket);
            TurnOnSocket(socket);
        }
        
        public void TurnOnSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(3).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            AddSocketToList(obj, socket);
            TurnOnSocket(socket);
        }

        public void TurnOffSocketLeft(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(1).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            RemoveSocketFromList(obj, socket);
            TurnOffSocket(socket);
        }
        
        public void TurnOffSocketRight(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(2).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            RemoveSocketFromList(obj, socket);
            TurnOffSocket(socket);
        }
        
        public void TurnOffSocketCeiling(XRBaseInteractable obj)
        {
            GameObject childObj = obj.transform.GetChild(0).gameObject;
            GameObject grandChildObj = childObj.transform.GetChild(3).gameObject;
            XRSocketInteractor socket = GetSocket(grandChildObj,0);

            RemoveSocketFromList(obj, socket);
            TurnOffSocket(socket);
        }

        public void TurnOnSocket(XRSocketInteractor socket)
        {
            socket.socketActive = true;

            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(true);
        }
        
        public void TurnOffSocket(XRSocketInteractor socket)
        {
            socket.socketActive = false;
            
            _socketVisual = GetSocketVisual(socket);
            _socketVisual.SetActive(false);
        }

        //Brīvās aktīvās kontaktligzdas tiek pievienotas to mājas kontroliera struktūrā
        public void AddSocketToList(XRBaseInteractable obj, XRSocketInteractor socket)
        {
            GameObject root = GetRoot(obj);
            Room room = root.GetComponent<Room>();
            EmptyActiveSocketController.AddSocket(room.controllerID, socket);
        }
        
        //Kad kontaktligzda vairs nav brīva, to no mājas kontroliera struktūras noņem
        public void RemoveSocketFromList(XRBaseInteractable obj, XRSocketInteractor socket)
        {
            GameObject root = GetRoot(obj);
            Room room = root.GetComponent<Room>();
            EmptyActiveSocketController.RemoveSocket(room.controllerID, socket);
        }

        //Funkcijas pārvalda specifiski mājas kontroliera kontaktligzdas ieslēgšanu un izslēgšanu
        public void TurnOnControllerSocket(XRSocketInteractor socket)
        {
            GameObject visual = socket.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            socket.socketActive = true;
            visual.SetActive(true);
        }
        
        public void TurnOffControllerSocket(XRSocketInteractor socket)
        {
            GameObject visual = socket.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            socket.socketActive = false;
            visual.SetActive(false);
        }
    }
}