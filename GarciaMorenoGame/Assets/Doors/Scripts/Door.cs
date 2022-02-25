using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{


    [Header("Sonido")]
    [SerializeField] private AudioSource doorOpenAudioSource = null;
    [SerializeField] private float openDelay = 0.0f;
    [SerializeField] private AudioSource doorCloseAudioSource = null;
    [SerializeField] private float closeDelay = 0.0f;

    [SerializeField] private AudioSource doorShutAudioSource = null;
    
    


    private bool isOpen = false;
    private bool canBeInteractedWith = true;
    private Animator anim;
    [SerializeField] private Key.KeyType keyType;



    public Key.KeyType getKeyType()
    {
        return keyType;
    }

    private IEnumerator pauseDoorInteraction(float secs) {
        canBeInteractedWith = false;
        yield return new WaitForSeconds(secs);
        canBeInteractedWith = true;
    }


    private IEnumerator automaticClose() {
        
        while (isOpen) {
            yield return new WaitForSeconds(3);
            if (Vector3.Distance(transform.position, PlayerControler.instance.transform.position) > 10) {
                isOpen = false;
                doorCloseAudioSource.PlayDelayed(closeDelay);
                anim.SetFloat("dot", 0);
                anim.SetBool("isOpen", false);
            }
        }
    }



    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void onInteract()
    {
        if (canBeInteractedWith)
        {
            StartCoroutine(pauseDoorInteraction(2.5f));
            KeyHolder keyList = PlayerControler.instance.GetComponent<KeyHolder>();
            if (keyList.containsKey(keyType))
            {
                Vector3 doorTransformDirection = transform.TransformDirection(Vector3.back);
                Vector3 playerTransformDirection = PlayerControler.instance.transform.position - transform.position;
                float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);
                if (isOpen)
                {
                    doorCloseAudioSource.PlayDelayed(closeDelay);
                    
                }
                else
                {
                    doorOpenAudioSource.PlayDelayed(openDelay);

                }
                isOpen = !isOpen;
                anim.SetFloat("dot", dot);
                anim.SetBool("isOpen", isOpen);
                StartCoroutine(automaticClose());
                
            } else {
                doorShutAudioSource.Play();
            }

        }
    }
}
