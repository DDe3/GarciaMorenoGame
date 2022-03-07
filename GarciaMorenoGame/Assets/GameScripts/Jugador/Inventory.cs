using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Mas de una instancia de inventario encontrada!");
        }
        instance = this;

    }
    #endregion


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public List<Pickable> items = new List<Pickable>();
    public int space = 9;


    public bool addKey(Pickable keyType)
    {
        if (items.Count>=space) 
        {
            return false;
        }
        items.Add(keyType);
        notifyChange();
        return true;
    }

    public void removeKey(Pickable keyType)
    {
        items.Remove(keyType);
        notifyChange();
    }

    public bool containsKey(Pickable.KeyType keyType)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetKeyType() == keyType) {
                return true;
            }
        }
        return false;
    }

    private void notifyChange() {
        if (onItemChangedCallBack!=null) {
            onItemChangedCallBack.Invoke();
        }
    }
}
