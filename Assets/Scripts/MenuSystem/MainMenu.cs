using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameManagerData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MenuSystem
{
        public class MainMenu : MonoBehaviour
    {
        public GameObject mainUI;
        [SerializeField] public GameObject saveButtonPrefab;
        [SerializeField] public Transform contentParent;
        
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
            
            _mainMenuUI.SetActive(true);
            FadeIn(_mainMenuUI);
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
            _mainMenuUI.SetActive(false);
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
            _loadGameUI.SetActive(true);
        }
        public void OptionsButton()
        {
            _mainMenuUI.SetActive(false);
            _optionsUI.SetActive(true);
        }
        public void ExitButton()
        {
            Application.Quit();
        }

        void FadeOut(GameObject obj)
        {
            CanvasGroup group = obj.GetComponent<CanvasGroup>();
            group.interactable = false;
            for (int i=100; i>0; i--)
            {
                group.alpha -= (float) i/100;
            }
        }
        
        void FadeIn(GameObject obj)
        {
            CanvasGroup group = obj.GetComponent<CanvasGroup>();
            group.interactable = true;
            for (int i=0; i<=100; i++)
            {
                group.alpha += (float) i/100;
            }
        }

        public void LoadGame()
        {
            
        }
    }
}

