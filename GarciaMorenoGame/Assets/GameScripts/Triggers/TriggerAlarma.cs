using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAlarma : MonoBehaviour
{

    private bool active = true; 
    public AudioSource source = default;
    public AudioClip sonido = default; 
    private void OnTriggerExit(Collider other) {
        if (active) {
            source.PlayOneShot(sonido);
            active = false;
        }
    }
}
