using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{

    [Header("Parametros de nombre")]
    public string nombre = "";
    public Text textName = default;
    public Text textComment = default;
    private Outline outline;

    public virtual void Awake()
    {
        gameObject.layer = 6;
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    public abstract void onInteract();
    public void onFocus()
    {

        outline.enabled = true;
        if (textName != null)
        {
            textName.enabled = true;
            textName.text = nombre;
            textName.color = new Color(textName.color.r, textName.color.g, textName.color.b, 1);
        }

    }
    public void onLoseFocus()
    {
        outline.enabled = false;
        if (textName != null)
        {
            StartCoroutine(FadeTextToZeroAlpha(0.2f, textName));
        }
    }


    private IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    private IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator handleFade(Text i)
    {
        StartCoroutine(FadeTextToFullAlpha(1f, i));
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeTextToZeroAlpha(1f, i));
    }
}
