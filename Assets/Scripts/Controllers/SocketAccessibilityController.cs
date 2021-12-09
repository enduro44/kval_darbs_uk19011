using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers
{
    public class SocketAccessibilityController
    {
        //public Color meshColorActive = new Color(0, 230, 230, 0.3f);
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
        
        // ReSharper disable once InconsistentNaming
        public bool ProcessEnterLR(HoverEnterEventArgs args, MeshFilter mesh, bool canBePlaced)
        {
            XRBaseInteractable obj = args.interactable;
            GameObject rootObj = obj.transform.root.gameObject;

            if (rootObj.CompareTag("Connected") || !canBePlaced)
            {
                ColorDanger(mesh);
                return false;
            }
            
            ColorAllowed(mesh);
            return true;
        }
        
        // ReSharper disable once InconsistentNaming
        public bool ProcessExitLR(MeshFilter mesh)
        {
            ColorActive(mesh);
            return true;
        }
        
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
    }

}