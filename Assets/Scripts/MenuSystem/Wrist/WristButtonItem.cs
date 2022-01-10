using UnityEngine;

namespace MenuSystem.Wrist
{
    public class WristButtonItem : MonoBehaviour
    {
        //Klase satur sturkūru ritināma skata pogas uzbūvei
        public string type;
        public void OnButtonClick()
        {
            InventoryController inventoryController = InventoryController.Instance();
            inventoryController.InstantiateNewObject(gameObject.name, type);
        }
    }
}
