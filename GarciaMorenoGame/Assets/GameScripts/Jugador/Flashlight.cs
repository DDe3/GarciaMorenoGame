using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    [SerializeField] private KeyCode turnOnOffKey = KeyCode.F;

    [SerializeField] private AudioSource flashlightAudioSource = default;
    [SerializeField] private AudioClip turnOnClip = default;
    [SerializeField] private AudioClip turnOffClip = default;
    public GameObject flashlight;


    public bool on;
    public bool off;

    public bool canUseFlashlihgt;



    void Start()
    {
        off = true;
        flashlight.SetActive(false);
        canUseFlashlihgt = false;
    }


    public void activateFlashlight()
    {
        canUseFlashlihgt = true;
    }


    void Update()
    {
        if (canUseFlashlihgt)
        {
            if (off && Input.GetKeyDown(turnOnOffKey))
            {
                flashlight.SetActive(true);
                flashlightAudioSource.PlayOneShot(turnOnClip);
                off = false;
                on = true;
            }
            else if (on && Input.GetKeyDown(turnOnOffKey))
            {
                flashlight.SetActive(false);
                flashlightAudioSource.PlayOneShot(turnOffClip);
                off = true;
                on = false;
            }
        }
    }
}
