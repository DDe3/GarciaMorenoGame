using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Pickable item;
    public Image icon;
    public Text itemName;
    public Text description;

    public void addItem(Pickable newItem) 
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void clearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void clickItem()
    {
        clearDescription();
        if (item!=null) 
        {
            Debug.Log("Objeto de inventario clikeado");
            showDescription();
        }
    }

    private void showDescription() {
        itemName.text = item.nombre;
        description.text = item.descriptionEnInventario;
    }

    private void clearDescription() {
        itemName.text = "";
        description.text = "";
    }
    
}
