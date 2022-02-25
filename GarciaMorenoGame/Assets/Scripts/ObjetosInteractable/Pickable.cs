using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{

    public override void onInteract()
    {
        Key key = gameObject.GetComponent<Key>();
        if (key != null) {
            KeyHolder keyHolder = PlayerControler.instance.GetComponent<KeyHolder>();
            keyHolder.addKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
    }
    
}
