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
        
        //dont know if I need it yet
        private FadeController _fadeController;
        
        public bool activeWristUI = true;
        
        //This is for build/furnish/playables modes
        public GameObject inventoryObject;
        public XRSocketInteractor inventorySocket;
        public Transform spawnPoint;
        private GameObject _newRoom;
        
        private void Awake()
        {
            _fadeController = wristUI.AddComponent<FadeController>();
        }
        
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
            //_fadeController.FadeIn(mainMenuUI);
            buildMenuUI.SetActive(false);
            furnishMenuUI.SetActive(false);
            playablesUI.SetActive(false);
            exitToMainUI.SetActive(false);
            exitUI.SetActive(false);
            backButton.SetActive(false);
        }

        public void PlayButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureStatic();
            //Also need to make rooms not grabby
            //Same for furniture
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
            FurnitureController.SetAllFurnitureStatic();
           // _fadeController.FadeOut(mainMenuUI);
            mainMenuUI.SetActive(false);
           // _fadeController.FadeIn(buildMenuUI);
            buildMenuUI.SetActive(true);
            backButton.SetActive(true);
        }
        
        public void FurnishButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureNonStatic();
            //_fadeController.FadeOut(mainMenuUI);
            mainMenuUI.SetActive(false);
            //_fadeController.FadeIn(furnishMenuUI);
            furnishMenuUI.SetActive(true);
            backButton.SetActive(true);
        }
        
        public void PlayablesButton()
        {
            EmptyActiveSocketController.TurnOffAllSockets();
            RoomController.ToggleGrabOffForGrabbableRooms();
            FurnitureController.SetAllFurnitureStatic();
           // _fadeController.FadeOut(mainMenuUI);
            mainMenuUI.SetActive(false);
            //_fadeController.FadeIn(playablesUI);
            playablesUI.SetActive(true);
            backButton.SetActive(true);
        }
        
        public void BackToMenuButton()
        {
            buildMenuUI.SetActive(false);
            furnishMenuUI.SetActive(false);
            playablesUI.SetActive(false); 
            mainMenuUI.SetActive(true);
            backButton.SetActive(false);
            //_fadeController.FadeIn(mainMenuUI);
        }
        
        public void ExitToMainButton()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadNewScene("MainMenu");
        }
        
        public void ExitButton()
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
