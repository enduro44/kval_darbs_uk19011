using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WristMenu : MonoBehaviour
{
    public GameObject wristUI;
    public XRSocketInteractor inventorySocket;
    //Should be reworked
    public GameObject largeRoom;
    private GameObject _objInInventory;

    public bool activeWristUI = true;

    void Start()
    {
        DisplayWristUI();
        GameObject sphere = gameObject.transform.GetChild(0).gameObject;
        inventorySocket = sphere.GetComponent<XRSocketInteractor>();
        inventorySocket.selectEntered.AddListener(GetObject);
    }

    public void MenuPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayWristUI();
        }
    }

    private void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            inventorySocket.gameObject.SetActive(false);
            activeWristUI = false;
            return;
        }
        wristUI.SetActive(true);
        inventorySocket.gameObject.SetActive(true);
        activeWristUI = true;
    }

    private void GetObject(SelectEnterEventArgs args)
    {
        XRBaseInteractable interactable = args.interactable;
        _objInInventory = interactable.gameObject;
    }
    
    public void AddRoom()
    {
        if (inventorySocket.selectTarget != null)
        {
            Destroy(_objInInventory);
        }
        GameObject newRoom = Instantiate(largeRoom, transform.position, Quaternion.identity);
    }

    // public void SaveGame()
    // {
    //     _gameManager.SaveJsonData(_gameData);
    // }
}
