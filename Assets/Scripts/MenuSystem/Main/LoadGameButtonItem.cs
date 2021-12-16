using System.Security.Cryptography;
using GameManagerData;
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
        
        private void Awake()
        {
            _textField = buttonText.GetComponent<TextMeshProUGUI>();
            _textField.text = saveName;
            Debug.Log(saveName);
        }

        public void OnButtonClick()
        {
            GameManager gameManager = GameManager.Instance();
            PlayerData.GameID = saveName;
            gameManager.LoadGame(saveName);
        }

        public void OnDeleteButtonClick()
        {
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/" + saveName);
            Destroy(gameObject);
        }
    }
}
