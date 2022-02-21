using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    private Outline outline;
    public virtual void Awake() {
        gameObject.layer = 6;
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    public abstract void onInteract();
    public void onFocus() {
        outline.enabled = true;
        Debug.Log("Viendo a: "+ gameObject.name);
    }
    public void onLoseFocus() {
        outline.enabled = false;
        Debug.Log("Perdido de vista: "+ gameObject.name);
    }

}
