using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickUp : Pickable
{
    public override void doSomethingElse()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Flashlight>().activateFlashlight();
    }
}
