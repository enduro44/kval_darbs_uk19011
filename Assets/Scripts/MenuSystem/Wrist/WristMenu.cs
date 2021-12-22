using System.Collections;
using Controllers;
using GameManagerData;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
        public GameObject backButton;
        public GameObject confirmationExitToMain;
        public GameObject confirmationExit;
        public GameObject savingGamePopup;

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
            backButton.SetActive(false);
            confirmationExitToMain.SetActive(false);
            
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
        }

        public void PlayButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesNonStatic();
            
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
        }

        public void SaveButton()
        {
            mainMenuUI.SetActive(false);
            savingGamePopup.SetActive(true);
            StartCoroutine(SaveWithPopup());
        }
        
        IEnumerator SaveWithPopup()
        {
            TextMeshProUGUI textObject = savingGamePopup.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            SaveGame();
            textObject.text = "Saving the game...";
            yield return new WaitForSeconds(3f);
            textObject.text = "Game saved!";
            yield return new WaitForSeconds(0.5f);
            savingGamePopup.SetActive(false);
            mainMenuUI.SetActive(true);
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
            confirmationExitToMain.SetActive(false);

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
            confirmationExitToMain.SetActive(false);

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
            confirmationExitToMain.SetActive(false);

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
            confirmationExitToMain.SetActive(false);
            
            ScrollViewController.DestroyPreviousData();
            ScrollViewController.HideScrollView();
            InventoryController.HideInventory();
        }
        
        public void ExitToMainButton()
        {
            mainMenuUI.SetActive(false);
            confirmationExitToMain.SetActive(true);
        }

        public void ExitToMainSaveConfirmed()
        {
            StartCoroutine(SaveAndExitToMain());
        }

        IEnumerator SaveAndExitToMain()
        {
            TextMeshProUGUI textObject = confirmationExitToMain.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            confirmationExitToMain.SetActive(false);
            savingGamePopup.SetActive(true);
            SaveGame();
            textObject.text = "Saving the game...";
            yield return new WaitForSeconds(3f);
            textObject.text = "Game saved!";
            yield return new WaitForSeconds(1f);
            textObject.text = "Exiting to main menu!";
            yield return new WaitForSeconds(1f);
            ExitToMain();
        }

        public void ExitToMain()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadNewScene("MainMenu");
        }
        public void ExitButton()
        {
            mainMenuUI.SetActive(false);
            confirmationExit.SetActive(true);
        }
        
        public void ExitGameSaveConfirmed()
        {
            StartCoroutine(ExitWithSaving());
        }

        IEnumerator ExitWithSaving()
        {
            TextMeshProUGUI textObject = confirmationExit.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            confirmationExitToMain.SetActive(false);
            savingGamePopup.SetActive(true);
            SaveGame();
            textObject.text = "Saving the game...";
            yield return new WaitForSeconds(3f);
            textObject.text = "Game saved!";
            yield return new WaitForSeconds(1f);
            textObject.text = "Exiting the game!";
            yield return new WaitForSeconds(1f);
            Exit();
        }

        public void Exit()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.QuitGame();
        }

        public void SaveGame()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.SaveGame();
        }


    }
}
