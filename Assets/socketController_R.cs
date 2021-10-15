using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class socketController_R : MonoBehaviour
{
    private const string CONNECTED_OBJ = "connected";

    void Start()
    {
        XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
        socket.onSelectEntered.AddListener(toggleSocket);
        socket.onSelectExited.AddListener(toggleSocket);
    }

    private void toggleSocket(XRBaseInteractable obj)
    {
        GameObject childObj = obj.transform.GetChild(0).gameObject;
        GameObject boxWall_L = childObj.transform.GetChild(1).gameObject;
        XRSocketInteractor socket = boxWall_L.GetComponent<XRSocketInteractor>();

        if (socket.socketActive == true)
        {
            socket.socketActive = false;
            boxWall_L.tag = CONNECTED_OBJ;
            this.tag = CONNECTED_OBJ;
        }
        else
        {
            socket.socketActive = true;
            boxWall_L.tag = null;
            this.tag = null;
        }
    }
}