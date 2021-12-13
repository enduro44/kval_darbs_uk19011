using UnityEngine;

namespace MenuSystem.Wrist
{
    public class WristButtonItem : MonoBehaviour
    {
        public void OnButtonClick()
        {
            InventoryController inventoryController = InventoryController.Instance();
            inventoryController.InstantiateNewObject(gameObject.name);
        }
    }
}
