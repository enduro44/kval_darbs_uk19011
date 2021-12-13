using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuSystem.Main
{
        public class MainMenu : MonoBehaviour
    {
        public GameObject mainUI;
        [SerializeField] public GameObject saveButtonPrefab;
        [SerializeField] public Transform contentParent;
        private FadeController _fadeController;

        private GameObject _mainMenuUI;
        private GameObject _newGameUI;
        private GameObject _loadGameUI;
        private GameObject _optionsUI;

        private void Awake()
        {
            _mainMenuUI = mainUI.transform.GetChild(0).gameObject;
            _newGameUI = mainUI.transform.GetChild(1).gameObject;
            _loadGameUI = mainUI.transform.GetChild(2).gameObject;
            _optionsUI = mainUI.transform.GetChild(3).gameObject;
            _fadeController = mainUI.AddComponent<FadeController>();
        }

        private void Start()
        {
            _mainMenuUI.SetActive(true);
            _fadeController.FadeIn(_mainMenuUI);
            _newGameUI.SetActive(false);
            _loadGameUI.SetActive(false);
            _optionsUI.SetActive(false);
        }

        public void NewButton()
        {
            PlayerData.GameID = Guid.NewGuid().ToString();
            Directory.CreateDirectory(Application.persistentDataPath + "/" + PlayerData.GameID);
            SceneManager.LoadScene("Testing");
        }
        public void LoadButton()
        {
            _fadeController.FadeOut(_mainMenuUI);
            _mainMenuUI.SetActive(false);
            GetSavedGames();
            _fadeController.FadeIn(_loadGameUI);
            _loadGameUI.SetActive(true);
        }
        
        public void BackButton()
        {
            _fadeController.FadeOut(_loadGameUI);
            _loadGameUI.SetActive(false);
            _fadeController.FadeIn(_mainMenuUI);
            _mainMenuUI.SetActive(true);
        }
        
        public void OptionsButton()
        {
            _fadeController.FadeOut(_mainMenuUI);
            _mainMenuUI.SetActive(false);
            _fadeController.FadeIn(_optionsUI);
            _optionsUI.SetActive(true);
        }
        
        public void ExitButton()
        {
            _fadeController.FadeOut(_mainMenuUI);
            #if UNITY_EDITOR
                    // Application.Quit() does not work in the editor so
                    // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }

        private void GetSavedGames()
        {
            string path = Application.persistentDataPath;
            string[] savedGames = Directory.GetDirectories(path)
                .Select(Path.GetFileName)
                .ToArray();
            Debug.Log(savedGames.Length);
            for (int i = 0; i < savedGames.Length; i++)
            {
                GameObject button = Instantiate(saveButtonPrefab, contentParent) as GameObject;
                button.GetComponent<LoadGameButtonItem>().saveName = savedGames[i];
                button.GetComponent<LoadGameButtonItem>().mainMenu = this;
                Debug.Log(savedGames[i]);
            }
        }
    }
}

