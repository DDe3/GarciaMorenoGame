using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Keypad : MonoBehaviour
{

    [Header("Codigo")]
    [SerializeField] private string CODIGO = "0000";
    [SerializeField] private GameObject puertaQueAbre = default;

    [Header("Sonidos")]
    private AudioSource audioSource = default;
    [SerializeField] private AudioClip correctAudio = default;
    [SerializeField] private AudioClip incorrectAudio = default;

    public Text[] digitos;
    private List<string> cod;
    int current;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        digitos = GetComponentsInChildren<Text>();
        cod = new List<string>();
        current = 0;
        //Debug.Log("Digitos esta vacio? " + digitos.Length);
    }

    public void addCode(string s)
    {
        if (current < 4)
        {
            digitos[current].text = s;
            cod.Add(s);
            current += 1;
        }

    }


    public void eraseAll()
    {
        foreach (Text t in digitos)
        {
            t.text = "";
        }
        current = 0;
        cod = new List<string>();
    }


    public void openDoor()
    {
        string inp = string.Empty;
        foreach (string s in cod)
        {
            inp += s;
        }
        if (inp.Equals(CODIGO))
        {
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(correctAudio);
            }
            Debug.Log("CORRECTO!");
            Door puerta = puertaQueAbre.GetComponent<Door>();
            puerta.isClosed = false;
        } else {
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(incorrectAudio);
            }
        }
        eraseAll();

    }


}
