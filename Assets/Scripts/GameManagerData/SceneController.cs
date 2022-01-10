using System.Collections;
using System.Xml.Linq;
using Controllers;
using GameManagerData.data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerData
{
    public class SceneController : MonoBehaviour
    {
        //Klase nodrošina ainas maiņas līdzprogrammas darbību
        public void StartSceneLoad(string sceneName)
        {
            StartCoroutine(LoadGameSceneAsync(sceneName));
        }
        
        IEnumerator LoadGameSceneAsync(string sceneName)
        {
            yield return null;
            
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
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