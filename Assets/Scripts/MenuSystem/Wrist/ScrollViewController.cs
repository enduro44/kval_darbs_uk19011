using System;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Wrist
{
    public class ScrollViewController : MonoBehaviour
    {
        public Transform contentParent;
        public GameObject objectButtonPrefab;
        
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
        private void DestroyPreviousData()
        {
            foreach (Transform child in contentParent) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    
    
}