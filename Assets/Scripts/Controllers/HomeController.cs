using GameManagerData.data;
using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Mājas kontroliera klase rūpējas par spēlei pievienotā mājas kontroliera paceļamības un kontakglizgzdu loģiku, kā arī reģistrē kontrolieri
    //EmptyActiveSocketController sarakstā, lai spēles stadijas mainot tam un tam pievienotajām istabām varētu ieslēgt un izslēgt kontaktligzdas
    public class HomeController : MonoBehaviour
    {
        private SocketController _socketController;
        private XRSocketInteractor _homeSocket;
        private GameObject _socketVisualOnEmpty;
        private GameObject _socketVisual;
        private EmptyActiveSocketData _emptyActiveSocketData;

        private SocketAccessibilityController _socketAccessibilityController;
        private static GameObject _root;
        
        private void Awake()
        {
            _root = gameObject.transform.root.gameObject;
            
            _socketAccessibilityController  = new SocketAccessibilityController();
            _socketController = new SocketController();
            _homeSocket = gameObject.GetComponent<XRSocketInteractor>();
            _homeSocket.selectEntered.AddListener(Entered);
            _homeSocket.selectExited.AddListener(Exited);
            _homeSocket.hoverEntered.AddListener(HoverEntered);
            _homeSocket.hoverExited.AddListener(HoverExited);
            
            _socketVisualOnEmpty = _homeSocket.transform.GetChild(0).gameObject;
            _socketVisual = _socketVisualOnEmpty.transform.GetChild(0).gameObject;

            //Katrs jauns kontrolieris izveido savu struktūru, kurai vēlāk tiks pievienotas kontrolierim pievienoto istabu kontaktligzdas
            _emptyActiveSocketData = new EmptyActiveSocketData(_root.GetComponent<HomeControllerObject>().controllerID, _homeSocket);
            EmptyActiveSocketController.AddData(_emptyActiveSocketData);
            
            //Mājas kontrolieris tiek arī pievienots paņemamo istabu sarakstam, lai arī tas tehniski nav "istaba", tas ietiplst istabu kategorijā un var 
            //tikt apstrādāts reizē ar istabām, kad tiek mainīta spēles stadija
            RoomController.GrabbableRooms.Add(_root);
        }
        
        private void OnDestroy()
        {
            EmptyActiveSocketController.RemoveData(_emptyActiveSocketData);
        }

        private void Entered(SelectEnterEventArgs args)
        {
            string nameOfObject = args.interactable.gameObject.transform.root.gameObject.name;
            //Spēle neļauj pievienot mājas kontrolieri citam mājas kontrolierim, tapēc viens no kontrolieriem tiek dzēsts
            //Kā arī mājas kontrolierim nevar pievienot jumtu
            if (name == "HomeController(Clone)" || _socketController.IsRoof(args.interactable))
            {
                Destroy(_homeSocket.selectTarget.gameObject.transform.root.gameObject);
                return;
            }
            
            _emptyActiveSocketData.isControllerEmpty = false;
            SetControllerNotGrabbable();
            
            //Kontrolierim pievienotā istaba atgūst tās pamata izmēru, kad tā tiek pievienota kontrolierim
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            
            //Pievienotajai istabai tiek piešķirts mājas kontroliera identifikators
            obj.GetComponent<Room>().controllerID = _root.GetComponent<HomeControllerObject>().controllerID;
            
            //Pievienotajai istabai tiek konfigurētas kontakligzdas un piešķirta etiķete, kā tā ir pievienota struktūrai
            TurnOnSockets(obj);
            _socketController.ToogleConnectedTag(obj);
            
            //Kad istaba ir pievienota, mājas kontroliera vizuālais veidols tiek izslēgts
            _socketVisualOnEmpty.SetActive(false);

            //Jauno istabu pievieno paņemamo istabu sarakstam, bet mājas kontrolieri noņem, jo to vairs nevar pacelt
            GameObject room = args.interactable.gameObject;
            RoomController.GrabbableRooms.Add(room);
            RoomController.GrabbableRooms.Remove(_root);
        }
    
        private void Exited(SelectExitEventArgs args)
        {
            if (args.interactable.gameObject.transform.root.gameObject.name == "HomeController(Clone)")
            {
                return;
            }
            
            _emptyActiveSocketData.isControllerEmpty = true;
            SetControllerGrabbable();
            
            //Noņemtai istabai tiek samazināts izmērs, lai to ir vieglāk pārvietot spēles ainā
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
            obj.transform.localScale = scaleChange;

            //Istaba vairs nepieder mājas kontrolierim, tāpēc tās kontroliera ID tiek nodzēsts
            obj.GetComponent<Room>().controllerID = "";
            
            //Istabai izslēdz visas kontakligzdas un noņem pievienoto etiķeti
            ResetSockets(obj);
            _socketController.ToogleConnectedTag(obj);
            
            //Tukšam kontrolierim ieslēdz atpakaļ tā vizuālo atveidojumu
            _socketVisualOnEmpty.SetActive(true);

            //Noņemto istabu noņem no paņemamo istabu saraksta un mājas kontrolieri pieliek atpakaļ
            GameObject room = args.interactable.gameObject;
            RoomController.GrabbableRooms.Remove(room);
            RoomController.GrabbableRooms.Add(_root);
        }
        
        //Hover metodes rūpējas par to, lai lietotājs saņemtu vizuālu informāciju par to vai istabu var pievienot vai nē
        private void HoverEntered(HoverEnterEventArgs args)
        {
            _socketAccessibilityController.ProcessEnterH(args, _socketVisual.GetComponent<MeshFilter>());
        }
        
        private void HoverExited(HoverExitEventArgs args)
        {
            _socketAccessibilityController.ProcessExitH(_socketVisual.GetComponent<MeshFilter>());
        }
        
        //Metode ieslēdz visas kontakligzdas pievienotajai istabai
        private void TurnOnSockets(XRBaseInteractable obj)
        {
            _socketController.TurnOnSocketLeft(obj);
            _socketController.TurnOnSocketRight(obj);
            _socketController.TurnOnSocketCeiling(obj);
        }
        
        //Metode izslēdz visas kontakgtligzdas noņemtai istabai
        private void ResetSockets(XRBaseInteractable obj)
        {
            _socketController.TurnOffSocketLeft(obj);
            _socketController.TurnOffSocketRight(obj);
            _socketController.TurnOffSocketCeiling(obj);
        }

        //Metode padara mājas kontroliera objektu paņemamu
        public static void SetControllerGrabbable()
        {
            if (_root == null)
            {
                return;
            }
            //Lai spēlētājs mājas kontroliera objektu varētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs var mijiedarboties
            _root.transform.root.GetComponent<XRGrabInteractable>().interactionLayerMask = (1<<7) | (1<<8);
        }
        
        //Metode padara mājas kontroliera objektu nepaņemamu
        public static void SetControllerNotGrabbable()
        {
            //Lai spēlētājs mājas kontroliera objektu nevarētu pacelt, tam tiek piešķirts LayerMask ar kuru spēlētājs nevar mijiedarboties
            _root.transform.root.GetComponent<XRGrabInteractable>().interactionLayerMask = 1<<3;
        }
    }
}
