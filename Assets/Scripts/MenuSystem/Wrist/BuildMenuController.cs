using System;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Wrist
{
    public class BuildMenuController : MonoBehaviour
    {
        public PrefabIcon[] AllBases;
        public PrefabIcon[] AllSmallRooms;
        public PrefabIcon[] AllLargeRooms;
        public PrefabIcon[] AllCornerRooms;
        public PrefabIcon[] AllRoofs;
        
        public Transform contentParent;
        public GameObject objectButtonPrefab;

        public void PopulateBaseData()
        {
            DestroyPreviousData();
            for (int i = 0; i < AllBases.Length; i++)
            {
                GameObject button = Instantiate(objectButtonPrefab, contentParent) as GameObject;
                button.transform.GetChild(0).GetComponent<Image>().sprite = AllBases[i].icon;
                button.name = AllBases[i].type;
            }
        }
        public void PopulateSmallRoomData()
        {
            DestroyPreviousData();
            for (int i = 0; i < AllSmallRooms.Length; i++)
            {
                GameObject button = Instantiate(objectButtonPrefab, contentParent) as GameObject;
                button.transform.GetChild(0).GetComponent<Image>().sprite = AllSmallRooms[i].icon;
                button.name = AllSmallRooms[i].type;
            }
        }
        
        public void PopulateLargeRoomData()
        {
            DestroyPreviousData();
            for (int i = 0; i < AllLargeRooms.Length; i++)
            {
                GameObject button = Instantiate(objectButtonPrefab, contentParent) as GameObject;
                button.transform.GetChild(0).GetComponent<Image>().sprite = AllLargeRooms[i].icon;
                button.name = AllLargeRooms[i].type;
            }
        }
        
        public void PopulateCornerRoomData()
        {
            DestroyPreviousData();
            for (int i = 0; i < AllCornerRooms.Length; i++)
            {
                GameObject button = Instantiate(objectButtonPrefab, contentParent) as GameObject;
                button.transform.GetChild(0).GetComponent<Image>().sprite = AllCornerRooms[i].icon;
                button.name = AllCornerRooms[i].type;
            }
        }
        
        public void PopulateRoofData()
        {
            DestroyPreviousData();
            for (int i = 0; i < AllRoofs.Length; i++)
            {
                GameObject button = Instantiate(objectButtonPrefab, contentParent) as GameObject;
                button.transform.GetChild(0).GetComponent<Image>().sprite = AllRoofs[i].icon;
                button.name = AllRoofs[i].type;
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