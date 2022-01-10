using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Wrist
{
    public class ScrollViewController : MonoBehaviour
    {
        //Klase nodrošina ritināmā skata loģiku, datu pievienošanu
        public static Transform contentParent;
        public static GameObject scrollView;
        public GameObject objectButtonPrefab;

        private void Awake()
        {
            contentParent = gameObject.transform;
            scrollView = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        }

        //Klase saņem kādus datus tai ir jāpievieno ritināmajā skatā un izveido pogas, kuras
        //spēlētājs var spiest, lai izveidotu spēles objektus
        public void PopulateData(PrefabIcon[] data, string type)
        {
            DestroyPreviousData();
            for (int i = 0; i < data.Length; i++)
            {
                GameObject button = Instantiate(objectButtonPrefab, contentParent) as GameObject;
                button.transform.GetChild(0).GetComponent<Image>().sprite = data[i].icon;
                button.name = data[i].type;
                button.GetComponent<WristButtonItem>().type = type;
            }
        }

        public static void DestroyPreviousData()
        {
            foreach (Transform child in contentParent) {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void HideScrollView()
        {
            scrollView.SetActive(false);
        }
        
        public static void ShowScrollView()
        {
            scrollView.SetActive(true);
        }
    }
}