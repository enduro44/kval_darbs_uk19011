using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class socketController_L : MonoBehaviour
{
    void Start()
    {
        XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
        socket.onSelectEntered.AddListener(toggleSocket);
        socket.onSelectExited.AddListener(toggleSocket);
    }

    private void toggleSocket(XRBaseInteractable obj)
    {
        GameObject childObj = obj.transform.GetChild(0).gameObject;
        GameObject boxWall_R = childObj.transform.GetChild(2).gameObject;
        XRSocketInteractor socket = boxWall_R.GetComponent<XRSocketInteractor>();
   
        if(socket.socketActive == true)
        {
            socket.socketActive = false;
        } else
        {
            socket.socketActive = true;
        }
    }
}
