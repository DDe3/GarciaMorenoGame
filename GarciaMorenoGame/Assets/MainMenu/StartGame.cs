using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("volumen")) {
            AudioListener.volume = PlayerPrefs.GetFloat("volumen");
        } 
    }

}
