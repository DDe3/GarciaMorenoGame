using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{

    [SerializeField] private GameObject Player;
    public override void onInteract()
    {
        Key key = gameObject.GetComponent<Key>();
        if (key != null) {
            KeyHolder keyHolder = Player.GetComponent<KeyHolder>();
            keyHolder.addKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
    }
    
}
