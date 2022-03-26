using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCambioEscena : MonoBehaviour
{
    int index;

    private void Start() {
        index = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            SceneManager.LoadScene(index+1);
        }
    }
}
