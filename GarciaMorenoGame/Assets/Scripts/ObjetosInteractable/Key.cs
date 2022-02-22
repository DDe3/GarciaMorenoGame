using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    
    // Poner todos los tipos de llaves aqui
    [SerializeField] private KeyType keyType;
    public enum KeyType {
        Test
    }

    public KeyType GetKeyType() {
        return keyType;
    }
}
