using System.IO;
using System.Security.Cryptography;
using GameManagerData;
using MenuSystem.Wrist;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace MenuSystem.Main
{
    public class LoadGameButtonItem : MonoBehaviour
    {
        [HideInInspector] public string saveName;

        [SerializeField] public GameObject buttonText;
        private TextMeshProUGUI _textField;
        private GameManager _gameManager;
        private MainMenu _mainMenu;
        
        private void Awake()
        {
            _textField = buttonText.GetComponent<TextMeshProUGUI>();
            _textField.text = saveName;
            _gameManager = GameManager.Instance();
            _mainMenu = MainMenu.Instance();
        }

        public void OnButtonClick()
        {
            PlayerData.GameID = saveName;
            _gameManager.LoadGame(saveName);
        }

        public void OnDeleteButtonClick()
        {
            _mainMenu.ShowPopup(saveName, gameObject);
        }
    }
}
