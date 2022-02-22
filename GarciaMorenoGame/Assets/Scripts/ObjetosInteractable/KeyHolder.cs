using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;

    private void Awake() {
        keyList = new List<Key.KeyType>();
    }


    public void addKey(Key.KeyType keyType) {
        Debug.Log("Añadido key de tipo: " + keyType);
        keyList.Add(keyType);
    }

    public void removeKey(Key.KeyType keyType) {
        keyList.Remove(keyType);
    }

    public bool containsKey(Key.KeyType keyType) {
        return keyList.Contains(keyType);
    }
}
