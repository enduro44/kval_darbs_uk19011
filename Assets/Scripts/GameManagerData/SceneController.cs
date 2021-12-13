using System.Collections;
using System.Xml.Linq;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerData
{
    public class SceneController : MonoBehaviour
    {
        public void StartSceneLoad(string sceneName)
        {
            StartCoroutine(LoadGameSceneAsync(sceneName));
        }

        public void SetSceneReady()
        {
            PlayerController.SetPlayerPos();
        }
        
        IEnumerator LoadGameSceneAsync(string sceneName)
        {
            yield return null;
            
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            Debug.Log("Pro :" + asyncOperation.progress);
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}