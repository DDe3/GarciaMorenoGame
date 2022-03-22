using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NoteStart : MonoBehaviour
{
    // Start is called before the first frame update

    public Image noteImage;
    public Button closeBtn;
    public Text text;

    void Start()
    {
        enableUI(false);
    }

    private void enableUI(bool setBool) {
        text.enabled = setBool;
        noteImage.enabled = setBool;
        closeBtn.gameObject.SetActive(setBool);
    }
}
