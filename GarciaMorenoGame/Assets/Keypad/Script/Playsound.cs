using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Playsound : MonoBehaviour

{
    private GameObject codePanel = default;
    private Keypad keypad;

    void Start()
    {
        codePanel = GameObject.FindGameObjectWithTag("CodeInput");
        keypad = codePanel.GetComponent<Keypad>();
    }

    public void Clicky()
    {
        GetComponent<AudioSource>().Play();
    }

    public void pressKey()
    {
        string input = GetComponentInChildren<Text>().text;
        Debug.Log("Presionaste: " + input);
        keypad.addCode(input);
    }

    public void greenKey()
    {
        keypad.openDoor();
    }

    public void redKey()
    {
        keypad.eraseAll();
    }

    public void exit()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Keypad");
        g.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerControler.instance.CanMove = true;
    }


}
