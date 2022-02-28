using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : Interactable
{
    public Image noteImage;
    public Button closeBtn;
    public AudioClip pickUpSound;
    public AudioClip putAwaySound;

    void Start()
    {
        enableUI(false);
        closeBtn.onClick.AddListener(hideNoteImage);
    }

    private void enableUI(bool setBool) {
        noteImage.enabled = setBool;
        closeBtn.gameObject.SetActive(setBool);
    }

    public override void onInteract()
    {
        showNoteImage();
    }

    public void showNoteImage()
    {
        PlayerControler.instance.CanMove = false;
        enableUI(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<AudioSource>().PlayOneShot(pickUpSound);
    }

    public void hideNoteImage()
    {
        enableUI(false);
        GetComponent<AudioSource>().PlayOneShot(putAwaySound);
        PlayerControler.instance.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
