using GameManagerData;
using UnityEngine;

namespace MenuSystem.Main
{
    public class MainMenu : MonoBehaviour
    {
        private static MainMenu _instance;
        public GameObject mainUI;
        [SerializeField] public GameObject saveButtonPrefab;
        [SerializeField] public Transform contentParent;
        private FadeController _fadeController;

        private GameObject _mainMenuUI;
        //private GameObject _newGameUI;
        private GameObject _loadGameUI;
        //private GameObject _optionsUI;
        private GameObject _confirmationUI;
        private GameObject _errorUI;
        
        private string _saveName;
        private GameObject _saveGameButton;

        private GameManager _gameManager;

        private void Awake()
        {
            _instance = this;
            _mainMenuUI = mainUI.transform.GetChild(0).gameObject;
            //_newGameUI = mainUI.transform.GetChild(1).gameObject;
            _loadGameUI = mainUI.transform.GetChild(2).gameObject;
            //_optionsUI = mainUI.transform.GetChild(3).gameObject;
            _confirmationUI = mainUI.transform.GetChild(4).gameObject;
            _errorUI = mainUI.transform.GetChild(5).gameObject;
            _fadeController = mainUI.AddComponent<FadeController>();
            _gameManager = GameManager.Instance();
        }
        
        public static MainMenu Instance() {
            return _instance;
        }

        private void Start()
        {
            _mainMenuUI.SetActive(true);
            _fadeController.FadeIn(_mainMenuUI);
            //_newGameUI.SetActive(false);
            _loadGameUI.SetActive(false);
            //_optionsUI.SetActive(false);
            _confirmationUI.SetActive(false);
            _errorUI.SetActive(false);
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
            foreach (Transform child in contentParent) {
                GameObject.Destroy(child.gameObject);
            }
            _fadeController.FadeOut(_loadGameUI);
            _loadGameUI.SetActive(false);
            _fadeController.FadeIn(_mainMenuUI);
            _mainMenuUI.SetActive(true);
        }

        // public void OptionsButton()
        // {
        //     _fadeController.FadeOut(_mainMenuUI);
        //     _mainMenuUI.SetActive(false);
        //     _fadeController.FadeIn(_optionsUI);
        //     _optionsUI.SetActive(true);
        // }

        public void ExitButton()
        {
            _fadeController.FadeOut(_mainMenuUI);
            _gameManager.QuitGame();
        }

        public void ShowPopup(string saveName, GameObject button)
        {
            _saveName = saveName;
            _saveGameButton = button;
            _fadeController.FadeOut(_loadGameUI);
            _loadGameUI.SetActive(false);
            _fadeController.FadeIn(_confirmationUI);
            _confirmationUI.SetActive(true);
        }
        
        public void HidePopup()
        {
            _confirmationUI.SetActive(false);
            _fadeController.FadeOut(_confirmationUI);
            _fadeController.FadeIn(_loadGameUI);
            _loadGameUI.SetActive(true);
        }
        
        public void OnDeleteDisconfirmation()
        {
            HidePopup();
        }
        public void OnDeleteConfirmation()
        {
            _gameManager.DeleteGame(_saveName);
            Destroy(_saveGameButton);
            HidePopup();
        }

        public void ShowGameCouldNotBeLoadedError()
        {
            _fadeController.FadeOut(_loadGameUI);
            _loadGameUI.SetActive(false);
            _fadeController.FadeIn(_errorUI);
            _errorUI.SetActive(true);
        }

        public void CloseGameCouldNotBeLoadedError()
        {
            _fadeController.FadeOut(_errorUI);
            _errorUI.SetActive(false);
            _fadeController.FadeIn(_loadGameUI);
            _loadGameUI.SetActive(true);
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