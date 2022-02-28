using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{

    [Header("Parametros de nombre")]
    [SerializeField] public string nombre = "";
    public Text textElement;
    private Outline outline;
    public virtual void Awake() {
        gameObject.layer = 6;
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    public abstract void onInteract();
    public void onFocus() {
        outline.enabled = true;
        if (textElement!=null) {
            textElement.text = nombre;
        }
    
    }
    public void onLoseFocus() {
        outline.enabled = false;
        if (textElement!=null) {
            textElement.text = "";
        }
    }
}
