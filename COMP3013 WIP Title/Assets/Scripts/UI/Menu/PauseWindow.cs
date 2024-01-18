using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindow : BaseWindow
{
    public void ExitMainMenu()
    {
        Destroy(GameObject.FindWithTag("UI"));
        SceneManager.LoadScene("MainMenu");
    }

    public void TogglePause()
    {
        if(GameManager.Instance.isGamePaused)
        {
            ResumeGame();
            return;
        }

        PauseGame();
    }

    public void ResumeGame() 
    {
        HideWindow();
        GameManager.Instance.SetPause(false);
    }

    public void PauseGame() 
    {
        ShowWindow();
        GameManager.Instance.SetPause(true);
    }

    public void Awake()
    {
        HideWindow();
    }


}
