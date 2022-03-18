using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevaPartida : MonoBehaviour
{

    private int index;
    private void Start() {
        index = SceneManager.GetActiveScene().buildIndex;
    }
    public void playGame() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(loadScene());
    }

    public void quitGame() {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }


    IEnumerator loadScene() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index+1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
