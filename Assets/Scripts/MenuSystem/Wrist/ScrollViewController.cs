using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Wrist
{
    public class ScrollViewController : MonoBehaviour
    {
        public static Transform contentParent;
        public static GameObject scrollView;
        public GameObject objectButtonPrefab;

        private void Awake()
        {
            contentParent = gameObject.transform;
            scrollView = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        }

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