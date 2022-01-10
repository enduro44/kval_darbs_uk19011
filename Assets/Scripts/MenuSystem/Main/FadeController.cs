using System.Collections;
using UnityEngine;

namespace MenuSystem
{
    public class FadeController : MonoBehaviour
    {
        //Klase nodrošina galvenās izvēlnes skatu maiņas animāciju - izgaišanu un parādīšanos
        public void FadeIn(GameObject obj)
        {
            CanvasGroup group = obj.GetComponent<CanvasGroup>();
            StartCoroutine(Fade(group, 0f, 1f));
        }
        
        public void FadeOut(GameObject obj)
        {
            CanvasGroup group = obj.GetComponent<CanvasGroup>();
            StartCoroutine(Fade(group, 1f, 0f));
        }
        
        private static IEnumerator Fade(CanvasGroup group, float start, float end)
        {
            group.interactable = true;
            float counter = 0f;
            float duration = 1f;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                group.alpha = Mathf.Lerp(start, end, counter / duration);
                yield return null;
            }
        }
    }
}