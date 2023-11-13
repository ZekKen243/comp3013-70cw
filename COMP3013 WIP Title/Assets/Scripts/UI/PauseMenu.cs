using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isGamePaused = false;
    public Canvas canvas;


    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            TogglePauseMenu();
        }
    }

    void Start()
    {
        this.HideMenu();
    }


    public void ResumeGame()
    {
        this.HideMenu();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void TogglePauseMenu()
    {
        isGamePaused = !isGamePaused;
        
        if(isGamePaused )
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }

    }


    private void ShowMenu()
    {
        canvas.enabled = true;
        Time.timeScale = 1f;
    }

    private void HideMenu()
    {
        canvas.enabled = false;
        Time.timeScale = 0f;
    }


}
