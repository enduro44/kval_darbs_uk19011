using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class socketController_C : MonoBehaviour
{
    private const string CONNECTED_OBJ = "connected";

    void Start()
    {
        XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
        socket.onSelectEntered.AddListener(onEnter);
        socket.onSelectExited.AddListener(onExit);
    }

    private void onEnter(XRBaseInteractable obj)
    {
        //Parent game object
        GameObject parentObj = transform.parent.gameObject;
        GameObject parent_boxWall_L = parentObj.transform.GetChild(1).gameObject;
        GameObject parent_boxWall_R = parentObj.transform.GetChild(2).gameObject;

        XRSocketInteractor socket_parent_boxWall_L = parent_boxWall_L.GetComponent<XRSocketInteractor>();
        XRSocketInteractor socket_parent_boxWall_R = parent_boxWall_R.GetComponent<XRSocketInteractor>();

        socket_parent_boxWall_L.onSelectEntered.AddListener(toggleSocket_L);
        socket_parent_boxWall_L.onSelectExited.AddListener(toggleSocket_L);

        socket_parent_boxWall_R.onSelectEntered.AddListener(toggleSocket_R);
        socket_parent_boxWall_R.onSelectExited.AddListener(toggleSocket_R);
    }

    private void onExit(XRBaseInteractable obj)
    {
        //Parent game object
        GameObject parentObj = transform.parent.gameObject;
        GameObject parent_boxWall_L = parentObj.transform.GetChild(1).gameObject;
        GameObject parent_boxWall_R = parentObj.transform.GetChild(2).gameObject;

        XRSocketInteractor socket_parent_boxWall_L = parent_boxWall_L.GetComponent<XRSocketInteractor>();
        XRSocketInteractor socket_parent_boxWall_R = parent_boxWall_L.GetComponent<XRSocketInteractor>();

        socket_parent_boxWall_L.onSelectEntered.RemoveListener(toggleSocket_L);
        socket_parent_boxWall_L.onSelectExited.RemoveListener(toggleSocket_L);

        socket_parent_boxWall_R.onSelectEntered.RemoveListener(toggleSocket_R);
        socket_parent_boxWall_R.onSelectExited.RemoveListener(toggleSocket_R);
    }

    private void toggleSocket_L(XRBaseInteractable obj)
    {
        //Object in the socket
        GameObject childObj = obj.transform.GetChild(0).gameObject;
        GameObject boxWall_L = childObj.transform.GetChild(1).gameObject;

        XRSocketInteractor socket = boxWall_L.GetComponent<XRSocketInteractor>();

        if (socket.socketActive == true)
        {
            socket.socketActive = false;
        }
        else
        {
            socket.socketActive = true;
        }
    }

    private void toggleSocket_R(XRBaseInteractable obj)
    {
        //Object in the socket
        GameObject childObj = obj.transform.GetChild(0).gameObject;
        GameObject boxWall_R = childObj.transform.GetChild(2).gameObject;

        XRSocketInteractor socket = boxWall_R.GetComponent<XRSocketInteractor>();

        if (socket.socketActive == true)
        {
            socket.socketActive = false;
        }
        else
        {
            socket.socketActive = true;
        }
    }
}