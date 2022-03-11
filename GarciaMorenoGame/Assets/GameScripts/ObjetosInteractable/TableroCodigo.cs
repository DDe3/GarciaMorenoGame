using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableroCodigo : Interactable
{
    public GameObject tableroUI;

    void Start() {
        tableroUI.SetActive(false);
    }

    public override void onInteract()
    {
        bool invHud = !tableroUI.activeSelf;
        tableroUI.SetActive(invHud);
        if (tableroUI)
        {
            PlayerControler.instance.CanMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
