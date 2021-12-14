using UnityEngine;

namespace MenuSystem.Wrist
{
    public class PlayablesMenuController : MonoBehaviour
    {
        public const string TYPE = "playable";
        public PrefabIcon[] AllDolls;
        public PrefabIcon[] AllMonsters;
        public PrefabIcon[] AllPets;
        public PrefabIcon[] AllCars;
        public PrefabIcon[] AllAdditionalPlayables;
        
        public ScrollViewController controller;
        
        public void PopulateDollsData()
        {
            controller.PopulateData(AllDolls, TYPE);
        }
        
        public void PopulateMonstersData()
        {
            controller.PopulateData(AllMonsters, TYPE);
        }
        
        public void PopulatePetsData()
        {
            controller.PopulateData(AllPets, TYPE);
        }
        
        public void PopulateCarsData()
        {
            controller.PopulateData(AllCars, TYPE);
        }
        
        public void PopulateAdditionalPlayablesData()
        {
            controller.PopulateData(AllAdditionalPlayables, TYPE);
        }
    }
}