using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{

    public override void onInteract()
    {
        print("Interactuaste con " + gameObject.name);
    }
    
}
