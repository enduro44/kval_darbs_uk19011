using Controllers;
using GameManagerData;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace MenuSystem.Wrist
{
    public class WristMenu : MonoBehaviour
    {
        public GameObject wristUI;
        
        public GameObject mainMenuUI;
        public GameObject buildMenuUI;
        public GameObject furnishMenuUI;
        public GameObject playablesUI;
        public GameObject exitToMainUI;
        public GameObject exitUI;
        public GameObject backButton;

        public bool activeWristUI = true;
        
        public GameObject inventoryObject;
        public XRSocketInteractor inventorySocket;

        private void Start()
        {
            DisplayWristUI();
            inventorySocket = inventoryObject.GetComponent<XRSocketInteractor>();
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
            
            mainMenuUI.SetActive(true);
            buildMenuUI.SetActive(false);
            furnishMenuUI.SetActive(false);
            playablesUI.SetActive(false);
            exitToMainUI.SetActive(false);
            exitUI.SetActive(false);
            backButton.SetActive(false);
            
            ScrollViewController.DestroyPreviousData();
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
        }

        public void PlayButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesNonStatic();
            
            ScrollViewController.DestroyPreviousData();
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
        }
        
        public void SaveButton()
        {
            //Need to make button unpressable and show that game is saved
            GameManager gameManager = GameManager.Instance();
            gameManager.SaveGame();   
        }
        
        public void BuildButton()
        {
            EmptyActiveSocketController.TurnOnAllSockets();
            RoomController.ToggleGrabOnForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesStatic();

            ShowBuildMenu();
        }

        private void ShowBuildMenu()
        {
            mainMenuUI.SetActive(false);
            buildMenuUI.SetActive(true);
            backButton.SetActive(true);

            ScrollViewController.DestroyPreviousData();
            ScrollViewController.ShowScrollView();
            InventoryController.ShowInventory();
        }

        public void FurnishButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureMovable();
            PlayableController.SetAllPlayablesStatic();

            ShowFurnishMenu();
        }

        private void ShowFurnishMenu()
        {
            mainMenuUI.SetActive(false);
            furnishMenuUI.SetActive(true);
            backButton.SetActive(true);

            ScrollViewController.DestroyPreviousData();
            ScrollViewController.ShowScrollView();
            InventoryController.ShowInventory();
        }

        public void PlayablesButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesNonStatic();
            
            ShowPlayablesMenu();
        }

        private void ShowPlayablesMenu()
        {
            mainMenuUI.SetActive(false);
            playablesUI.SetActive(true);
            backButton.SetActive(true);

            ScrollViewController.DestroyPreviousData();
            ScrollViewController.ShowScrollView();
            InventoryController.ShowInventory();
        }

        public void BackToMenuButton()
        {
            buildMenuUI.SetActive(false);
            furnishMenuUI.SetActive(false);
            playablesUI.SetActive(false); 
            mainMenuUI.SetActive(true);
            backButton.SetActive(false);
            ScrollViewController.DestroyPreviousData();
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
        }
        
        public void ExitToMainButton()
        {
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadNewScene("MainMenu");
        }
        
        public void ExitButton()
        {
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
            GameManager gameManager = GameManager.Instance();
            gameManager.QuitGame();
        }
    }
}
