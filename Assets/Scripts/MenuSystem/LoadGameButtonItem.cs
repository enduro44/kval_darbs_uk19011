using GameManagerData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            DontDestroyOnLoad(this.gameObject);
            _textField = buttonText.GetComponent<TextMeshProUGUI>();
            _textField.text = saveName;
        }

        public void OnButtonClick()
        {
            //Scene loads without objects, however, objects are generated, can see this by Log
            //SceneManager.LoadScene("Testing"); this didnt help the issue
            GameManager gameManager = GameManager.Instance();
            gameManager.LoadGame(saveName);
        }
    }
}
