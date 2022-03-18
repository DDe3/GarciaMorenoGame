using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("volumen")){
            PlayerPrefs.SetFloat("volumen", 0.5f);
        }
        load();
    }

    public void changeVolume() {
        AudioListener.volume = volumeSlider.value;
        save();
    }

    private void load() 
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumen");
    }

    private void save() {
        PlayerPrefs.SetFloat("volumen", volumeSlider.value);
        PlayerPrefs.Save();
    }

}
