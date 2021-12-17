using System.IO;
using System.Linq;
using GameManagerData;
using UnityEngine;

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

        private GameManager _gameManager;

        private void Awake()
        {
            _mainMenuUI = mainUI.transform.GetChild(0).gameObject;
            _newGameUI = mainUI.transform.GetChild(1).gameObject;
            _loadGameUI = mainUI.transform.GetChild(2).gameObject;
            _optionsUI = mainUI.transform.GetChild(3).gameObject;
            _fadeController = mainUI.AddComponent<FadeController>();
            _gameManager = GameManager.Instance();
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
            _gameManager.StartNewGame();
        }
        public void LoadButton()
        {
            _fadeController.FadeOut(_mainMenuUI);
            _mainMenuUI.SetActive(false);
            PopulateSaveGameData();
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
            _gameManager.QuitGame();
        }

        private void PopulateSaveGameData()
        {
            string[] savedGames = _gameManager.GetSavedGames();
            for (int i = 0; i < savedGames.Length; i++)
            {
                GameObject button = Instantiate(saveButtonPrefab, contentParent) as GameObject;
                button.GetComponent<LoadGameButtonItem>().saveName = savedGames[i];
            }
        }
    }
}

