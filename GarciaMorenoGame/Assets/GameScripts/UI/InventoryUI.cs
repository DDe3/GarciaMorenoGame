using UnityEngine;

public class InventoryUI : MonoBehaviour
{


    public Transform itemsParent;
    Inventory inventory;

    InventorySlot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
    }

    void UpdateUI() 
    {
        Debug.Log("Modificando inventario...");
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].addItem(inventory.items[i]);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
        

    }
}
