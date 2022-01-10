using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerL : MonoBehaviour
    {
        //Klase kontrolē istabas kreisās sienas kontaktligzdu, kā arī istabas un citus spēles objekuts, ko spēlētājs
        //novieto kontaktligzdā
        private SocketController _controller;
        private XRSocketInteractor _socketL;
        
        //Kontaktligzdas vizuālais atveidojums
        private GameObject _socketVisual;
        private MeshFilter _mesh;
        
        private bool _canBePlaced = true;
        private SocketAccessibilityController _socketAccessibilityController;
        //Mainīgais nodrošina, ka dažādām istabām var piešķirt dažādas pozīcijas pēc pievienošanas mājas struktūrai
        private GameObject _socketTransform;
        
        void Awake()
        {
            _socketAccessibilityController  = new SocketAccessibilityController();
            _controller = new SocketController();
            _socketL = gameObject.GetComponent<XRSocketInteractor>();
            //Notikumu klausīšanās
            _socketL.selectEntered.AddListener(Entered);
            _socketL.selectExited.AddListener(Exited);
            _socketL.hoverEntered.AddListener(HoverEntered);
            _socketL.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketL.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();

            _socketTransform = _socketL.transform.parent.gameObject.transform.GetChild(1).gameObject;
        }
    
        //Ieiešanas notikums, ko izsauc istabas novietošana kontaktligzdā
        private void Entered(SelectEnterEventArgs args)
        {
            //Ja spēles objektu,ko spēlētājs mēģina pievienot nedrīkst pievienot mājas struktūrai, to iznīcina
            if (!_canBePlaced)
            {
                Destroy(_socketL.selectTarget.gameObject.transform.root.gameObject);
                return;
            }
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            //Kontaktligzda vairs nab brīva, tapēc to noņem no saraksta
            EmptyActiveSocketController.RemoveSocket(controllerID, _socketL);
            
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;

            //Pievienoto istabu novieto pareizā pozīcijā un izmaina izmēru
            TransformPosition(obj);
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;

            _controller.ToogleConnectedTag(obj);
            _socketVisual.SetActive(false);
            
            obj.GetComponent<Room>().controllerID = controllerID;
            //Ieslēdz kontaktligzdas
            _controller.TurnOnSocketLeft(obj);
            _controller.TurnOnSocketCeiling(obj);
        }

        //Funkcija nodrošina precīzu pievienoto istabu pozicionēšanu, tā kā istabas nav vienādās, tām nepieciešmamas
        //dažādas pozīcijas
        private void TransformPosition(XRBaseInteractable obj)
        {
            string type = _controller.GetType(obj);
            if (type == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-12.19f, -0.466f, 0.0100003f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            if (type == "CornerRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-17.41f, -0.466f, -0.489f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, -90, 0);
            }

            if (type == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-7.01f, -0.466f, 0.001f);
                _socketTransform.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        //Istabas noņemšas brīdī tiek izsaukta iziešanas notikumu funkcija
        private void Exited(SelectExitEventArgs args)
        {
            if (!_canBePlaced)
            {
                return;
            }
            
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            //Kontaktligdzu pieliek atpakaļ sarakstā, jo tā ir brīva un aktīva
            EmptyActiveSocketController.AddSocket(controllerID, _socketL);
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            //Noņemtajai istabai izslēdz visas kontaktligzdas, noņem etiķeti
            _controller.TurnOffSocketLeft(obj);
            _controller.TurnOffSocketCeiling(obj);
            _controller.ToogleConnectedTag(obj);
            //Ieslēdz istabas kontaktligzdas vizuālo atveidojumu
            _socketVisual.SetActive(true);
            obj.GetComponent<Room>().controllerID = "";
        }
        
        //Funkcija nodrošina tuvināšanās notikuma apstrādi, iekrāsojot kontaktligzdu zaļu, ja objektu iespējams 
        //pievienot un sarkanu, ka nav iespējams
        private void HoverEntered(HoverEnterEventArgs args)
        {
            _canBePlaced = _socketAccessibilityController.ProcessEnterLR(args, _mesh, _canBePlaced);
        }
        
        //Funkcija nodrošina attālināšanās notikuma apstrādi, atgriežot kontaktligzdas krāsu brīvas aktīvas
        // kontaktligzdas krāsā
        private void HoverExited(HoverExitEventArgs args)
        {
            _canBePlaced = _socketAccessibilityController.ProcessExitLR(_mesh);
        }
    }
}
