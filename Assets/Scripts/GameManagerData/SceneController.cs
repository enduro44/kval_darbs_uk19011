using GameManagerData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerData
{
    public class SceneController 
    {
        public void LoadGameScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Testing");
            while (!operation.isDone)
            {
                Debug.Log("Scene is loading");
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Testing"));
        }
    }
}