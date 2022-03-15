using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLuz : MonoBehaviour
{
    private Light luz = default;
    private AudioSource sonido;


    public float minTime, maxTime, timer;


    private void Start()
    {
        luz = GetComponentInParent<Light>();
        sonido = GetComponentInParent<AudioSource>();
        timer = Random.Range(minTime, maxTime);
    }

    private void OnTriggerStay(Collider other)
    {
        flicker();
    }

    void flicker()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            luz.enabled = !luz.enabled;
            timer = Random.Range(minTime, maxTime);
            if (!sonido.isPlaying)
            {
                sonido.Play();
            }

        }
    }
}
