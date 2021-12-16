using System;
using UnityEngine;

namespace MenuSystem.Wrist
{
    public class FurnishMenuController : MonoBehaviour
    {
        public const string TYPE = "furniture";
        public PrefabIcon[] AllKitchenFurniture;
        public PrefabIcon[] AllLivingroomFurniture;
        public PrefabIcon[] AllBedroomFurniture;
        public PrefabIcon[] AllBathroomFurniture;
        public PrefabIcon[] AllAccessories;
        
        public ScrollViewController controller;

        public void PopulateKitchenFurnitureData()
        {
            controller.PopulateData(AllKitchenFurniture, TYPE);
        }
        public void PopulateLivingroomFurnitureData()
        {
            controller.PopulateData(AllLivingroomFurniture, TYPE);
        }
        
        public void PopulateBedroomData()
        {
            controller.PopulateData(AllBedroomFurniture, TYPE);
        }

        public void PopulateBathroomFurnitureData()
        {
            controller.PopulateData(AllBathroomFurniture, TYPE);
        }
        
        public void PopulateAccessoriesData()
        {
            controller.PopulateData(AllAccessories, TYPE);
        }
    }
}
