using GameManagerData;
using TMPro;
using UnityEngine;

namespace MenuSystem
{
    public class LoadGameButtonItem : MonoBehaviour
    {
        [HideInInspector] public string saveName;
        [HideInInspector] public MainMenu mainMenu;

        [SerializeField] public GameObject buttonText;
        private TextMeshProUGUI _textField;

        private void Awake()
        {
            _textField = buttonText.GetComponent<TextMeshProUGUI>();
            _textField.text = saveName;
        }

        public void OnButtonClick()
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadGame(saveName);
        }
    }
}
