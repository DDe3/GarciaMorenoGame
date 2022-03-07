using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : Interactable
{

    [Header("Dibujo")]
    [SerializeField] public Sprite icon;
    [Header("Tipo")]
    [SerializeField] private KeyType keyType;
    [Header("Sonidos")]
    [SerializeField] private AudioSource pickUpAudioSource = default;
    [SerializeField] private AudioClip pickUpSound = default;


    public enum KeyType  // Poner aqui todos los tipos de llave que hay
    {
        Test
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }

    public override void onInteract()
    {
        bool wasPickedUp = Inventory.instance.addKey(this);
        if (wasPickedUp)
        {
            pickUpAudioSource.PlayOneShot(pickUpSound);
            StartCoroutine(destruir());
        }
        else
        {
            StartCoroutine(handleFade(textComment));
            textComment.text = "No tengo espacio para este objeto";
        }
    }

    public IEnumerator destruir()
    {
        yield return new WaitUntil(() => !pickUpAudioSource.isPlaying);
        base.textName.enabled = false;
        gameObject.SetActive(false);
        
    }


}
