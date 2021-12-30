using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    //Klase kontrolē kontaktligzdu krāsu, atkarībā no tā vai objekts, ko mēģina pievienot ir atļauts vai nav
    public class SocketAccessibilityController
    {
        public Color meshColorActive = new Color(255, 0, 255, 0.3f);
        public Color meshColorAllowed = new Color(0, 204, 102, 0.3f);
        public Color meshColorDanger = new Color(255, 0, 0, 0.3f);

        public void ColorActive(MeshFilter mesh)
        {
            mesh.GetComponent<MeshRenderer>().material.color = meshColorActive;
        }
        
        public void ColorAllowed(MeshFilter mesh)
        {
            mesh.GetComponent<MeshRenderer>().material.color = meshColorAllowed;
        }
        
        public void ColorDanger(MeshFilter mesh)
        {
            mesh.GetComponent<MeshRenderer>().material.color = meshColorDanger;
        }
        //Metode apstrādā gan labo, gan kreiso kontakligzdu
        // ReSharper disable once InconsistentNaming
        public bool ProcessEnterLR(HoverEnterEventArgs args, MeshFilter mesh, bool canBePlaced)
        {
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;

            //Šī pārbaude apstrādā situāciju, kad ir sabūvēta mājas stuktūra, kur saskarās divas istabas, kas jau ir pievienotas mājai.
            //Tā kā šādā situācijā istabas nevar pievienoties vienai otrai, kontakligzda tiek iekrāsota sarkana
            if (rootObj.CompareTag("Connected") || !canBePlaced)
            {
                ColorDanger(mesh);
                return false;
            }

            //Ja mājas struktūrai mēģina pievienot mājas kontrolieri, tad arī šī darbība nav atļauta
            if (rootObj.name == "HomeController(Clone)")
            {
                ColorDanger(mesh);
                return false;
            }
            
            ColorAllowed(mesh);
            return true;
        }
        
        //Objektiem izejot no kontakligzdas tās krāsa tiek atgriezta uz pamata aktīvo
        // ReSharper disable once InconsistentNaming
        public bool ProcessExitLR(MeshFilter mesh)
        {
            ColorActive(mesh);
            return true;
        }
        
        //Metode nodrošina griestu kontaktligzdu pieejamību
        public void ProcessEnterC(HoverEnterEventArgs args, SocketController controller, GameObject root, MeshFilter mesh)
        {
            XRBaseInteractable obj = args.interactable;

            string typeOfObjectInSocket = controller.GetType(obj);
            GameObject objInSocket = obj.transform.root.gameObject;
            string typeOfRootObject = root.name;

            if (typeOfRootObject != typeOfObjectInSocket && !objInSocket.CompareTag("Connected"))
            {
                ColorDanger(mesh);
            }
            else
            {
                ColorAllowed(mesh);
            }
        }
        
        public void ProcessExitC(MeshFilter mesh)
        {
            ColorActive(mesh);
        }

        //Metode nodrošina mājas kontroliera kontakligzdas pieejamību
        public void ProcessEnterH(HoverEnterEventArgs args, MeshFilter mesh)
        {
            XRBaseInteractable obj = args.interactable;
            string typeOfObjectInSocket = obj.gameObject.transform.root.gameObject.name;
            
            if (typeOfObjectInSocket == "HomeController(Clone)")
            {
                ColorDanger(mesh);
            }
            else
            {
                ColorAllowed(mesh);
            }
        }
        
        public void ProcessExitH(MeshFilter mesh)
        {
            ColorActive(mesh);
        }
    }
}