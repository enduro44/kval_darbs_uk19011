using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketControllerR : MonoBehaviour
    {
        //Klase kontrolē istabas labās sienas kontaktligzdu, kā arī istabas un citus spēles objekuts, ko spēlētājs
        //novieto kontaktligzdā
        private SocketController _controller;
        private XRSocketInteractor _socketR;
        
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
            _socketR = gameObject.GetComponent<XRSocketInteractor>();
            //Notikumu klausīšanās
            _socketR.selectEntered.AddListener(Entered);
            _socketR.selectExited.AddListener(Exited);
            _socketR.hoverEntered.AddListener(HoverEntered);
            _socketR.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketR.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();

            _socketTransform = _socketR.transform.parent.gameObject.transform.GetChild(1).gameObject;
        }

        private void Entered(SelectEnterEventArgs args)
        {
            //Ja spēles objektu,ko spēlētājs mēģina pievienot nedrīkst pievienot mājas struktūrai, to iznīcina

            if (!_canBePlaced)
            {
                Destroy(_socketR.selectTarget.gameObject.transform.root.gameObject);
                return;
            }
            
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            //Kontaktligzda vairs nab brīva, tapēc to noņem no saraksta
            EmptyActiveSocketController.RemoveSocket(controllerID, _socketR);
            
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
            _controller.TurnOnSocketRight(obj);
            _controller.TurnOnSocketCeiling(obj);
        }

        //Funkcija nodrošina precīzu pievienoto istabu pozicionēšanu, tā kā istabas nav vienādās, tām nepieciešmamas
        //dažādas pozīcijas
        private void TransformPosition(XRBaseInteractable obj)
        {
            string type = _controller.GetType(obj);
            if (type == "CornerRoom(Clone)" || type == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(12.19f, -0.466f, 0.0100003f);
            }

            if (type == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(7.01f, -0.466f, 0.001f);
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
            EmptyActiveSocketController.AddSocket(controllerID, _socketR);
            
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
            //Noņemtajai istabai izslēdz visas kontaktligzdas, noņem etiķeti
            _controller.TurnOffSocketRight(obj);
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