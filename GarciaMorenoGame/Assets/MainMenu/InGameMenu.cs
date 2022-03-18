using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    
    public GameObject gameObjectUI;

    private int index;

    private void Start() {
        index = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) {
                resume();
            } else {
                pause();
            }
        }
    }

    public void pause()
    {
        PlayerControler.instance.CanMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObjectUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void resume()
    {
        gameObjectUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        PlayerControler.instance.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void loadMenu() {
        resume();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(index-1);
    }

    public void quitGame() {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
