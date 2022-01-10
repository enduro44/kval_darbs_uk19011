using GameManagerData.objClasses;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase kontrolē istabas griestu kontaktligzdu, kā arī istabas un citus spēles objekuts, ko spēlētājs
    //novieto kontaktligzdā
    public class SocketControllerC : MonoBehaviour
    {
        private SocketController _controller;
        private XRSocketInteractor _socketC;
        
        //Kontaktligzdas vizuālais atveidojums
        private GameObject _socketVisual;
        private MeshFilter _mesh;
        
        private SocketAccessibilityController _socketAccessibilityController;
        //Mainīgais nodrošina, ka dažādām istabām var piešķirt dažādas pozīcijas pēc pievienošanas mājas struktūrai
        private GameObject _socketTransform;
        private GameObject _root;
        
        void Awake()
        {
            _socketAccessibilityController  = new SocketAccessibilityController();
            _controller = new SocketController();
            _socketC = gameObject.GetComponent<XRSocketInteractor>();
            //Notikumu klausīšanās
            _socketC.selectEntered.AddListener(Entered);
            _socketC.selectExited.AddListener(Exited);
            _socketC.hoverEntered.AddListener(HoverEntered);
            _socketC.hoverExited.AddListener(HoverExited);
            
            _socketVisual = _socketC.transform.GetChild(0).gameObject;
            _mesh = _socketVisual.GetComponent<MeshFilter>();

            Transform transformC = _socketC.transform;
            _socketTransform = transformC.parent.gameObject.transform.GetChild(1).gameObject;
            _root = transformC.root.gameObject;
        }

        //Ieiešanas notikums, ko izsauc istabas novietošana kontaktligzdā
        private void Entered(SelectEnterEventArgs args)
        {
            XRBaseInteractable obj = args.interactable;
            string typeOfObjectInSocket = _controller.GetType(obj);
            string typeOfRootObject = _root.name;

            //Ja istabu tipi nesaskan un pievienojamā istaba nav jumts, tad objekts tiek dzēsts
            if (typeOfRootObject != typeOfObjectInSocket && !_controller.IsRoof(obj))
            {
                Destroy(_socketC.selectTarget.gameObject.transform.root.gameObject);
                return;
            }
            
            //Istabai pievieno mājas kontroliera identifikatoru
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            obj.GetComponent<Room>().controllerID = controllerID;

            //Griestu kontaktligzda vairs nab brīva, tapēc to noņem no saraksta
            EmptyActiveSocketController.RemoveSocket(controllerID, _socketC);
            //Un izslēdz tās vizuālo atveidojumu
            _socketVisual.SetActive(false);

            //Pievienoto istabu novieto pareizā pozīcijā
            TransformPosition(typeOfObjectInSocket);
            //Un tās izmēru palielina(istabas ir samazinātas,lai ar tām vieglāk darboties)
            Vector3 scaleChange = new Vector3(1, 1, 1);
            obj.transform.localScale = scaleChange;
            //Ja istaba ir jumts, tad kontaktligzdas neieslēdz
            if (_controller.IsRoof(obj))
            {
                return;
            }
            //Ja istaba nav jumts, tad tai ieslēdz griestu kontaktligzdu
            _controller.TurnOnSocketCeiling(obj);
        }

        //Funkcija nodrošina precīzu pievienoto istabu pozicionēšanu, tā kā istabas nav vienādās, tām nepieciešmamas
        //dažādas pozīcijas
        private void TransformPosition(string typeOfObjectInSocket)
        {
            if (typeOfObjectInSocket == "CornerRoom(Clone)" || typeOfObjectInSocket == "LargeRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(0f, 1f, 0f);
            }

            if (typeOfObjectInSocket == "SmallRoom(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.515f, 0.032f, 0.486f);
            }

            if (typeOfObjectInSocket == "SmallRoof(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-1.103f, -0.006f, 1.094f);
            }
            
            if (typeOfObjectInSocket == "LargeRoof(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.545f, 0.41f, 0.58f);
            }
            
            if (typeOfObjectInSocket == "CornerRoof(Clone)")
            {
                _socketTransform.transform.localPosition = new Vector3(-0.28f, 0.39f, 0.186f);
            }
        }

        //Istabas noņemšas brīdī tiek izsaukta iziešanas notikumu funkcija
        private void Exited(SelectExitEventArgs args)
        {
            //Istabas kontaktligzdu pieliek atpakaļ sarakstā
            string controllerID = gameObject.transform.root.gameObject.GetComponent<Room>().controllerID;
            EmptyActiveSocketController.AddSocket(controllerID, _socketC);
            //Istabai samazina izmēru, laia r to ir vieglāk darboties
            XRBaseInteractable obj = args.interactable;
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;

            //Mājas struktūras istabai ieslēdz griestu kontaktligzdu,jo tā ir tagad tukša
            _socketVisual.SetActive(true);
            
            //Ja istaba ir jumts, tai noņem mājas kontroliera identifikatoru
            if (_controller.IsRoof(obj))
            {
                obj.GetComponent<Room>().controllerID = "";
                return;
            }
            //Ja istaba nav jumts, tai izslēdz greistu kontaktligzdu un noņem mājas kontroliera identifikatoru
            _controller.TurnOffSocketCeiling(obj);
            obj.GetComponent<Room>().controllerID = "";
        }

        //Funkcija nodrošina tuvināšanās notikuma apstrādi, iekrāsojot kontaktligzdu zaļu, ja objektu iespējams 
        //pievienot un sarkanu, ka nav iespējams
        private void HoverEntered(HoverEnterEventArgs args)
        {
            _socketAccessibilityController.ProcessEnterC(args, _controller, _root, _mesh);
        }
        //Funkcija nodrošina attālināšanās notikuma apstrādi, atgriežot kontaktligzdas krāsu brīvas aktīvas
        // kontaktligzdas krāsā
        private void HoverExited(HoverExitEventArgs args)
        {
            _socketAccessibilityController.ProcessExitC(_mesh);
        }
    }
}