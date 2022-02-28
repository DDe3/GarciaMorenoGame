using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{

    [Header("Sonidos")]
    [SerializeField] private AudioSource pickUpAudioSource = default;
    [SerializeField] private AudioClip pickUpSound = default;

    public override void onInteract()
    {
        base.onLoseFocus();
        Key key = gameObject.GetComponent<Key>();
        if (key != null) {
            KeyHolder keyHolder = PlayerControler.instance.GetComponent<KeyHolder>();
            keyHolder.addKey(key.GetKeyType());
            pickUpAudioSource.PlayOneShot(pickUpSound);
            StartCoroutine(destruir(key));
        }
    }

    public IEnumerator destruir(Key key) {
        yield return new WaitUntil(() => !pickUpAudioSource.isPlaying );
        Destroy(key.gameObject);
    }

    
}
