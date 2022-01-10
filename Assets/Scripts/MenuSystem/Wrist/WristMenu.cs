using System.Collections;
using Controllers;
using GameManagerData;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace MenuSystem.Wrist
{
    public class WristMenu : MonoBehaviour
    {
        //Klase nodrošina plaukstas izvēlnes skatu loģiku un komunikāciju ar GameManager klasi, lai
        //spēlētājs varētu saglabāt spēli, iziet uz galveno izvēlni vai iziet no spēles. Kā arī klase 
        //nodrošina spēles stadiju maiņu
        private static WristMenu _instance;
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
        private bool isGameBeingSaved = false;

        public GameObject inventoryObject;
        public XRSocketInteractor inventorySocket;

        private PlayerController _playerController;

        private void Awake()
        {
            _instance = this;
        }
        
        public static WristMenu Instance() {
            return _instance;
        }
        
        private void Start()
        {
            wristUI.SetActive(false);
            InventoryController.HideInventory();
            activeWristUI = false;
            _playerController = PlayerController.Instance();
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
            if (activeWristUI && !isGameBeingSaved)
            {
                ScrollViewController.HideScrollView();
                InventoryController.HideInventory();
                wristUI.SetActive(false);
                activeWristUI = false;
                _playerController.EnableRayLeftHand();
                return;
            }

            if (!activeWristUI && !isGameBeingSaved)
            {
                _playerController.DisableRayLeftHand();
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
        }

        //Spēlēšanas stadijas aktivizēšana
        public void PlayButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesMovable();
            DeleteObjectController.isPlayGameMode = true;
            
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
            isGameBeingSaved = true;
            _playerController.DisableMovementAndRays();
            TextMeshProUGUI textObject = savingGamePopup.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            textObject.text = "Saving the game...";
            SaveGame();
            yield return new WaitForSeconds(1f);
            textObject.text = "Game saved!";
            yield return new WaitForSeconds(0.5f);
            _playerController.EnableMovementAndRays();
            savingGamePopup.SetActive(false);
            mainMenuUI.SetActive(true);
            isGameBeingSaved = false;
        }

        //Mēbelēšanas stadijas aktivizēšana
        public void BuildButton()
        {
            EmptyActiveSocketController.TurnOnAllSockets();
            RoomController.ToggleGrabOnForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesNotMovable();
            DeleteObjectController.isPlayGameMode = false;
            
            ShowBuildMenu();
        }

        //Būvēšanas izvēlnes parādīšana
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

        //Mēbelēšanas stadijas aktivizēšana
        public void FurnishButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureMovable();
            PlayableController.SetAllPlayablesNotMovable();
            DeleteObjectController.isPlayGameMode = false;

            ShowFurnishMenu();
        }

        //Mēbelēšanas izvēlnes parādīšana
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

        //Spēlējamo objektu stadijas aktivizēšana
        public void PlayablesButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNotMovable();
            PlayableController.SetAllPlayablesMovable();
            DeleteObjectController.isPlayGameMode = false;
            
            ShowPlayablesMenu();
        }

        //Spēlējamo objektu izvēlnes parādīšana
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
            confirmationExit.SetActive(false);
            
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
            isGameBeingSaved = true;
            _playerController.DisableMovementAndRays();
            TextMeshProUGUI textObject = savingGamePopup.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            confirmationExitToMain.SetActive(false);
            savingGamePopup.SetActive(true);
            textObject.text = "Saving the game...";
            SaveGame();
            yield return new WaitForSeconds(1f);
            textObject.text = "Game saved!";
            yield return new WaitForSeconds(0.5f);
            textObject.text = "Exiting to main menu!";
            yield return new WaitForSeconds(0.5f);
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
            isGameBeingSaved = true;
            _playerController.DisableMovementAndRays();
            TextMeshProUGUI textObject = savingGamePopup.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            confirmationExit.SetActive(false);
            savingGamePopup.SetActive(true);
            textObject.text = "Saving the game...";
            SaveGame();
            yield return new WaitForSeconds(1f);
            textObject.text = "Game saved!";
            yield return new WaitForSeconds(0.5f);
            textObject.text = "Exiting the game!";
            yield return new WaitForSeconds(0.5f);
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
