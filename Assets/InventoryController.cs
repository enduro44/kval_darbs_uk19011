using Controllers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryController : MonoBehaviour
{
    private XRSocketInteractor _socketI;
    private SocketController _controller;
    void Awake()
    {
        _controller = new SocketController();
        _socketI = gameObject.GetComponent<XRSocketInteractor>();
        _socketI.selectEntered.AddListener(Entered);
        _socketI.selectExited.AddListener(Exited);
    }

    private void Entered(SelectEnterEventArgs args)
    {
        XRBaseInteractable obj = args.interactable;
        string typeOfObjectInSocket = _controller.GetType(obj);

        if (typeOfObjectInSocket == "CornerRoom(Clone)" || typeOfObjectInSocket == "LargeRoom(Clone)" || typeOfObjectInSocket == "SmallRoom(Clone)")
        {
            Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.localScale = scaleChange;
        }
    }

    private void Exited(SelectExitEventArgs args)
    {
        //TODO: Might not need exited listener at all? Depends on other objects I will have
        // XRBaseInteractable obj = args.interactable;
        // Vector3 scaleChange = new Vector3(1, 1, 1); 
        // obj.transform.localScale = scaleChange;
    }
}
