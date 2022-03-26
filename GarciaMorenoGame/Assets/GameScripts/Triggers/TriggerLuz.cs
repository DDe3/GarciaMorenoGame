using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLuz : MonoBehaviour
{
    public Light luz = default;
    public AudioSource sonido;


    public float minTime, maxTime, timer;


    private void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            flicker();
        }
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
